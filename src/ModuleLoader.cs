using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
    public static void Load(Action<TModule> factory, Assembly assembly = null)
    {
      if (AutoLoad)
      {
        if (assembly != null)
        {
          FindAndRegisterTypes(assembly);
        }
        else
        {
          FindAndRegisterTypes();
        }
      }

      foreach (Type moduleType in _modulesToLoad)
      {
        TModule module = CreateModule(moduleType);
        factory(module);
      }
    }

    public static void Register<T>()
      where T : TModule
    {
      Register(typeof(T));
    }

    public static void Register(Type type)
    {
      lock (_modulesToLoadLock)
      {
        if (!_modulesToLoad.Contains(type))
        {
          _modulesToLoad.Add(type);
        }
      }
    }

    public static bool AutoLoad = true;
    
    private static void FindAndRegisterTypes()
    {
      foreach (Assembly assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>())
      {
        FindAndRegisterTypes(assembly);
      }
    }

    private static void FindAndRegisterTypes(Assembly assembly)
    {
      Type abstractModuleType = typeof(TModule);
      Trace.TraceInformation($"{abstractModuleType.FullName} module loader scanning {assembly.FullName} for {abstractModuleType.Name} types.");
      foreach (Type moduleType in GetAssemblyTypes(assembly)
        .Where(x => x != null && !x.IsAbstract && x != abstractModuleType && abstractModuleType.IsAssignableFrom(x)))
      {
        Register(moduleType);
        Trace.TraceInformation($"Registering module components for {moduleType.FullName}.");
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
        if (e.LoaderExceptions != null && e.LoaderExceptions.Length > 0)
        {
          Trace.TraceError($"{nameof(ModuleLoader<TModule>)} caught exception for assembly {assembly.FullName}. {string.Join(", ", e.LoaderExceptions.Select(x => x.Message))}");
        }
        else
        {
          Trace.TraceError($"{nameof(ModuleLoader<TModule>)} caught exception for assembly {assembly.FullName}.");
        }
        return e.Types;
      }
    }

    private static TModule CreateModule(Type type)
    {
      NewExpression newExp = Expression.New(type);
      LambdaExpression lambda = Expression.Lambda(typeof(ModuleActivator), newExp, new ParameterExpression[] { });
      ModuleActivator activator = (ModuleActivator)lambda.Compile();
      return activator();
    }

    private delegate TModule ModuleActivator();

    private static IList<Type> _modulesToLoad = new List<Type>();

    private static object _modulesToLoadLock = new object();
  }
}