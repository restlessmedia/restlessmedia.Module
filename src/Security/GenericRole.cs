using System;

namespace restlessmedia.Module.Security
{
  [Serializable]
  public class GenericRole : GenericRoleInfo, IRole
  {
    public GenericRole()
      : base() { }

    public GenericRole(int roleId, string name, string description, bool isSystem)
      : base(roleId, name) { }

    public GenericRole(int roleId, string name, params Activity[] activities)
      : base(roleId, name) { }

    public GenericRole(int roleId, string name)
      : base(name) { }

    public GenericRole(string name)
      : base(name) { }

    public string Description { get; set; }

    /// <summary>
    /// Returns a list of usernames currently within this role
    /// </summary>
    public string[] Users { get; set; }
  }
}