using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace restlessmedia.Module
{
  /// <summary>
  /// Used for find types in loaded assemblies.
  /// </summary>
  /// <typeparam name="TModule"></typeparam>
  public class ModuleLoader<TModule>
  {
    /// <summary>
    /// Loads modules automatically, or for the given assembly.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="assembly"></param>
    public static void Load(Action<TModule> factory, Assembly assembly = null)
    {
      IEnumerable<Type> types = assembly != null ? FindModuleTypes(assembly) : FindModuleTypes();

      foreach (Type moduleType in types)
      {
        TModule module = CreateModule(moduleType);
        factory(module);
      }
    }

    /// <summary>
    /// Finds module types by looking for .module files.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Type> FindModuleTypes()
    {
      // look for .module files in the bin folder
      string executingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
      executingDirectory = executingDirectory.Replace("file:\\", "");
      return Directory.GetFiles(executingDirectory, "*.module").SelectMany(FindModuleTypes);
    }

    private static IEnumerable<Type> FindModuleTypes(string pathToModuleFile)
    {
      string moduleFile = Path.GetFileName(pathToModuleFile);
      string assemblyName = moduleFile.Replace(".module", string.Empty);
      Assembly moduleAssembly = Assembly.Load(assemblyName);
      return FindModuleTypes(moduleAssembly);
    }

    private static IEnumerable<Type> FindModuleTypes(Assembly assembly)
    {
      Type abstractModuleType = typeof(TModule);
      Trace.TraceInformation($"{abstractModuleType.FullName} module loader scanning {assembly.FullName} for {abstractModuleType.Name} types.");
      return GetAssemblyTypes(assembly).Where(x => x != null && !x.IsAbstract && x != abstractModuleType && abstractModuleType.IsAssignableFrom(x));
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
  }
}