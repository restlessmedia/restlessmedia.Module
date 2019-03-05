using System;
using System.Collections.Generic;

namespace restlessmedia.Module.Security
{
  [Serializable]
  public class GenericRoleInfo : IRoleInfo
  {
    public GenericRoleInfo() { }

    public GenericRoleInfo(int roleId, string name, bool isSystem, params Activity[] activities)
      : this(name)
    {
      RoleId = roleId;
      IsSystem = isSystem;
      Activities = activities;
    }

    public GenericRoleInfo(int roleId, string name, params Activity[] activities)
      : this(roleId, name, false, activities) { }

    public GenericRoleInfo(string name)
    {
      Name = name;
    }

    public int? RoleId { get; set; }

    /// <summary>
    /// If true, this is a system role and cannot be updated/deleted
    /// </summary>
    public bool IsSystem { get; set; }

    public virtual string Name { get; set; }

    public IList<Activity> Activities
    {
      get
      {
        return _activities = _activities ?? new List<Activity>();
      }
      set
      {
        _activities = value;
      }
    }

    private IList<Activity> _activities = null;
  }
}