using Autofac;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Security;
using restlessmedia.Module.Security.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace restlessmedia.Module
{
  public class ModuleBuilder
  {
    public static void RegisterModules(ContainerBuilder containerBuilder)
    {
      RegisterComponents(containerBuilder);
      RegisterAssemblies(containerBuilder);
    }

    private static void RegisterComponents(ContainerBuilder containerBuilder)
    {
      // settings
      containerBuilder.RegisterSettings<LicenseSettings>("restlessmedia/license", required: true);
      containerBuilder.RegisterSettings<DatabaseSettings>("restlessmedia/database");
      containerBuilder.RegisterSettings<RoleSettings>("restlessmedia/role");

      // services
      containerBuilder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();

      // providers
      containerBuilder.RegisterType<SecurityDataProvider>().As<ISecurityDataProvider>().SingleInstance();
      containerBuilder.RegisterType<ProfileDataProvider>().As<IProfileDataProvider>().SingleInstance();
      containerBuilder.RegisterType<AuthDataProvider>().As<IAuthDataProvider>().SingleInstance();
    }

    private static void RegisterAssemblies(ContainerBuilder containerBuilder)
    {
      IEnumerable<Assembly> assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
      RegisterAssemblies(containerBuilder, assemblies);
    }

    private static void RegisterAssemblies(ContainerBuilder containerBuilder, IEnumerable<Assembly> assemblies)
    {
      Type abstractModuleType = typeof(IModule);
      Assembly currentAssembly = Assembly.GetExecutingAssembly();

      foreach (Assembly assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>())
      {
        if (assembly == currentAssembly)
        {
          continue;
        }

        Type moduleType = assembly.GetTypes().SingleOrDefault(x => x.IsAssignableFrom(abstractModuleType));

        if (moduleType == null)
        {
          continue;
        }

        RegisterModule(containerBuilder, moduleType);
      }
    }

    private static void RegisterModule(ContainerBuilder containerBuilder, Type type)
    {
      IModule module = Activator.CreateInstance(type) as IModule;
      module.RegisterComponents(containerBuilder);
    }
  }
}