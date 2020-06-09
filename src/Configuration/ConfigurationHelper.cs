using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public class ConfigurationHelper
  {
    public static T GetSection<T>(string path)
    {
      return GetSection<T>(path, true);
    }

    public static T GetSection<T>(string path, bool required)
    {
      if (!TryGetSection(path, out object section) && required)
      {
        throw new ConfigurationErrorsException($"Unable to find configuration section for {path}.");
      }

      return (T)section;
    }

    public static bool IsDefined(string path)
    {
      return ConfigurationManager.GetSection(path) != null;
    }

    public static bool TryGetSection(string path, out object section)
    {
      return (section = ConfigurationManager.GetSection(path)) != null;
    }
  }
}