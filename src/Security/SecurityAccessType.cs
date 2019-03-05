namespace restlessmedia.Module.Security
{
  public enum SecurityAccessType
    : byte
  {
    /// <summary>
    /// Not set
    /// </summary>
    None = 0,
    /// <summary>
    /// Confirmation within 1 day of users email (after email sent)
    /// </summary>
    PostRegistrationConfirm = 1,
    /// <summary>
    /// Send request for password reset
    /// </summary>
    PasswordReset = 2,
    /// <summary>
    /// Public API and internal job access
    /// </summary>
    Job = 3
  }
}