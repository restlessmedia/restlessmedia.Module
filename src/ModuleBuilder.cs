using Autofac;
using restlessmedia.Module.Caching;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
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

      IEnumerable<Assembly> assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
      RegisterModules(containerBuilder, assemblies);
    }

    public static void RegisterModules(ContainerBuilder containerBuilder, Assembly assembly)
    {
      Type abstractModuleType = typeof(IModule);
      Trace.TraceInformation($"ModuleBuilder scanning {assembly.FullName} for {abstractModuleType.Name} types.");
      foreach (Type moduleType in GetAssemblyTypes(assembly).Where(x => x != null && !x.IsAbstract && x != abstractModuleType && abstractModuleType.IsAssignableFrom(x)))
      {
        RegisterModule(containerBuilder, moduleType);
      }
    }

    public static void RegisterModule(ContainerBuilder containerBuilder, Type type)
    {
      Trace.TraceInformation($"Registering module components for {type.FullName}.");
      IModule module = Activator.CreateInstance(type) as IModule;
      module.RegisterComponents(containerBuilder);
    }

    private static void RegisterComponents(ContainerBuilder containerBuilder)
    {
      #region settings
      containerBuilder.RegisterSettings<ILicenseSettings>("restlessmedia/license", required: true);
      containerBuilder.RegisterSettings<IDatabaseSettings>("restlessmedia/database", required: true);
      containerBuilder.RegisterSettings<IRoleSettings>("restlessmedia/role");
      #endregion

      #region contexts
      containerBuilder.RegisterType<DataContext>().As<IDataContext>().SingleInstance();
      #endregion

      #region services
      containerBuilder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
      containerBuilder.RegisterType<EntityService>().As<IEntityService>().SingleInstance();
      #endregion

      #region providers
      containerBuilder.RegisterType<SecurityDataProvider>().As<ISecurityDataProvider>().SingleInstance();
      containerBuilder.RegisterType<ProfileDataProvider>().As<IProfileDataProvider>().SingleInstance();
      containerBuilder.RegisterType<AuthDataProvider>().As<IAuthDataProvider>().SingleInstance();
      containerBuilder.RegisterType<EntityDataProvider>().As<IEntityDataProvider>().SingleInstance();
      containerBuilder.RegisterType<PubSubProvider>().As<IPubSubProvider>().SingleInstance();

      // this is the default cache provider, most applications override with redis cache which is done via json config
      containerBuilder.RegisterType<HttpCacheProvider>().As<ICacheProvider>().SingleInstance();
      #endregion
    }

    private static void RegisterModules(ContainerBuilder containerBuilder, IEnumerable<Assembly> assemblies)
    {
      foreach (Assembly assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>())
      {
        RegisterModules(containerBuilder, assembly);
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
  }
}