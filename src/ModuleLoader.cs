using Autofac;
using SqlBuilder.DataServices;
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
      if (assembly != null)
      {
        foreach (Type moduleType in GetModuleTypes(assembly))
        {
          factory(CreateModule(moduleType));
        }
      }
      else
      {
        Load(factory, "*.module");
      }
    }

    /// <summary>
    /// Loads modules automatically, or for the given assembly that match the <paramref name="pattern"/>.
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="pattern"></param>
    public static void Load(Action<TModule> factory, string pattern)
    {
      foreach (TModule module in FindModules(pattern))
      {
        factory(module);
      }
    }

    /// <summary>
    /// Finds modules by looking for files with given <paramref name="pattern"/>.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEnumerable<TModule> FindModules(string pattern)
    {
      foreach(Type moduleType in FindModuleTypes(pattern))
      {
        yield return CreateModule(moduleType);
      }
    }

    /// <summary>
    /// Finds module types by looking for .module files.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IEnumerable<Type> FindModuleTypes(string pattern)
    {
      // look for .module files in the bin folder
      string executingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
      executingDirectory = executingDirectory.Replace("file:\\", "");
      return Directory.GetFiles(executingDirectory, pattern)
        .SelectMany(GetModuleTypes);
    }

    /// <summary>
    /// Returns module types based on the .module file.
    /// </summary>
    /// <param name="pathToModuleFile"></param>
    /// <returns></returns>
    private static IEnumerable<Type> GetModuleTypes(string pathToModuleFile)
    {
      string moduleFile = Path.GetFileName(pathToModuleFile);
      string assemblyName = moduleFile.Replace(".module", string.Empty);
      Assembly moduleAssembly = Assembly.Load(assemblyName);
      return GetModuleTypes(moduleAssembly);
    }

    private static IEnumerable<Type> GetModuleTypes(Assembly assembly)
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