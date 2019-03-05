using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace restlessmedia.Module.Address
{
  public static class IAddressExtensions
  {
    /// <summary>
    /// Returns the address firstline i.e. 146 Somewhere Street
    /// </summary>
    public static string FirstLine(this IAddress address)
    {
      const string space = " ";
      return string.Concat(address.NameNumber, space, address.Address01).Trim();
    }

    public static string ToStringUrlEncoded(this IAddress address)
    {
      return HttpUtility.UrlEncode(address.ToString());
    }

    public static string ToString(this IAddress address, string separator)
    {
      return string.Join(separator, new string[7] { address.KnownAs, FirstLine(address), address.Address02, address.Town, address.City, address.PostCode, address.CountryCode }.Where(p => !string.IsNullOrEmpty(p)));
    }

    /// <summary>
    /// Returns a format suitable for passing to for a map query
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToMapQuery(this IAddress address, string separator = ",")
    {
      return string.Join(separator, new string[6] { address.Address01, address.Address02, address.Town, address.City, address.PostCode, address.CountryCode }.Where(p => !string.IsNullOrEmpty(p)));
    }

    public static string PostCodeArea(this IAddress address)
    {
      string firstPart = PostCodeParts(address)[0];

      if (string.IsNullOrEmpty(firstPart))
      {
        return firstPart;
      }

      const string pattern = "[^a-zA-Z]";

      return Regex.Replace(firstPart, pattern, string.Empty);
    }

    /// <summary>
    /// Returns the 2 post code parts of the post code.  If the post code is null this will still return a 2 length string array but the parts will be empty
    /// </summary>
    public static string[] PostCodeParts(this IAddress address)
    {
      if (string.IsNullOrEmpty(address.PostCode))
      {
        return new string[2] { string.Empty, string.Empty };
      }
      else
      {
        const string space = " ";
        return address.PostCode.Split(new string[] { space }, StringSplitOptions.None);
      }
    }

    /// <summary>
    /// Returns a vague version of the address omitting house number etc
    /// </summary>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToVague(this IAddress address, string separator = ", ")
    {
      return string.Join(separator, new string[4] { address.KnownAs, address.Address01, address.Address02, address.PostCode }.Where(p => !string.IsNullOrEmpty(p)));
    }

    public static string UrlEncodedPostCode(this IAddress address)
    {
        return HttpUtility.UrlEncode(address.PostCode);
    }

    public static string PostCodeFirstPart(this IAddress address)
    {
      string[] parts = PostCodeParts(address);
      return parts.FirstOrDefault();
    }
  }
}