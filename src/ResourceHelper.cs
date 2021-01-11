using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace restlessmedia.Module
{
  public static class ResourceHelper
  {
    public static bool Exists(Assembly assembly, string resource)
    {
      return assembly.GetManifestResourceNames().Contains(resource);
    }

    public static bool Exists(Assembly assembly, string resource, out Stream stream)
    {
      if (string.IsNullOrWhiteSpace(resource))
      {
        throw new ArgumentNullException(nameof(resource));
      }

      stream = assembly.GetManifestResourceStream(resource);

      return stream != null;
    }

    public static Stream GetResourceStream(Assembly assembly, string resource)
    {
      if (!Exists(assembly, resource, out Stream stream))
      {
        throw new FileNotFoundException($"No resource matching '{resource}' could be found in assembly '{assembly.FullName}'.");
      }

      return stream;
    }
  }
}