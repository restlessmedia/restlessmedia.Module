using restlessmedia.Test;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class LinqExtensionsTests
  {
    [Fact]
    public void Page_tests()
    {
      IEnumerable<TestClass> testClasses = Enumerable.Repeat(new TestClass(), 15);

      LinqExtensions.Page(testClasses, 1, 10).Count().MustBe(10);
      LinqExtensions.Page(testClasses, 1, 5).Count().MustBe(5);
      LinqExtensions.Page(testClasses, 3, 10).Count().MustBe(0);
      LinqExtensions.Page(testClasses, 2, 10).Count().MustBe(5);
    }

    private class TestClass { }
  }
}