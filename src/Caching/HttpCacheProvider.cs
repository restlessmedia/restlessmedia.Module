using restlessmedia.Module.Configuration;
using System;
using System.Web;

namespace restlessmedia.Module.Caching
{
  internal class HttpCacheProvider : CacheProviderBase, ICacheProvider
  {
    public HttpCacheProvider(ICacheSettings cacheSettings, ILog log)
      : base(cacheSettings, log) { }

    public override void Add<T>(string key, T value, TimeSpan? expiry = null)
    {
      if (expiry.HasValue)
      {
        HttpContext.Cache.Insert(key, value, null, DateTime.Now.Add(expiry.Value), System.Web.Caching.Cache.NoSlidingExpiration);
      }
      else
      {
        HttpContext.Cache.Insert(key, value);
      }
    }

    public override T Get<T>(string key)
    {
      object result = HttpContext.Cache.Get(key);
      return result is T t ? t : default(T);
    }

    public void Remove(string key)
    {
      HttpContext.Cache.Remove(key);
    }

    public bool Exists(string key)
    {
      return HttpContext.Cache[key] != null;
    }

    private HttpContextBase HttpContext
    {
      get
      {
        return _httpContext ??= new HttpContextWrapper(System.Web.HttpContext.Current);
      }
    }

    private HttpContextBase _httpContext;
  }
}