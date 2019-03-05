namespace restlessmedia.Module.Data.Sql
{
  public class UDTInt : UDTBase
  {
    public UDTInt(int value)
    {
      Value = value;
    }

    [MetaData("Value", System.Data.SqlDbType.Int)]
    public int Value
    {
      get
      {
        return DataRecord.GetInt32(0);
      }
      private set
      {
        DataRecord.SetValue(0, value);
      }
    }

    public const string TypeName = "UDTInt";
  }
}