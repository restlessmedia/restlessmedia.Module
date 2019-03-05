using Microsoft.SqlServer.Server;
using System;
using System.Data;

namespace restlessmedia.Module.Data.Sql
{
  public class MetaData : Attribute
  {
    public MetaData(string name, SqlDbType dbType)
    {
      _metaData = new SqlMetaData(name, dbType);
    }

    public MetaData(string name, SqlDbType dbType, long maxLength)
    {
      _metaData = new SqlMetaData(name, dbType, maxLength);
    }

    public string Name
    {
      get
      {
        return _metaData.Name;
      }
    }

    public DbType DbType
    {
      get
      {
        return _metaData.DbType;
      }
    }

    public long MaxLength
    {
      get
      {
        return _metaData.MaxLength;
      }
    }

    public SqlMetaData ToSqlMetaData()
    {
      return _metaData;
    }

    private readonly SqlMetaData _metaData;
  }
}