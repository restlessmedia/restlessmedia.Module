using System;

namespace restlessmedia.Module.Security
{
  [Serializable]
  public class Activity : IEquatable<Activity>
	{
		public Activity() { }

    public Activity(int roleId, string name, bool isAdvanced = false, ActivityAccess access = ActivityAccess.None, bool isSystem = false)
      : this(name, isAdvanced, access, isSystem)
    {
      RoleId = roleId;
    }

		public Activity(string name, bool isAdvanced = false, ActivityAccess access = ActivityAccess.None, bool isSystem = false)
		{
			Name = name;
			Access = access;
			IsAdvanced = isAdvanced;
			IsSystem = isSystem;
		}

    /// <summary>
    /// Used to marry up an activity to a role and is rarely used.  It's nullable as it will render in editorfor output if it isn't (not sure why)
    /// </summary>
		public int? RoleId { get; set; }

		/// <summary>
		/// If true, this is a system activity and cannot be updated/deleted
		/// </summary>
		public bool IsSystem { get; set; }

		public virtual string Name { get; set; }

		public ActivityAccess Access { get; set; }

		public bool IsAdvanced { get; set; }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return obj is Activity ? Equals((Activity)obj) : base.Equals(obj);
    }

    /// <summary>
    /// 1
    /// </summary>
    public virtual bool HasBasic
    {
      get
      {
        return Access.HasFlag(ActivityAccess.Basic);
      }
      set
      {
        SetFlag(ActivityAccess.Basic, value);
      }
    }

    /// <summary>
    /// 2
    /// </summary>
    public virtual bool HasCreate
    {
      get
      {
        return Access.HasFlag(ActivityAccess.Create);
      }
      set
      {
        SetFlag(ActivityAccess.Create, value);
      }
    }

    /// <summary>
    /// 4
    /// </summary>
    public virtual bool HasRead
    {
      get
      {
        return Access.HasFlag(ActivityAccess.Read);
      }
      set
      {
        SetFlag(ActivityAccess.Read, value);
      }
    }

    /// <summary>
    /// 8
    /// </summary>
    public virtual bool HasUpdate
    {
      get
      {
        return Access.HasFlag(ActivityAccess.Update);
      }
      set
      {
        SetFlag(ActivityAccess.Update, value);
      }
    }

    /// <summary>
    /// 16
    /// </summary>
    public virtual bool HasDelete
    {
      get
      {
        return Access.HasFlag(ActivityAccess.Delete);
      }
      set
      {
        SetFlag(ActivityAccess.Delete, value);
      }
    }

    public bool Equals(Activity other)
    {
      return other.Name == Name;
    }

    /// <summary>
    /// Sets a flag on or off
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="on"></param>
    private void SetFlag(ActivityAccess flag, bool on = true)
    {
      Access = on ? Access |= flag : Access &= ~flag;
    }
  }
}