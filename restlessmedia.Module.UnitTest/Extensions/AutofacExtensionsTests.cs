using Autofac;
using restlessmedia.Test;
using System;
using System.IO;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class AutofacExtensionsTests
  {
    [Fact]
    public void AddJsonFile_does_not_overflow()
    {
      ContainerBuilder containerBuilder = new ContainerBuilder();

      Action action = () => AutofacExtensions.AddJsonFile(containerBuilder, "test.json");

      // this is intentional as the json file won't exist
      // we are protecting against an issue where the extension
      // was calling itself and not JsonConfigurationExtensions
      action.MustThrow<FileNotFoundException>();
    }
  }
}