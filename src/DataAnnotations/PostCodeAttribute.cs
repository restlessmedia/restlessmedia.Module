using System.ComponentModel.DataAnnotations;

namespace restlessmedia.Module.DataAnnotations
{
  public class PostCodeAttribute : RegularExpressionAttribute
  {
    public PostCodeAttribute(string message = "The post code you have provided is invalid")
      : base(_pattern)
    {
      ErrorMessage = message;
    }

    private const string _pattern = ValidationHelper.PostCodePattern;
  }
}