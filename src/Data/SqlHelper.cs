using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restlessmedia.Module.Data
{
  public static class SqlHelper
  {
    public static string ToSqlLike(this string value)
    {
      return ToSqlLike(value, SqlLikeMatch.Both);
    }

    public static string ToSqlLike(this string value, SqlLikeMatch match)
    {
      if (string.IsNullOrEmpty(value))
      {
        return value;
      }

      switch (match)
      {
        case SqlLikeMatch.Left:
          return string.Concat("%", value);
        case SqlLikeMatch.Right:
          return string.Concat(value, "%");
        default:
        case SqlLikeMatch.Both:
          return string.Concat("%", value, "%");
      }
    }
  }
}
