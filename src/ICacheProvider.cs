using System;

namespace restlessmedia.Module
{
  public interface ICacheProvider : IProvider
  {
    void Add<T>(string key, T value, TimeSpan? expiry = null);

    T Get<T>(string key)
      where T : class;

    T Get<T>(string key, Func<T> valueProvider)
      where T : class;

    void Remove(string key);

    bool Exists(string key);

    string GetKey(params object[] keys);
  }
}