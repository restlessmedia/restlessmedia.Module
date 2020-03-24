using restlessmedia.Module.Configuration;
using StackExchange.Redis;
using System;

namespace restlessmedia.Module.Caching
{
  public class RedisCacheProvider : CacheProviderBase, ICacheProvider
  {
    public RedisCacheProvider(ICacheSettings cacheSettings, ILog log)
      : base(cacheSettings)
    {
      _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public override void Add<T>(string key, T value, TimeSpan? expiry = null)
    {
      WithDatabase(database => database.StringSet(key, Serialize(value), expiry));
    }

    public override T Get<T>(string key)
    {
      byte[] result = WithDatabase(database => database.StringGet(key));

      if (result == null)
      {
        return default;
      }

      return Deserialize<T>(result);
    }

    public void Remove(string key)
    {
      WithDatabase(database => database.KeyDelete(key));
    }

    public bool Exists(string key)
    {
      return WithDatabase(database => database.KeyExists(key));
    }

    protected virtual bool TryGetDatabase(out IDatabase database)
    {
      database = null;

      try
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

        database = _connection.GetDatabase();
      }
      catch (Exception e)
      {
        _connection = null;
        database = null;
        _log.Exception(e);
      }

      return database != null;
    }

    private T WithDatabase<T>(Func<IDatabase, T> invoker)
    {
      if (TryGetDatabase(out IDatabase database))
      {
        return invoker(database);
      }

      return default;
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

    private readonly ILog _log;
  }
}