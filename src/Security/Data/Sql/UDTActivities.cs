using restlessmedia.Module.Data.Sql;

namespace restlessmedia.Module.Security.Data
{
  public class UDTActivities : UDTBase
  {
    public UDTActivities(int roleId, string name, ActivityAccess access)
    {
      RoleId = roleId;
      Name = name;
      Access = (byte)access;
    }

    [MetaData("RoleId", System.Data.SqlDbType.Int)]
    public int RoleId
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

    [MetaData("Name", System.Data.SqlDbType.VarChar, 30)]
    public string Name
    {
      get
      {
        return DataRecord.GetString(1);
      }
      private set
      {
        DataRecord.SetValue(1, value);
      }
    }

    [MetaData("Access", System.Data.SqlDbType.TinyInt)]
    public byte Access
    {
      get
      {
        return DataRecord.GetByte(2);
      }
      private set
      {
        DataRecord.SetValue(2, value);
      }
    }

    public const string TypeName = "UDTActivities";
  }
}