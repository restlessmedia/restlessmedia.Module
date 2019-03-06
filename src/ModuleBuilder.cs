using Autofac;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Security;
using restlessmedia.Module.Security.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
      containerBuilder.RegisterSettings<ILicenseSettings>("restlessmedia/license", required: true);
      containerBuilder.RegisterSettings<IDatabaseSettings>("restlessmedia/database", required: true);
      containerBuilder.RegisterSettings<IRoleSettings>("restlessmedia/role");

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

        Trace.TraceInformation($"ModuleBuilder scanning {assembly.FullName} for {abstractModuleType.Name} types.");

        Type moduleType = GetAssemblyTypes(assembly).SingleOrDefault(x => x.IsAssignableFrom(abstractModuleType));

        if (moduleType == null)
        {
          continue;
        }

        RegisterModule(containerBuilder, moduleType);
      }
    }

    private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
    {
      try
      {
        return assembly.GetTypes();
      }
      catch (ReflectionTypeLoadException e)
      {
        return e.Types;
      }
    }

    private static void RegisterModule(ContainerBuilder containerBuilder, Type type)
    {
      Trace.TraceInformation($"Registering module components for {type.FullName}.");
      IModule module = Activator.CreateInstance(type) as IModule;
      module.RegisterComponents(containerBuilder);
    }
  }
}