using restlessmedia.Module.Data;
using System;

namespace restlessmedia.Module.Security.Data
{
  public interface ISecurityDataProvider : IProfileDataProvider, IDataProvider
  {
    Guid CreateAccess(string applicationName, string email, DateTime validUntil, SecurityAccessType accessType);

    Guid CreateAccess(string applicationName, DateTime validUntil, SecurityAccessType accessType, Guid? key);

    SecurityAccess CheckAccess(Guid accessKey);

    ModelCollection<IRole> ListRoles(string username = null);

    bool IsUserInRole(string username, int roleId);

    bool IsUserInRole(string username, string roleName);

    bool RoleExists(string name);

    void RemoveUsersFromRoles(string[] usernames, string[] roleNames);

    bool Can(string username, string activity, ActivityAccess access);

    void CreateUser(string applicationName, string username, string password, string email, int[] roles);

    /// <summary>
    /// Sets all the roles for the specified user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="roles"></param>
    void SaveUserRoles(string username, int[] roles);

    /// <summary>
    /// Adds the role to an existing user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="role"></param>
    void AddUserToRole(string username, int role);

    IRole ReadRole(int roleId);

    T ReadRole<T>(int roleId) where T : IRole;

    TRole ReadRole<TRole, TActivity>(int roleId)
      where TRole : IRole
      where TActivity : Activity;

    void SaveRole(IRole role);

    void DeleteRole(int roleId);

    Auth ReadAuth(AuthServiceType type, string username);

    void LockAccount(string applicationName, string username);
  }
}