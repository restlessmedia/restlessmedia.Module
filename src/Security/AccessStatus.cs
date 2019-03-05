namespace restlessmedia.Module.Security
{
  public enum AccessStatus : byte
  {
    /// <summary>
    /// No access type has been set-up
    /// </summary>
    None = 0,
    /// <summary>
    /// Access has been set-up but not confirmed/triggered
    /// </summary>
    Open = 1,
    /// <summary>
    /// The access has been created but the window for triggering it has expired
    /// </summary>
    Expired = 2,
    /// <summary>
    /// The access has already been triggered in the time window
    /// </summary>
    Triggered = 3,
    /// <summary>
    /// The trigger was successfully set
    /// </summary>
    Success = 4
  }
}