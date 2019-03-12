using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public class EndPoint : ConfigurationElement
  {
    [ConfigurationProperty(hostProperty, IsRequired = true)]
    public string Host
    {
      get
      {
        return (string)this[hostProperty];
      }
    }

    private const string hostProperty = "host";
  }
}