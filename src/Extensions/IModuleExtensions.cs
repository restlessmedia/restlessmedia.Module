using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System;
using System.Configuration;

namespace restlessmedia.Module
{
  public static class IModuleExtensions
  {
    public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterIf<T, TResolvesAs>(this ContainerBuilder containerBuilder, Func<IComponentRegistry, bool> predicate)
      where T : TResolvesAs
    {
      IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = RegistrationBuilder.ForType<T>();

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
    public static void RegisterWhen<T>(this ContainerBuilder containerBuilder, Func<IComponentRegistry, bool> predicate, Func<T> whenTrue, Func<T> whenFalse)
    {
      containerBuilder.Register(x => whenTrue())
        .OnlyIf(x => predicate(x))
        .As<T>();

      containerBuilder.Register(x => whenFalse())
        .OnlyIf(x => !predicate(x))
        .As<T>();
    }

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