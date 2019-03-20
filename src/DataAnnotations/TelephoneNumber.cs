using System.ComponentModel.DataAnnotations;

namespace restlessmedia.Module.DataAnnotations
{
  public class TelephoneNumberAttribute : RegularExpressionAttribute
  {
    public TelephoneNumberAttribute(string message = "Not a valid telephone number")
      : base(_pattern)
    {
      ErrorMessage = message;
    }

    private const string _pattern = ValidationHelper.TelephoneNumberPattern;
  }
}