using System.Collections.Generic;

namespace restlessmedia.Module.Security
{
  public interface IRoleInfo
  {
    int? RoleId { get; set; }

    /// <summary>
    /// If true, this is a system role and cannot be updated/deleted
    /// </summary>
    bool IsSystem { get; set; }

    string Name { get; set; }

    IList<Activity> Activities { get; set; }
  }
}