﻿using restlessmedia.Module.Configuration;
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

    public T Get<T>(string key, Func<T> valueProvider)
      where T : class
    {
      T result = default(T);

      try
      {
        result = Get<T>(key);
      }
      catch (Exception e)
      {
        if (e is ArgumentException || e is SerializationException)
        {
          // catch the situations where the cached value can't be deserialised
          result = default(T);
        }
      }

      if (!EqualityComparer<T>.Default.Equals(result, default(T)))
      {
        return result;
      }

      result = valueProvider();

      if (!ReferenceEquals(result, null))
      {
        Add(key, result);
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