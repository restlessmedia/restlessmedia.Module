using Autofac;
using System.Configuration;

namespace restlessmedia.Module
{
  public static class IModuleExtensions
  {
    public static void RegisterSettings<T>(this ContainerBuilder containerBuilder, string path, T defaultValue)
    {
      object section = ConfigurationManager.GetSection(path);

      if (section == null)
      {
        section = defaultValue;
      }

      Register<T>(containerBuilder, path, section);
    }

    public static void RegisterSettings<T>(this ContainerBuilder containerBuilder, string path, bool required = false)
    {
      object section = ConfigurationManager.GetSection(path);

      if (section == null)
      {
        if (required)
        {
          throw new ConfigurationErrorsException($"Required configuration section '{path}' not found and is marked as required.");
        }

        return;
      }

      Register<T>(containerBuilder, path, section);
    }

    private static void Register<T>(ContainerBuilder containerBuilder, string path, object section)
    {
      if (!(section is T))
      {
        throw new ConfigurationErrorsException($"Configuration section {section.GetType().FullName} found at '{path}' is not type of {typeof(T).FullName}.");
      }

      containerBuilder.Register(x => section).As<T>().SingleInstance();
    }
  }
}