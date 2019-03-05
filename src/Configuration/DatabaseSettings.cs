using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  internal class DatabaseSettings : SerializableConfigurationSection, IDatabaseSettings
  {
    [ConfigurationProperty(_defaultConnectionProperty, IsRequired = true)]
    public string DefaultConnection
    {
      get
      {
        return (string)this[_defaultConnectionProperty];
      }
    }

    public ConnectionStringSettings ConnectionString
    {
      get
      {
        ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings[DefaultConnection];

        if (connection == null)
        {
          throw new ConfigurationErrorsException($"There is no connectionString defined for '{DefaultConnection}'.  A valid connection string is required in the configuratin file with name '{DefaultConnection}'.");
        }

        return connection;
      }
    }

    private const string _defaultConnectionProperty = "defaultConnection";
  }
}
