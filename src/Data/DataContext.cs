using restlessmedia.Module.Configuration;
using SqlBuilder.DataServices;
using System;

namespace restlessmedia.Module.Data
{
  internal class DataContext : IDataContext
  {
    public DataContext(IConnectionFactory connectionFactory, IRetry retry, IDatabaseSettings databaseSettings, ILicenseSettings licenseSettings)
    {
      ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
      Retry = retry ?? throw new ArgumentNullException(nameof(retry));
      DatabaseSettings = databaseSettings ?? throw new ArgumentNullException(nameof(databaseSettings));
      LicenseSettings = licenseSettings ?? throw new ArgumentNullException(nameof(licenseSettings));
    }

    public IDatabaseSettings DatabaseSettings { get; private set; }

    public ILicenseSettings LicenseSettings { get; private set; }

    public IConnectionFactory ConnectionFactory { get; private set; }

    public IRetry Retry { get; private set; }
  }
}