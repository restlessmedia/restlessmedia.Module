using System;

namespace restlessmedia.Module.Security
{
  /// <summary>
  /// Used to model public access checks in the system based around an access key guid that can expire.
  /// </summary>
  /// <example>User registration to confirm email address, key = UserKey</example>
  public class SecurityAccess
  {
    #region Constructors

    public SecurityAccess()
    {
      Status = AccessStatus.None;
    }

    public SecurityAccess(Guid accessKey)
      : this() { }

    #endregion

    /// <summary>
    /// Used for 2-part authentication.  For user access checks, this would be the UserKey, but it could be any guid.
    /// </summary>
    public Guid Key { get; set; }

    /// <summary>
    /// Access url key supplied to validate this access
    /// </summary>
    public Guid AccessKey { get; set; }

    public DateTime ValidUntil { get; set; }

    public SecurityAccessType AccessType { get; set; }

    public AccessStatus Status
    {
      get
      {
        return StatusValue;
      }
      set
      {
        StatusValue = value;
      }
    }

    /// <summary>
    /// Used by dapper
    /// </summary>
    private AccessStatus StatusValue { get; set; }
  }
}