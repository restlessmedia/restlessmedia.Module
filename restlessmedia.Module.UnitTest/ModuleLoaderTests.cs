using Autofac;
using FakeItEasy;
using restlessmedia.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class ModuleLoaderTests : IDisposable
  {
    public ModuleLoaderTests()
    {
      // we do this to get around the fact we are using Directory.GetFiles()
      _preTestDirectoryGetFiles = ModuleLoader.DirectoryGetFiles;
      _preTestAssemblyLoad = ModuleLoader.AssemblyLoad;
      ModuleLoader.DirectoryGetFiles = A.Fake<Func<string, string, string[]>>();
      ModuleLoader.AssemblyLoad = A.Fake<Func<string, Assembly>>();

      
    }

    [Fact]
    public void RegisterModules_finds_modules_in_current_asembly()
    {
      ContainerBuilder containerBuilder = new ContainerBuilder();
      List<IModule> found = new List<IModule>();

      // call
      ModuleLoader<IModule>.Load(found.Add, Assembly.GetExecutingAssembly());

      // assert
      found.Count.MustBe(2);
    }

    [Fact]
    public void RegisterModules_finds_modules_by_file()
    {
      // set-up
      ContainerBuilder containerBuilder = new ContainerBuilder();
      List<IModule> found = new List<IModule>();

      // fake the directory get files result
      A.CallTo(() => ModuleLoader.DirectoryGetFiles(A<string>.Ignored, A<string>.Ignored))
        .Returns(new string[] { @"c:\bin\test.testmodule" });

      // we fake the return assembly to be the current, test assembly. this is because we can't fake up the Assembly type due to ctor restrictions.
      A.CallTo(() => ModuleLoader.AssemblyLoad("test"))
        .Returns(Assembly.GetExecutingAssembly());

      // call
      ModuleLoader<IModule>.Load(found.Add, ".testmodule");

      // assert

      // finds 2 because we return the current assembly from AssemblyLoad (fake) which has two module classes.
      found.Count.MustBe(2);

      // shouldn't really need this but here anyway
      A.CallTo(() => ModuleLoader.AssemblyLoad("test"))
        .MustHaveHappened();
    }

    [Fact]
    public void FindModules()
    {
      // set-up

      // fake the directory get files result
      A.CallTo(() => ModuleLoader.DirectoryGetFiles(A<string>.Ignored, A<string>.Ignored))
        .Returns(new string[] { @"c:\bin\test.module" });

      // call & assert

      // finds 2 because we return the current assembly from AssemblyLoad (fake) which has two module classes.
      ModuleLoader<IModule>.FindModules(".module").Count().MustBe(2);
    }

    [Fact]
    public void FindModuleTypes()
    {
      // set-up

      // fake the directory get files result
      A.CallTo(() => ModuleLoader.DirectoryGetFiles(A<string>.Ignored, A<string>.Ignored))
        .Returns(new string[] { @"c:\bin\test.module" });

      // fake the assembly returned
      A.CallTo(() => ModuleLoader.AssemblyLoad("test"))
       .Returns(Assembly.GetExecutingAssembly());

      // call
      IEnumerable<Type> types = ModuleLoader<IModule>.FindModuleTypes(".module");

      // assert
      types.MustContain(typeof(TestModule), typeof(TestInheritanceModule));
    }

    public void Dispose()
    {
      ModuleLoader.DirectoryGetFiles = _preTestDirectoryGetFiles;
      ModuleLoader.AssemblyLoad = _preTestAssemblyLoad;
    }

    public class TestModule : IModule
    {
      public virtual void RegisterComponents(ContainerBuilder containerBuilder) { }
    }

    public class TestInheritanceModule : TestModule
    {
      public override void RegisterComponents(ContainerBuilder containerBuilder) { }
    }

    public abstract class TestAssert
    {
      public abstract void Call();
    }

    private readonly Func<string, string, string[]> _preTestDirectoryGetFiles;

    private readonly Func<string, Assembly> _preTestAssemblyLoad;
  }
}