using restlessmedia.Module.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace restlessmedia.Module.Security.Data.Sql
{
  public class SaveUserRolesParameters : ParametersBase
  {
    public SaveUserRolesParameters(string username, IEnumerable<int> roles)
    {
      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      _username = username;
      _roles = roles;
    }

    protected override void AddParameters(SqlCommand command)
    {
      Add("@username", _username);
      Add("@roles", UDTInt.TypeName, _roles.Select(x => new UDTInt(x)));
    }

    private readonly string _username;

    private readonly IEnumerable<int> _roles;
  }
}