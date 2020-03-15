using FakeItEasy;
using restlessmedia.Module.Caching;
using restlessmedia.Module.Configuration;
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

      redisCacheProvider.Add("foo", DateTime.Now);
    }

    private RedisCacheProvider CreateInstance()
    {
      ICacheSettings cacheSettings = A.Fake<ICacheSettings>();
      RedisCacheProvider cacheProvider = new RedisCacheProvider(cacheSettings);
      return cacheProvider;
    }
  }
}