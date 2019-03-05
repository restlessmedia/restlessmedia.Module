using restlessmedia.Module.Configuration;
using SqlBuilder.DataServices;

namespace restlessmedia.Module.Data
{
  public interface IDataContext
  {
    IDatabaseSettings DatabaseSettings { get; }

    ILicenseSettings LicenseSettings { get; }

    IConnectionFactory ConnectionFactory { get; }

    IRetry Retry { get; }
  }
}