using restlessmedia.Module.Configuration;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace restlessmedia.Module.Caching
{
  public class RedisCacheProvider : CacheProviderBase, ICacheProvider, IDisposable
  {
    public RedisCacheProvider(ICacheSettings cacheSettings, ILog log)
      : base(cacheSettings, log) { }

    public override void Add<T>(string key, T value, TimeSpan? expiry = null)
    {
      WithDatabase(database => database.StringSet(key, Serialize(value), expiry));
    }

    public override T Get<T>(string key)
    {
      byte[] result = WithDatabase(database => database.StringGet(key));

      if (result != null)
      {
        try
        {
          return Deserialize<T>(result);
        }
        catch (SerializationException)
        {
          Remove(key);
        }
      }

      return default;
    }

    /// <summary>
    /// Removes an entry by key. If a wilcard is used, the remove will be performed across all available endpoint databases.
    /// </summary>
    /// <param name="key"></param>
    public void Remove(string key)
    {
      if (string.IsNullOrEmpty(key))
      {
        throw new ArgumentNullException(nameof(key));
      }

      if (key.EndsWith("*"))
      {
        RemoveFromAllServers(key);
      }
      else
      {
        WithDatabase(database => database.KeyDelete(key));
      }
    }

    public bool Exists(string key)
    {
      return WithDatabase(database => database.KeyExists(key));
    }

    public void Dispose()
    {
      if (_connection != null)
      {
        _connection.Dispose();
        _connection = null;
      }
    }

    protected virtual bool TryGetDatabase(out IDatabase database)
    {
      try
      {
        database = GetConnection().GetDatabase();
      }
      catch (Exception e)
      {
        // if we fail to connect, null out database and null out connection
        // there are cases where there is a connection but the database is failing
        _connection = null;
        database = null;
        Log.Exception(e);
      }

      return database != null;
    }

    private ConnectionMultiplexer GetConnection()
    {
      if (_connection == null)
      {
        lock (_connectionLock)
        {
          if (_connection == null)
          {
            _connection = ConnectionMultiplexer.Connect(GetOptions(CacheSettings));
          }
        }
      }

      return _connection;
    }

    private T WithDatabase<T>(Func<IDatabase, T> invoker)
    {
      if (!TryGetDatabase(out IDatabase database))
      {
        return default;
      }

      return invoker(database);
    }

    private void RemoveFromAllServers(string pattern)
    {
      try
      {
        ConnectionMultiplexer connection = GetConnection();

        foreach (System.Net.EndPoint endPoint in connection.GetEndPoints())
        {
          IServer server = connection.GetServer(endPoint);
          RedisKey[] keys = server.Keys(pattern: pattern).ToArray();
          server.Multiplexer.GetDatabase().KeyDeleteAsync(keys);
        }
      }
      catch (Exception e)
      {
        Log.Exception(e);
      }
    }

    private static ConfigurationOptions GetOptions(ICacheSettings cacheSettings)
    {
      ConfigurationOptions options = new ConfigurationOptions
      {
        Ssl = cacheSettings.Ssl,
        ConnectRetry = cacheSettings.Retry,
        ConnectTimeout = cacheSettings.Timeout,
        AbortOnConnectFail = false,
      };

      if (!string.IsNullOrEmpty(cacheSettings.AccessKey))
      {
        options.Password = cacheSettings.AccessKey;
      }

      foreach (EndPoint endPoint in cacheSettings.EndPointCollection)
      {
        options.EndPoints.Add(endPoint.Host);
      }

      return options;
    }

    private static readonly object _connectionLock = new object();

    private static ConnectionMultiplexer _connection;
  }
}