using Dapper;
using restlessmedia.Module.Address;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using SqlBuilder.DataServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace restlessmedia.Module.Security.Data.Sql
{
  /// <summary>
  /// This is public due to the membership provider and how we deal with DI for that framework feature
  /// </summary>
  public class SecuritySqlDataProvider : SqlDataProviderBase
  {
    public SecuritySqlDataProvider(IDataContext context, IRoleSettings roleSettings)
      : base(context)
    {
      _membershipUserProfileDataProvider = new ProfileSqlDataProvider(context.ConnectionFactory, context.Retry);
      _roleSettings = roleSettings ?? throw new ArgumentNullException(nameof(roleSettings));
    }

    public Guid CreateAccess(string applicationName, string email, DateTime validUntil, SecurityAccessType accessType)
    {
      DynamicParameters parameters = new DynamicParameters();

      parameters.Add("@applicationName", applicationName);
      parameters.Add("@email", email);
      parameters.Add("@validUntil", validUntil);
      parameters.Add("@accessType", accessType);

      return QueryWithTransaction<Guid>("dbo.SPCreateUserAccess", parameters).First();
    }

    public Guid CreateAccess(string applicationName, DateTime validUntil, SecurityAccessType accessType, Guid? key)
    {
      DynamicParameters parameters = new DynamicParameters();

      parameters.Add("@applicationName", applicationName);
      parameters.Add("@validUntil", validUntil);
      parameters.Add("@accessType", accessType);
      parameters.Add("@key", key);

      return QueryWithTransaction<Guid>("dbo.SPCreateAccess", parameters).First();
    }

    public void UpdateProfileAddress(string applicationName, string userName, AddressEntity address)
    {
      _membershipUserProfileDataProvider.UpdateProfileAddress(applicationName, userName, address);
    }

    public int DeleteProfiles(string[] usernames)
    {
      return _membershipUserProfileDataProvider.DeleteProfiles(usernames);
    }

    public dynamic ReadProfile(string applicationName, string userName)
    {
      return _membershipUserProfileDataProvider.ReadProfile(applicationName, userName);
    }

    public UserProfileAddress ReadProfileAddress(string applicationName, string userName, AddressType type)
    {
      return _membershipUserProfileDataProvider.ReadProfileAddress(applicationName, userName, type);
    }

    public SecurityAccess CheckAccess(Guid accessKey)
    {
      return Query<SecurityAccess>("dbo.SPCheckAccess", new { accessKey = accessKey }).FirstOrDefault();
    }

    public void UpdateProfile(string applicationName, string userName, SettingsPropertyValueCollection valueCollection)
    {
      _membershipUserProfileDataProvider.UpdateProfile(applicationName, userName, valueCollection);
    }

    public ModelCollection<IRole> ListRoles(string username = null)
    {
      List<GenericRole> roles;
      List<Activity> activities;

      using (IGridReader reader = QueryMultiple("dbo.SPListRoles", new { username }))
      {
        roles = reader.Read<GenericRole>().ToList();
        activities = reader.Read<Activity>().ToList();
      }

      roles.ForEach(r => r.Activities = activities.OrderBy(x => x.IsAdvanced).Where(a => a.RoleId == r.RoleId).ToList());

      ModelCollection<IRole> list = new ModelCollection<IRole>(roles);

      return list;
    }

    public bool IsUserInRole(string username, int roleId)
    {
      return IsUserInRole(username: username, roleId: roleId);
    }

    public bool IsUserInRole(string username, string roleName)
    {
      return IsUserInRole(username: username, roleName: roleName);
    }

    public bool RoleExists(string name)
    {
      return Query<bool>("dbo.SPRoleExists", new { roleName = name }).FirstOrDefault();
    }

    public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      ExecuteWithTransaction((transaction) =>
      {
        foreach (string username in usernames)
        {
          foreach (string roleName in roleNames)
          {
            transaction.Connection.Execute("dbo.SPUserRemoveRole", new { username = username, roleName = roleName }, transaction: transaction, commandType: CommandType.StoredProcedure);
          }
        }
      });
    }

    public bool Can(string username, string activity, ActivityAccess access)
    {
      return Query<bool>("dbo.SPActivityAccess", new { username = username, activity = activity, access = (byte)access }).FirstOrDefault();
    }

    public void CreateUser(string applicationName, string username, string password, string email, int[] roles)
    {
      const string commandName = "dbo.SPCreateUser";
      Execute(commandName, new CreateUserParameters(applicationName, username, password, email, roles));
    }

    public void SaveUserRoles(string username, params int[] roles)
    {
      const string commandName = "dbo.SPSaveUserRoles";
      Execute(commandName, new SaveUserRolesParameters(username, roles));
    }

    public void AddUserToRole(string username, int role)
    {
      Execute("dbo.SPSaveUserRole", new { username = username, roleId = role });
    }

    public TRole ReadRole<TRole, TActivity>(int roleId)
      where TRole : IRole
      where TActivity : Activity
    {
      TRole role;

      using (IGridReader reader = QueryMultiple("dbo.SPReadRole", new { roleId = roleId }))
      {
        role = reader.Read<TRole>().SingleOrDefault();
        role.Activities = reader.Read<TActivity>().ToArray();
        role.Users = reader.Read<string>().ToArray();
      }

      // we only get the activities back from the db the role has been added with.
      // here, we'll need to fill in gaps so the activities that haven't been picked still come through
      // this will also fix up the activities with information from the config that isn't stored in the db (isadvanced)
      role.Activities = role.Activities
        .Union(_roleSettings.ActivityCollection)
        .Where(x => _roleSettings.ActivityCollection.Contains(x))
        .Select(x =>
        {
          x.IsAdvanced = _roleSettings.ActivityCollection.First(y => y.Equals(x)).IsAdvanced;
          return x;
        })
        .ToArray();

      return role;
    }

    public T ReadRole<T>(int roleId)
      where T : IRole
    {
      return ReadRole<T, Activity>(roleId);
    }

    public IRole ReadRole(int roleId)
    {
      return ReadRole<GenericRole>(roleId);
    }

    public void SaveRole(IRole role)
    {
      SaveRoleParameters parameters = new SaveRoleParameters(role);
      Execute("dbo.SPSaveRole", parameters);
      role.RoleId = parameters.RoleId;
    }

    public void DeleteRole(int roleId)
    {
      Execute("dbo.SPDeleteRole", new { roleId = roleId });
    }

    public Auth ReadAuth(AuthServiceType type, string username)
    {
      Auth auth = Query<Auth>("dbo.SPReadAuth", new { username = username }).FirstOrDefault();
      auth.Type = type;
      return auth;
    }

    public void LockAccount(string applicationName, string username)
    {
      const string commandName = "dbo.SPLockUser";
      Execute(commandName, new { applicationName, username });
    }

    private bool IsUserInRole(string username = null, int? roleId = null, string roleName = null)
    {
      return Query<bool>("dbo.SPUserHasRole", new { roleId = roleId, username = username, roleName = roleName }).FirstOrDefault();
    }

    private readonly ProfileSqlDataProvider _membershipUserProfileDataProvider;

    private readonly IRoleSettings _roleSettings;
  }
}