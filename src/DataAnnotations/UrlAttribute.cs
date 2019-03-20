using System.ComponentModel.DataAnnotations;

namespace restlessmedia.Module.DataAnnotations
{
  public class UrlAttribute : RegularExpressionAttribute
  {
    public UrlAttribute()
      : base(_pattern)
    {
      ErrorMessage = "The url you have provided is invalid";
    }

    /// <summary>
    /// http://www.geekpedia.com/KB65_How-to-validate-an-URL-using-RegEx-in-Csharp.html
    /// </summary>
    private const string _pattern = "^(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?";
  }
}
