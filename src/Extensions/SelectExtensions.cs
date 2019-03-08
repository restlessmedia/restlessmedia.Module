using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using System.Data;

namespace SqlBuilder
{
  public static class SelectExtensions
  {
    public static void WithLicenseId(this Select select, IDbConnection connection, ILicenseSettings licenseSettings)
    {
      select.Where("LicenseId", LicenseHelper.GetLicenseId(connection, licenseSettings));
    }
  }
}