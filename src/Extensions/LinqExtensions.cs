using System.Linq;

namespace System.Collections.Generic
{
  public static class LinqExtensions
  {
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int maxPerPage)
    {
      if (source == null)
      {
        return null;
      }

      if (page == 1)
      {
        return source.Take(maxPerPage);
      }

      int skip = maxPerPage * (page - 1);

      return source.Skip(skip).Take(maxPerPage);
    }
  }
}