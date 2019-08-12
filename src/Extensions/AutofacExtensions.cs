using Autofac.Configuration;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace Autofac
{
  public static class AutofacExtensions
  {
    public static void AddJsonFile(this ContainerBuilder builder, string jsonFile)
    {
      if (string.IsNullOrEmpty(jsonFile))
      {
        throw new ArgumentNullException(nameof(jsonFile), $"{nameof(jsonFile)} cannot be null or empty for configuring Autofac container.");
      }

      ConfigurationBuilder configBuilder = new ConfigurationBuilder();
      JsonConfigurationExtensions.AddJsonFile(configBuilder, jsonFile);
      ConfigurationModule module = new ConfigurationModule(configBuilder.Build());
      builder.RegisterModule(module);
    }

    /// <summary>
    /// Determines whether the specified service is registered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="componentRegistry"></param>
    /// <returns></returns>
    public static bool IsRegistered<T>(this IComponentRegistry componentRegistry)
    {
      return componentRegistry.IsRegistered(new TypedService(typeof(T)));
    }
  }
}