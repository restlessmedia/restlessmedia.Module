using System;

namespace restlessmedia.Module
{
  public interface ICacheProvider : IProvider
  {
    /// <summary>
    /// Adds an item to the cache with the given <paramref name="key"/>.  If an item already exists with the same name, it is overwritten.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    void Add<T>(string key, T value, TimeSpan? expiry = null);

    /// <summary>
    /// Gets an item of type <typeparamref name="T"/> with the given key <paramref name="key"/>.  If the item does not exist, this will return null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T Get<T>(string key)
      where T : class;

    /// <summary>
    /// Gets an item of type <typeparamref name="T"/> with the given key <paramref name="key"/>.  If the item does not exist, the result from <paramref name="valueProvider"/> will be added to the cache and this result returned.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="valueProvider"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    T Get<T>(string key, Func<T> valueProvider, TimeSpan? expiry = null)
      where T : class;

    /// <summary>
    /// Removes an item from the cache with the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    void Remove(string key);

    /// <summary>
    /// Returns true if an item in the cache exists with the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool Exists(string key);

    /// <summary>
    /// Returns a key for this cache provider based on the <paramref name="keys"/> provided.
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    string GetKey(params object[] keys);
  }
}