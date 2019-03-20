using System.Text.RegularExpressions;

namespace restlessmedia.Module
{
  public static class ValidationHelper
  {
    public static bool IsValidTelephoneNumber(string value)
    {
      return Regex.IsMatch(value, TelephoneNumberPattern);
    }

    public static bool IsValidPostCode(string value)
    {
      return Regex.IsMatch(value, PostCodePattern);
    }

    public const string TelephoneNumberPattern = "^[\\+0-9\\s\\-\\(\\)]+$";

    /// <remarks>From http://www.mgbrown.com/PermaLink66.aspx</remarks>
    public const string PostCodePattern = "^(([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) [0-9][A-Za-z]{2}))$";
  }
}