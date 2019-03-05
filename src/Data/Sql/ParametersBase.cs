using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace restlessmedia.Module.Data.Sql
{
  public abstract class ParametersBase : SqlMapper.IDynamicParameters
  {
    public void AddParameters(IDbCommand command, Dapper.SqlMapper.Identity identity)
    {
      SqlCommand sqlCommand = (SqlCommand)command;
      Parameters = sqlCommand.Parameters;
      AddParameters(sqlCommand);
    }

    public SqlParameterCollection Parameters { get; private set; }

    public SqlParameter Add(string name, object value = null, SqlDbType? dbType = null, ParameterDirection? direction = null)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      SqlParameter parameter = dbType.HasValue ? new SqlParameter(name, dbType.Value) { Value = value } : new SqlParameter(name, value);
      
      if (direction.HasValue)
      {
        parameter.Direction = direction.Value;
      }

      Parameters.Add(parameter);

      return parameter;
    }

    public SqlParameter Add(string name, string typeName, object value, ParameterDirection? direction = null)
    {
      if (string.IsNullOrEmpty(typeName))
      {
        throw new ArgumentNullException(nameof(typeName));
      }

      SqlParameter parameter = Add(name, value, SqlDbType.Structured, direction);
      parameter.TypeName = typeName;
      return parameter;
    }

    public SqlParameter Add(string name, string typeName, IEnumerable<UDTBase> value, ParameterDirection? direction = null)
    {
      return Add(name, typeName, value.Select(x => x.DataRecord), direction);
    }

    protected abstract void AddParameters(SqlCommand command);
  }
}