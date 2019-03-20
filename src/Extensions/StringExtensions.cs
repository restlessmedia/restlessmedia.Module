using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace restlessmedia.Module.Extensions
{
  public static class StringExtension
  {
    /// <summary>
    /// Returns a decimal as currency formatted string (with culture specific currency sign)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToCurrency(this decimal value)
    {
      return ToCurrency(value, false);
    }

    /// <summary>
    /// Returns a decimal as currency formatted string (with culture specific currency sign). If the decimal is null, a hyphen "-" is displayed.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToCurrency(this decimal? value, bool excludePence = false)
    {
      return value.HasValue ? ToCurrency(value.Value, excludePence) : "-";
    }

    /// <summary>
    /// Returns a decimal as currency formatted string (with culture specific currency sign).
    /// </summary>
    /// <param name="value"></param>
    /// <param name="excludePence"></param>
    /// <returns></returns>
    public static string ToCurrency(this decimal value, bool excludePence = false)
    {
      const string format = "{0:c0}";
      const string formatWithPence = "{0:c}";
      return string.Format(excludePence ? format : formatWithPence, value);
    }

    /// <summary>
    /// Attempts to parse a string to the specified type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool TryParse<T>(this string value, out T defaultValue)
    {
      TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
      defaultValue = default(T);
      bool success = true;

      if (converter != null)
      {
        try
        {
          defaultValue = (T)converter.ConvertFromString(value);
        }
        catch
        {
          success = false;
        }
      }
      return success;
    }

    /// <summary>
    /// Converts a string from base64
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FromBase64(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      byte[] bytes;

      try
      {
        bytes = Convert.FromBase64String(value);
      }
      catch
      {
        return null;
      }

      return Encoding.ASCII.GetString(bytes);
    }

    /// <summary>
    /// Converts string to base 64
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBase64(this string value)
    {
      return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
    }

    public static bool Contains(this string value, string toCheck, bool ignoreCase)
    {
      return Contains(value, toCheck, StringComparison.OrdinalIgnoreCase);
    }

    public static bool Contains(this string value, string toCheck, StringComparison comp)
    {
      return value.IndexOf(toCheck, comp) >= 0;
    }

    /// <summary>
    /// Truncates and preserves words if a string if it exceeds the length
    /// </summary>
    /// <param name="value"></param>
    /// <param name="length"></param>
    /// <param name="preserveWords"></param>
    /// <param name="append"></param>
    /// <returns></returns>
    public static string Truncate(this string value, int length, bool preserveWords = true, string append = "...")
    {
      if (string.IsNullOrEmpty(value) || value.Length < length)
      {
        return value;
      }

      if (preserveWords)
      {
        const string space = " ";
        int nextSpace = value.LastIndexOf(space, length);
        return string.Concat(value.Substring(0, (nextSpace > 0) ? nextSpace : length), append);
      }
      else
      {
        return value.Substring(0, length);
      }
    }

    public static string TrimComma(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      const char comma = ',';
      return value.Trim(new char[1] { comma });
    }

    /// <summary>
    /// Makes a string unique. Useful for having unique filenames.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string MakeUnique(this string value)
    {
      return string.IsNullOrEmpty(value) ? value : string.Concat(value, DateTime.Now.Ticks);
    }

    public static string Right(this string value, int length)
    {
      if (string.IsNullOrEmpty(value) || value.Length <= length)
      {
        return value;
      }

      return value.Substring(value.Length - length);
    }

		/// <summary>
		/// Replaces any invalid characters unsupported for filenames and returns a legal filename.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToValidFileName(this string value, string replaceWith = "-")
		{
			if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      return string.Join(replaceWith, value.Split(Path.GetInvalidFileNameChars()));
		}

		/// <summary>
		/// Gets a byte array from string.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte[] GetBytes(this string value)
		{
			byte[] bytes = new byte[value.Length * sizeof(char)];
			Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

    /// <summary>
    /// Appends and makes a system path using the given prefix and subsequent parts
    /// </summary>
    /// <param name="value"></param>
    /// <param name="parts"></param>
    /// <returns></returns>
    public static string MakePath(this string value, params string[] parts)
    {
      if (string.IsNullOrEmpty(value) || parts == null || parts.Length == 0)
      {
        return value;
      }

      const string pattern = @"\\\\+|//+";
      const string backslash = @"\";

      // replace extra forward or backslashes
      return Regex.Replace(string.Concat(value, backslash, string.Join(backslash, parts)), pattern, backslash);
    }

    public static string Pad(this string value, string padChar, int totalWidth = 1)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      return string.Concat(value.StartsWith(padChar) ? null : padChar, value, value.EndsWith(padChar) ? null : padChar);
    }

    /// <summary>
    /// Removes any non-digit values from the string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int? ToNumber(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return null;
      }

      const string pattern = "[^0-9]*";
      string strippedValue = Regex.Replace(value, pattern, string.Empty);
      int parseResult;
      return int.TryParse(strippedValue, out parseResult) ? parseResult : (int?)null;
    }

    public static string Unquote(this string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return value;
      }

      const string quote = "\"";

      if (value.StartsWith(quote, StringComparison.Ordinal) && value.EndsWith(quote, StringComparison.Ordinal) && value.Length > 1)
      {
        return value.Substring(1, value.Length - 2);
      }

      return value;
    }

    public static string StripInvalidFileNameChars(this string fileName, string replaceWith = null)
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        return fileName;
      }

      return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), replaceWith ?? string.Empty));
    }

    /// <summary>
    /// Fixes up duplicate commas
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string FixDuplicateCommas(this string value)
    {
      if(string.IsNullOrWhiteSpace(value))
      {
        return value;
      }

      const string pattern = ",[\\s|,]*";
      const string replaceWith = ", ";

      return Regex.Replace(value, pattern, replaceWith, RegexOptions.Multiline);
    }

    public static string Remove(this string value, string patternToRemove)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return value;
      }

      return Regex.Replace(value, patternToRemove, string.Empty);
    }

    public static string ToCamelCase(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      if (value.Length == 1)
      {
        return value.ToLowerInvariant();
      }

      return string.Concat(Char.ToLowerInvariant(value[0]), value.Substring(1));
    }
  }
}