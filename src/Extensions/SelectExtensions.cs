using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using System.Data;

namespace SqlBuilder
{
  public static class SelectExtensions
  {
    public static void WithLicenseId(this Select select, IDbConnection connection, ILicenseSettings licenseSettings)
    {
      select.Where(_licenseColumnName, LicenseHelper.GetLicenseId(connection, licenseSettings));
    }

    public static void WithLicenseId(this Select select, IDataContext dataContext)
    {
      using (IDbConnection connection = dataContext.ConnectionFactory.CreateConnection(true))
      {
        WithLicenseId(select, connection, dataContext.LicenseSettings);
      }
    }

    private const string _licenseColumnName = "LicenseId";
  }
}