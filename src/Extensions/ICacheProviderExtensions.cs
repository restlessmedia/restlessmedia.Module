using System;

namespace restlessmedia.Module
{
  public static class ICacheProviderExtensions
  {
    /// <summary>
    /// Adds an item to the cache, if any exceptions occur, the item isn't written and false is returned throwing no exceptions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public static bool TryAdd<T>(this ICacheProvider cacheProvider, string key, T value, TimeSpan? expiry = null)
    {
      try
      {
        cacheProvider.Add(key, value, expiry);
        return true;
      }
      catch (Exception e)
      {
        new TraceLog().Exception(e);
        return false;
      }
    }

    /// <summary>
    /// Gets an item from the cache, if any exceptions occur, false is returned and <paramref name="value"/> is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="valueProvider"></param>
    /// <param name="value"></param>
    /// <param name="expiry"></param>
    /// <returns></returns>
    public static bool TryGet<T>(this ICacheProvider cacheProvider, string key, Func<T> valueProvider, out T value, TimeSpan? expiry = null)
      where T : class
    {
      try
      {
        value = cacheProvider.Get<T>(key, valueProvider, expiry);
        return true;
      }
      catch (Exception e)
      {
        value = default(T);
        new TraceLog().Exception(e);
        return false;
      }
    }
  }
}
