using Autofac;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace restlessmedia.Module.UnitTest
{
  [TestClass]
  public class ModuleBuilderTests
  {
    [TestMethod]
    public void RegisterModules_finds_modules_in_current_asembly()
    {
      ContainerBuilder containerBuilder = new ContainerBuilder();
      TestAssert testAssert = A.Fake<TestAssert>();
      containerBuilder.RegisterCallback(x => testAssert.Call());

      ModuleBuilder.RegisterModules(containerBuilder, Assembly.GetExecutingAssembly());
      
      containerBuilder.Build();

      A.CallTo(() => testAssert.Call()).MustHaveHappened();
    }

    public class TestModule : IModule
    {
      public void RegisterComponents(ContainerBuilder containerBuilder) { }
    }

    public abstract class TestAssert
    {
      public abstract void Call();
    }
  }
}