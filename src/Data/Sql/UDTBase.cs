using Microsoft.SqlServer.Server;

namespace restlessmedia.Module.Data.Sql
{
  public abstract class UDTBase
  {
    public UDTBase()
    {
      Definition = UDTHelper.GetSqlMetaData(this);
      DataRecord = new SqlDataRecord(Definition);
    }

    public readonly SqlMetaData[] Definition;

    public readonly SqlDataRecord DataRecord;
  }
}