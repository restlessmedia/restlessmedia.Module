using restlessmedia.Module.Configuration;
using SqlBuilder.DataServices;
using System.Data;

namespace restlessmedia.Module
{
  public class ModuleConnectionFactory : ConnectionFactory
  {
    public ModuleConnectionFactory(IDatabaseSettings databaseSettings)
      : base(databaseSettings.ConnectionString.ConnectionString) { }

    public override IDbTransaction CreateTransaction(bool open = true)
    {
      return base.CreateTransaction(open);
    }
  }
}