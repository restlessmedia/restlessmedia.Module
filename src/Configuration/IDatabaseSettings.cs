using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public interface IDatabaseSettings
  {
    string DefaultConnection { get; }

    ConnectionStringSettings ConnectionString { get; }
  }
}