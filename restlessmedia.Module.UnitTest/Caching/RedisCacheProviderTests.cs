using FakeItEasy;
using restlessmedia.Module.Caching;
using restlessmedia.Module.Configuration;
using restlessmedia.Test;
using System;
using Xunit;

namespace restlessmedia.Module.UnitTest.Caching
{
  public class RedisCacheProviderTests
  {
    [Fact]
    public void Add_does_not_throw_when_cache_not_available()
    {
      RedisCacheProvider redisCacheProvider = CreateInstance();

      // this should fail - there is no connection set-up
      Action action = () => redisCacheProvider.Add("foo", DateTime.Now);

      // assert that it fails silently
      action.MustNotThrow();
    }

    private RedisCacheProvider CreateInstance()
    {
      ICacheSettings cacheSettings = A.Fake<ICacheSettings>();
      ILog log = A.Fake<ILog>();
      RedisCacheProvider cacheProvider = new RedisCacheProvider(cacheSettings, log);
      return cacheProvider;
    }
  }
}