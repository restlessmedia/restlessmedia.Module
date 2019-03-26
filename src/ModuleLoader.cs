using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace restlessmedia.Module
{
  /// <summary>
  /// Used for find types in loaded assemblies.
  /// </summary>
  /// <typeparam name="TModule"></typeparam>
  public class ModuleLoader<TModule>
  {
    public static void Load(Action<TModule> factory)
    {
      foreach (TModule module in FindModules())
      {
        factory(module);
      }
    }

    public static void Load(Assembly assembly, Action<TModule> factory)
    {
      foreach (TModule module in FindModules(assembly))
      {
        factory(module);
      }
    }

    public static IEnumerable<TModule> FindModules()
    {
      return BuildManager.GetReferencedAssemblies().Cast<Assembly>().SelectMany(assembly => FindModules(assembly));
    }

    public static IEnumerable<TModule> FindModules(Assembly assembly)
    {
      Type abstractModuleType = typeof(TModule);
      Trace.TraceInformation($"{abstractModuleType.FullName} module loader scanning {assembly.FullName} for {abstractModuleType.Name} types.");
      foreach (Type moduleType in GetAssemblyTypes(assembly).Where(x => x != null && !x.IsAbstract && x != abstractModuleType && abstractModuleType.IsAssignableFrom(x)))
      {
        Trace.TraceInformation($"Registering module components for {moduleType.FullName}.");
        yield return (TModule)Activator.CreateInstance(moduleType);
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