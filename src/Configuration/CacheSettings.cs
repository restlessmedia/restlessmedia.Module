using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Collections.Generic;
using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public class CacheSettings : SerializableConfigurationSection, ICacheSettings
  {
    [ConfigurationProperty(_sslProperty, IsRequired = false, DefaultValue = true)]
    public bool Ssl
    {
      get
      {
        return (bool)this[_sslProperty];
      }
    }

    [ConfigurationProperty(_accessKeyProperty, IsRequired = false)]
    public string AccessKey
    {
      get
      {
        return (string)this[_accessKeyProperty];
      }
    }

    [ConfigurationProperty(_retryProperty, IsRequired = false, DefaultValue = 3)]
    public int Retry
    {
      get
      {
        return (int)this[_retryProperty];
      }
    }

    [ConfigurationProperty(_timeoutProperty, IsRequired = false, DefaultValue = 5000)]
    public int Timeout
    {
      get
      {
        return (int)this[_timeoutProperty];
      }
    }

    public IEnumerable<EndPoint> EndPointCollection
    {
      get
      {
        return EndPoints.GetAll();
      }
    }

    [ConfigurationProperty(_endPointsProperty, IsRequired = true)]
    [ConfigurationCollection(typeof(EndPointCollection), AddItemName = "add")]
    private EndPointCollection EndPoints
    {
      get
      {
        return (EndPointCollection)this[_endPointsProperty];
      }
    }

    private const string _sslProperty = "ssl";

    private const string _accessKeyProperty = "accessKey";

    private const string _endPointsProperty = "endPoints";

    private const string _retryProperty = "retry";

    private const string _timeoutProperty = "timeout";
  }
}