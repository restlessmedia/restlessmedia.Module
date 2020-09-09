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
  public class ModuleLoader<TModule> : ModuleLoader
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
        Load(factory, DefaultModuleExtension);
      }
    }

    /// <summary>
    /// Loads modules automatically, or for the given assembly that match the <paramref name="extension"/>.
    /// </summary>
    /// <example>
    /// Given SampleModule.module - when .module is passed in as the <paramref name="extension"/> would attempt to load a module called 'SampleModule'.
    /// </example>
    /// <param name="factory"></param>
    /// <param name="extension"></param>
    public static void Load(Action<TModule> factory, string extension)
    {
      foreach (TModule module in FindModules(extension))
      {
        factory(module);
      }
    }

    /// <summary>
    /// Finds modules by looking for files with given <paramref name="extension"/>.
    /// </summary>
    /// <example>
    /// Given SampleModule.module - when .module is passed in as the <paramref name="extension"/> would attempt to load a module called 'SampleModule'.
    /// </example>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static IEnumerable<TModule> FindModules(string extension)
    {
      foreach (Type moduleType in FindModuleTypes(extension))
      {
        yield return CreateModule(moduleType);
      }
    }

    /// <summary>
    /// Finds module types by looking for files with the given <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static IEnumerable<Type> FindModuleTypes(string extension)
    {
      string pattern = string.Concat("*", GetExtension(extension));
      return DirectoryGetFiles(ExecutingDirectory, pattern)
        .SelectMany(path => GetModuleTypes(path, extension));
    }

    /// <summary>
    /// Returns module types based on the .module file.
    /// </summary>
    /// <param name="pathToModuleFile"></param>
    /// <returns></returns>
    private static IEnumerable<Type> GetModuleTypes(string pathToModuleFile, string extension)
    {
      string moduleFile = Path.GetFileName(pathToModuleFile);
      string assemblyName = moduleFile.Replace(extension, string.Empty);
      Assembly moduleAssembly = AssemblyLoad(assemblyName);
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

  public class ModuleLoader
  {
    static ModuleLoader()
    {
      ExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
    }

    /// <summary>
    /// Factory method for returning file paths for a given directory path.
    /// </summary>
    internal static Func<string, string, string[]> DirectoryGetFiles = Directory.GetFiles;

    /// <summary>
    /// Factory method for returning an assembly for a given name.
    /// </summary>
    internal static Func<string, Assembly> AssemblyLoad = Assembly.Load;

    protected readonly static string ExecutingDirectory;

    protected const string DefaultModuleExtension = ".module";

    protected static string GetExtension(string extension)
    {
      if (string.IsNullOrEmpty(extension))
      {
        throw new ArgumentNullException(nameof(extension), $"The extension passed into {nameof(ModuleLoader)} is null. Modules cannot be found with a null or empty extension.");
      }

      const string period = ".";

      if (extension.StartsWith(period))
      {
        return extension;
      }

      return string.Concat(period, extension);
    }
  }
}