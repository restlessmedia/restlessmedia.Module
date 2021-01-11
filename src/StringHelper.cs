using System.Text.RegularExpressions;

namespace restlessmedia.Module
{
  public class StringHelper
  {
    /// <summary>
    /// Strips the xml tags revealing the inner text.
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string StripXmlTags(string html)
    {
      if (string.IsNullOrWhiteSpace(html))
      {
        return html;
      }

      const string _matchTagPattern = @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[\^'"">\s]+))?)+\s*|\s*)/?>";
      return Regex.Replace(html, _matchTagPattern, string.Empty).Trim();
    }
  }
}