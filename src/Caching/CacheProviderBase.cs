using restlessmedia.Module.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace restlessmedia.Module.Caching
{
  public abstract class CacheProviderBase
  {
    public CacheProviderBase(ICacheSettings cacheSettings)
    {
      CacheSettings = cacheSettings;
    }

    public abstract T Get<T>(string key)
      where T : class;

    public abstract void Add<T>(string key, T value, TimeSpan? expiry = null);

    public bool TryAdd<T>(string key, T value, TimeSpan? expiry = null)
    {
      try
      {
        Add(key, value, expiry);
        return true;
      }
      catch
      {
        // TODO: Log
        return false;
      }
    }

    public T Get<T>(string key, Func<T> valueProvider, TimeSpan? expiry = null)
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

      // if it's not null (default), return it
      if (!EqualityComparer<T>.Default.Equals(result, default))
      {
        return result;
      }

      result = valueProvider();

      if (!ReferenceEquals(result, null))
      {
        // if it's not null, try to add it
        TryAdd(key, result, expiry);
      }

      return result;
    }

    public string GetKey(params object[] keys)
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
  }
}