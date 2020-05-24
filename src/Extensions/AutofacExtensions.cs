﻿using Autofac.Configuration;
using Autofac.Core;
using Autofac.Core.Registration;
using Microsoft.Extensions.Configuration;
using System;

namespace Autofac
{
  public static class AutofacExtensions
  {
    /// <summary>
    /// Adds a json config file to the registration.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="jsonFile"></param>
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

    /// <summary>
    /// Determines whether the specified service is registered.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="componentRegistry"></param>
    /// <returns></returns>
    public static bool IsRegistered<T>(this IComponentRegistryBuilder componentRegistry)
    {
      return componentRegistry.IsRegistered(new TypedService(typeof(T)));
    }
  }
}