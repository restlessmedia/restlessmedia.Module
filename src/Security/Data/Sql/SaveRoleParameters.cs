using restlessmedia.Module.Data.Sql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace restlessmedia.Module.Security.Data.Sql
{
  public class SaveRoleParameters : ParametersBase
  {
    public SaveRoleParameters(IRole role)
    {
      _role = role ?? throw new ArgumentNullException(nameof(role));
    }

    public int RoleId
    {
      get
      {
        return (int)Parameters["@roleId"].Value;
      }
    }

    protected override void AddParameters(SqlCommand command)
    {
      Add("@roleId", _role.RoleId, direction: ParameterDirection.InputOutput);
      Add("@roleName", _role.Name);
      Add("@description", _role.Description);
      Add("@activities", UDTActivities.TypeName, _role.Activities.Select(x => new UDTActivities(_role.RoleId.Value, x.Name, x.Access)));
      Add("@isSystem", _role.IsSystem);
    }

    private readonly IRole _role;
  }
}