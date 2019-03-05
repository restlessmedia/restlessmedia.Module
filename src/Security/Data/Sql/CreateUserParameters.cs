using restlessmedia.Module.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace restlessmedia.Module.Security.Data.Sql
{
  public class CreateUserParameters : ParametersBase
  {
    public CreateUserParameters(string applicationName, string username, string password, string email, IEnumerable<int> roles)
    {
      if (string.IsNullOrEmpty(applicationName))
      {
        throw new ArgumentNullException(nameof(applicationName));
      }

      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      if (string.IsNullOrEmpty(password))
      {
        throw new ArgumentNullException(nameof(password));
      }

      if (string.IsNullOrEmpty(email))
      {
        throw new ArgumentNullException(nameof(email));
      }

      _applicationName = applicationName;
      _username = username;
      _password = password;
      _email = email;
      _roles = roles;
    }

    protected override void AddParameters(SqlCommand command)
    {
      Add("@userId", null);
      Add("@applicationName", _applicationName);
      Add("@username", _username);
      Add("@password", _password);
      Add("@email", _email);
      UDTHelper.AddParameter(command, "@roles", _roles);
    }

    private readonly string _applicationName;

    private readonly string _password;

    private readonly string _email;

    private readonly string _username;

    private readonly IEnumerable<int> _roles;
  }
}