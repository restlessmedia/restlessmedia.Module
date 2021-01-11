using FakeItEasy;
using restlessmedia.Module.Caching;
using restlessmedia.Module.Configuration;
using restlessmedia.Test;
using StackExchange.Redis;
using System;
using Xunit;

namespace restlessmedia.Module.UnitTest.Caching
{
  public class RedisCacheProviderTests
  {
    public RedisCacheProviderTests()
    {
      ICacheSettings cacheSettings = A.Fake<ICacheSettings>();
      ILog log = A.Fake<ILog>();
      _databaseFactory = A.Fake<Func<IDatabase>>();
      _cacheProvider = new RedisCacheProvider(cacheSettings, log, _databaseFactory);
    }

    [Fact]
    public void Add_does_not_throw_when_cache_not_available()
    {
      // this should fail - there is no connection set-up
      Action action = () => _cacheProvider.Add("foo", DateTime.Now);

      // assert that it fails silently
      action.MustNotThrow();
    }

    [Fact]
    public void cache_item_removed_when_deserialisation_fails()
    {
      // set-up
      IDatabase database = A.Fake<IDatabase>();

      A.CallTo(() => _databaseFactory()).Returns(database);
      A.CallTo(() => database.StringGet(A<RedisKey>.Ignored, A<CommandFlags>.Ignored)).Returns("some-nonsense");

      // call
      _cacheProvider.Get<object>("test");

      // assert
      A.CallTo(() => database.KeyDelete("test", A<CommandFlags>.Ignored)).MustHaveHappened();
    }

    [Fact]
    public void get_adds_from_valueprovider_when_not_initially_found_in_cache()
    {
      // set-up
      IDatabase database = A.Fake<IDatabase>();
      object valueToCache = 4;

      A.CallTo(() => _databaseFactory()).Returns(database);
      A.CallTo(() => database.StringGet(A<RedisKey>.Ignored, A<CommandFlags>.Ignored)).Returns((byte[])null);

      // call
      _cacheProvider.Get("test", () => valueToCache);

      // assert
      A.CallTo(() => database.StringSet("test", A<RedisValue>.Ignored, A<TimeSpan?>.Ignored, A<When>.Ignored, A<CommandFlags>.Ignored)).MustHaveHappened();
    }

    private readonly RedisCacheProvider _cacheProvider;

    private readonly Func<IDatabase> _databaseFactory;
  }
}