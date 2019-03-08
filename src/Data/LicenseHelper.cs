using Dapper;
using restlessmedia.Module.Configuration;
using System;
using System.Data;
using System.Linq;

namespace restlessmedia.Module.Data
{
  public static class LicenseHelper
  {
    public static int GetLicenseId(IDbConnection connection, ILicenseSettings licenseSettings)
    {
      return GetLicenseId(connection, licenseSettings.LicenseKey);
    }

    public static int GetLicenseId(IDbConnection connection, Guid licenseKey)
    {
      if (!_licenseId.HasValue)
      {
        lock (_licenseIdLock)
        {
          if (!_licenseId.HasValue)
          {
            _licenseId = connection.Query<int?>("select LicenseId from TLicense where LicenseKey = @licenseKey", new { licenseKey }).SingleOrDefault();

            if (!_licenseId.HasValue)
            {
              throw new LicenseException("Unable to obtain license id");
            }
          }
        }
      }

      return _licenseId.Value;
    }

    public static void SetContext(IDbConnection connection, ILicenseSettings licenseSettings)
    {
      const string sql = "dbo.SPSetLicenseContext @licenseId";
      int licenseId = GetLicenseId(connection, licenseSettings.LicenseKey);
      connection.Execute(sql, new { licenseId });
    }

    private static int? _licenseId = null;

    private static readonly object _licenseIdLock = new object();
  }
}