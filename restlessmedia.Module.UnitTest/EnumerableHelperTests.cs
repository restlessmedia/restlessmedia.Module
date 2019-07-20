using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class EnumerableHelperTests
  {
    [Fact]
    public void Range_returns_number_range()
    {
      EnumerableHelper.Range(1, 10, 1).MustContain(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
      EnumerableHelper.Range(1, 10, 5).MustContain(1, 6);
      EnumerableHelper.Range(2, 10, 2).MustContain(2, 4, 6, 8, 10);
    }
  }
}
