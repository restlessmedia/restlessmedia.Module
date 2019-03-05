using System;
using System.Collections.Generic;

namespace restlessmedia.Module.Configuration
{
  public interface ILicenseSettings
  {
    Guid LicenseKey { get; }

    string CompanyName { get; }

    string VirtualDirectory { get; }

    IEnumerable<IFeature> FeatureCollection { get; }

    string ApplicationName { get; }
  }
}