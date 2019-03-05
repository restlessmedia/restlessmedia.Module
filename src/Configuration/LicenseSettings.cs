using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  internal class LicenseSettings : SerializableConfigurationSection, ILicenseSettings
  {
    [ConfigurationProperty(_licenseKeyProperty, IsRequired = true)]
    public Guid LicenseKey
    {
      get
      {
        return (Guid)this[_licenseKeyProperty];
      }
    }

    [ConfigurationProperty(_companyNameProperty, IsRequired = true)]
    public string CompanyName
    {
      get
      {
        return (string)this[_companyNameProperty];
      }
    }

    [ConfigurationProperty(_virtualDirectoryProperty, IsRequired = false)]
    public string VirtualDirectory
    {
      get
      {
        return (string)this[_virtualDirectoryProperty];
      }
    }

    public IEnumerable<IFeature> FeatureCollection
    {
      get
      {
        return Features.GetAll();
      }
    }

    public string ApplicationName
    {
      get
      {
        return System.Web.Security.Membership.ApplicationName;
      }
    }

    [ConfigurationProperty(_featuresProperty, IsRequired = false)]
    [ConfigurationCollection(typeof(FeatureCollection), AddItemName = "add")]
    private FeatureCollection Features
    {
      get { return (FeatureCollection)this[_featuresProperty]; }
    }

    private const string _licenseKeyProperty = "licenseKey";

    private const string _companyNameProperty = "companyName";

    private const string _virtualDirectoryProperty = "virtualDirectory";

    private const string _featuresProperty = "features";
  }
}