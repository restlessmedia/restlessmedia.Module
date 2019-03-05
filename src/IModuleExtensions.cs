using Autofac;
using System.Configuration;

namespace restlessmedia.Module
{
  public static class IModuleExtensions
  {
    public static void RegisterSettings<T>(this ContainerBuilder containerBuilder, string path, bool required = false)
    {
      object section = ConfigurationManager.GetSection(path);

      if (section == null && required)
      {
        throw new ConfigurationErrorsException($"Required configuration section '{path}' not found.");
      }

      if (section is T)
      {
        containerBuilder.Register(x => section).As<T>().SingleInstance();
      }
      else
      {
        throw new ConfigurationErrorsException($"Configuration section {section.GetType().FullName} found at '{path}' is not type of {typeof(T).FullName}.");
      }
    }
  }
}