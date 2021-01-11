using restlessmedia.Module.Configuration;
using SqlBuilder.DataServices;

namespace restlessmedia.Module
{
  public class ModuleConnectionFactory : ConnectionFactory
  {
    public ModuleConnectionFactory(IDatabaseSettings databaseSettings)
      : base(databaseSettings.ConnectionString.ConnectionString) { }
  }
}