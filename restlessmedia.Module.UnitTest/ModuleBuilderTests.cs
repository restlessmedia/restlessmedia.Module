using Autofac;
using restlessmedia.Test;
using SqlBuilder.DataServices;
using System;
using System.Data;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class ModuleBuilderTests
  {
    [Fact]
    public void RegisterComponents_does_not_override_connection_factory_if_already_registered()
    {
      // set-up
      ContainerBuilder containerBuilder = new ContainerBuilder();

      // register the type which shouldn't get overridden
      containerBuilder.RegisterType<TestConnectionFactory>().As<IConnectionFactory>();

      // build
      ModuleBuilder.RegisterGlobalComponents(containerBuilder);

      // assert
      containerBuilder.Build().Resolve<IConnectionFactory>().MustBeA<TestConnectionFactory>();
    }

    private class TestConnectionFactory : IConnectionFactory
    {
      public IDbConnection CreateConnection(bool open = false)
      {
        throw new NotImplementedException();
      }

      public IDbTransaction CreateTransaction(IDbConnection connection)
      {
        throw new NotImplementedException();
      }

      public IDbTransaction CreateTransaction(bool open = false)
      {
        throw new NotImplementedException();
      }
    }
  }
}