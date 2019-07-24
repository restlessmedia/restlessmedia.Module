using System;

namespace restlessmedia.Module
{
  /// <summary>
  /// Compares two enums and provides methods to query both.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class EnumComparer<T>
    where T : struct
  {
    public EnumComparer(T oldFlags, T newFlags)
    {
      _oldValue = Convert.ToInt32(oldFlags);
      _newValue = Convert.ToInt32(newFlags);
    }

    /// <summary>
    /// If true, the flag was not set but now is
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public bool IsSet(T flag)
    {
      return !SetInOld(flag) && SetInNew(flag);
    }

    /// <summary>
    /// If true, the flag was set but now is not
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public bool IsUnset(T flag)
    {
      return SetInOld(flag) && !SetInNew(flag);
    }

    /// <summary>
    /// If true, the flag was set in the oldFlags value
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public bool SetInOld(T flag)
    {
      int flagValue = Convert.ToInt32(flag);
      return (_oldValue & flagValue) == 0;
    }

    /// <summary>
    /// If true, the flag was set in the newFlags value
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public bool SetInNew(T flag)
    {
      int flagValue = Convert.ToInt32(flag);
      return (_newValue & flagValue) == 0;
    }

    private readonly int _oldValue;

    private readonly int _newValue;
  }
}