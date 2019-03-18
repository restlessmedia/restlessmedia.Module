using Dapper;
using System.Data;

namespace restlessmedia.Module.Data
{
  public static class DynamicParameterExtensions
  {
    public static void AddId(this DynamicParameters parameters, string name, int? value)
    {
      parameters.Add(name, value, DbType.Int32, ParameterDirection.InputOutput, 4);
    }
  }
}