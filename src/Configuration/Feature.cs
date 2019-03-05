using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public class Feature : ConfigurationElement, IFeature
  {
    /// <summary>
    /// Serves as the key
    /// </summary>
    [ConfigurationProperty(_nameProperty, IsRequired = true)]
    public string Name
    {
      get
      {
        return (string)this[_nameProperty];
      }
    }

    [ConfigurationProperty(_enabledProperty, DefaultValue = true, IsRequired = false)]
    public bool Enabled
    {
      get
      {
        return (bool)this[_enabledProperty];
      }
    }

    private const string _nameProperty = "name";

    private const string _enabledProperty = "enabled";
  }
}