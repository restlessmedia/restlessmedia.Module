using Autofac;
using Autofac.Core.Registration;
using restlessmedia.Test;
using System;
using System.IO;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class AutofacExtensionsTests
  {
    [Fact]
    public void AddJsonFile_does_not_overflow()
    {
      ContainerBuilder containerBuilder = new ContainerBuilder();

      Action action = () => AutofacExtensions.AddJsonFile(containerBuilder, "test.json");

      // this is intentional as the json file won't exist
      // we are protecting against an issue where the extension
      // was calling itself and not JsonConfigurationExtensions
      action.MustThrow<FileNotFoundException>();
    }

    [Fact]
    public void RegisterIf_true()
    {
      // set-up
      ContainerBuilder containerBuilder = new ContainerBuilder();

      // call
      containerBuilder.RegisterIf<Foo, IFoo>(registry => true);

      // assert
      IContainer container = containerBuilder.Build();
      container.Resolve<IFoo>().MustBeA<Foo>();
    }

    [Fact]
    public void RegisterIf_false()
    {
      // set-up
      ContainerBuilder containerBuilder = new ContainerBuilder();

      // call
      containerBuilder.RegisterIf<Foo, IFoo>(registry => false);

      // assert
      IContainer container = containerBuilder.Build();
      Action action = () => container.Resolve<IFoo>();
      action.MustThrow<ComponentNotRegisteredException>();
    }

    private class Foo : IFoo { }

    private interface IFoo { }
  }
}