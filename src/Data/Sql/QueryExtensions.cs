using restlessmedia.Module.Extensions;
using System;
using System.Linq.Expressions;

namespace restlessmedia.Module.Data.Sql
{
  public static class QueryExtensions
  {
    public static string Like<T>(this T query, Expression<Func<T, string>> expression)
      where T : Query
    {
      return Like(query, expression, LikeMatch.Both);
    }

    public static string LikeRight<T>(this T query, Expression<Func<T, string>> expression)
      where T : Query
    {
      return Like(query, expression, LikeMatch.Right);
    }

    public static string LikeLeft<T>(this T query, Expression<Func<T, string>> expression)
      where T : Query
    {
      return Like(query, expression, LikeMatch.Left);
    }

    private static string Like<T>(T query, Expression<Func<T, string>> expression, LikeMatch likeMatch)
     where T : Query
    {
      string value = expression.GetValue(query);

      if (string.IsNullOrWhiteSpace(value))
      {
        return value;
      }

      switch (likeMatch)
      {
        case LikeMatch.Left:
          {
            return string.Concat("%", value);
          }
        case LikeMatch.Right:
          {
            return string.Concat(value, "%");
          }
        case LikeMatch.Both:
        default:
          {
            return string.Concat("%", value, "%");
          }
      }
    }

    private enum LikeMatch
    {
      Left,
      Right,
      Both,
    }
  }
}