using Autofac.Configuration;
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
      builder.AddJsonFile(jsonFile);
      ConfigurationModule module = new ConfigurationModule(configBuilder.Build());
      builder.RegisterModule(module);
    }
  }
}