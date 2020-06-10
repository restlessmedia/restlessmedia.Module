using System;
using System.Linq;

namespace restlessmedia.Module.Configuration
{
  public static class ILicenseSettingsExtensions
  {
    public static bool HasFeature(this ILicenseSettings licenseSettings, string name)
    {
      return licenseSettings.FeatureCollection != null && licenseSettings.FeatureCollection.Any(x => x.Name == name);
    }

    public static bool IsEnabled(this ILicenseSettings licenseSettings, string name)
    {
      if (licenseSettings.FeatureCollection == null)
      {
        return false;
      }

      IFeature feature = licenseSettings.FeatureCollection.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

      return feature != null && feature.Enabled;
    }
  }
}