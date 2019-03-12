using restlessmedia.Module.Configuration;
using StackExchange.Redis;
using System;

namespace restlessmedia.Module.Caching
{
  public class RedisCacheProvider : CacheProviderBase, ICacheProvider
  {
    public RedisCacheProvider(ICacheSettings cacheSettings)
      : base(cacheSettings) { }

    public override void Add<T>(string key, T value, TimeSpan? expiry = null)
    {
      GetDatabase().StringSet(key, Serialize(value), expiry);
    }

    public override T Get<T>(string key)
    {
      byte[] result = GetDatabase().StringGet(key);

      if (result == null)
      {
        return default(T);
      }

      return Deserialize<T>(result);
    }

    public void Remove(string key)
    {
      GetDatabase().KeyDelete(key);
    }

    public bool Exists(string key)
    {
      return GetDatabase().KeyExists(key);
    }

    protected ConnectionMultiplexer Connection
    {
      get
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
    }

    private void Add(string key, byte[] value, TimeSpan? expiry = null)
    {
      GetDatabase().StringSet(key, value, expiry);
    }

    private IDatabase GetDatabase()
    {
      return Connection.GetDatabase();
    }

    private static ConfigurationOptions GetOptions(ICacheSettings cacheSettings)
    {
      ConfigurationOptions options = new ConfigurationOptions
      {
        Ssl = cacheSettings.Ssl,
        ConnectRetry = cacheSettings.Retry,
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