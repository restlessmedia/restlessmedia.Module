using Autofac;
using restlessmedia.Test;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class ModuleLoaderTests
  {
    [Fact]
    public void RegisterModules_finds_modules_in_current_asembly()
    {
      ContainerBuilder containerBuilder = new ContainerBuilder();
      List<IModule> found = new List<IModule>();

      // call
      ModuleLoader<IModule>.Load(Assembly.GetExecutingAssembly(), found.Add);
      
      // assert
      found.Count.MustBe(2);
    }

    public class TestModule : IModule
    {
      public virtual void RegisterComponents(ContainerBuilder containerBuilder) { }
    }

    public class TestInheritnceModule : TestModule
    {
      public override void RegisterComponents(ContainerBuilder containerBuilder) { }
    }

    public abstract class TestAssert
    {
      public abstract void Call();
    }
  }
}