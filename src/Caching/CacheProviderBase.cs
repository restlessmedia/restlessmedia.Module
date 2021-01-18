using restlessmedia.Module.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace restlessmedia.Module.Caching
{
  internal abstract class CacheProviderBase
  {
    public CacheProviderBase(ICacheSettings cacheSettings, ILog log)
    {
      CacheSettings = cacheSettings;
      Log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public abstract T Get<T>(string key)
      where T : class;

    public abstract void Add<T>(string key, T value, TimeSpan? expiry = null);

    public virtual T Get<T>(string key, Func<T> valueProvider, TimeSpan? expiry = null)
      where T : class
    {
      T result = default;

      try
      {
        result = Get<T>(key);
      }
      catch (Exception e)
      {
        if (e is ArgumentException || e is SerializationException)
        {
          // catch the situations where the cached value can't be deserialised
          result = default;
        }
      }

      // if the result from the cahce is not null (default), return it
      if (!EqualityComparer<T>.Default.Equals(result, default))
      {
        return result;
      }

      result = valueProvider();

      // if the result from the valueProvider is not null, add it
      if (result is object)
      {
        Add(key, result, expiry);
      }

      return result;
    }

    public virtual string GetKey(params object[] keys)
    {
      const string separator = ":";
      return string.Join(separator, keys);
    }

    protected static byte[] Serialize<T>(T obj)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize(memoryStream, obj);
        return memoryStream.ToArray();
      }
    }

    protected static T Deserialize<T>(byte[] data)
    {
      using (MemoryStream stream = new MemoryStream(data))
      {
        T result = (T)new BinaryFormatter().Deserialize(stream);
        return result;
      }
    }

    protected readonly ICacheSettings CacheSettings;

    protected readonly ILog Log;
  }
}