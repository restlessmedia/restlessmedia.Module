using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using System;
using System.Data;

namespace restlessmedia.Module
{
  public class LicensedModuleConnectionFactory : ModuleConnectionFactory
  {
    public LicensedModuleConnectionFactory(IDatabaseSettings databaseSettings, ILicenseSettings licenseSettings)
      : base(databaseSettings)
    {
      _licenseSettings = licenseSettings ?? throw new ArgumentNullException(nameof(licenseSettings));
    }

    public override IDbConnection CreateConnection(bool open = true)
    {
      IDbConnection connection = base.CreateConnection(open);

      if (open)
      {
        LicenseHelper.SetContext(connection, _licenseSettings);
      }

      return connection;
    }

    private readonly ILicenseSettings _licenseSettings;
  }
}