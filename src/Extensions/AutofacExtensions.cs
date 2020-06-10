using Autofac.Builder;
using Autofac.Configuration;
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

    /// <summary>
    /// Registers if the predicate returns true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResolvesAs"></typeparam>
    /// <param name="containerBuilder"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterIf<T, TResolvesAs>(this ContainerBuilder containerBuilder, Func<IComponentRegistryBuilder, bool> predicate)
      where T : TResolvesAs
    {
      IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = RegistrationBuilder.ForType<T>().As<TResolvesAs>();

      registrationBuilder.RegistrationData.DeferredCallback = containerBuilder.RegisterCallback(x =>
      {
        if (predicate(x))
        {
          RegistrationBuilder.RegisterSingleComponent(x, registrationBuilder);
        }
      });

      return registrationBuilder;
    }

    /// <summary>
    /// Provides a way to construct a different type based on the existance of another registration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="containerBuilder"></param>
    /// <param name="predicate"></param>
    /// <param name="whenTrue"></param>
    /// <param name="whenFalse"></param>
    public static IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> RegisterWhen<T>(this ContainerBuilder containerBuilder, Func<IComponentRegistryBuilder, bool> predicate, Func<IComponentContext, T> whenTrue, Func<IComponentContext, T> whenFalse)
    {
      IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> registrationBuilder = containerBuilder.Register(whenTrue)
        .OnlyIf(x => predicate(x))
        .As<T>();

      registrationBuilder = containerBuilder.Register(whenFalse)
        .OnlyIf(x => !predicate(x))
        .As<T>();

      return registrationBuilder;
    }
  }
}