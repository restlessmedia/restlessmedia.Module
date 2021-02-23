using System.Collections.Generic;

namespace restlessmedia.Module
{
  public class EnumerableHelper
  {
    public static IEnumerable<int> Range(int from, int to, int step)
    {
      for (int current = from; current < to; current += step)
      {
        yield return current;
      }
    }
  }
}