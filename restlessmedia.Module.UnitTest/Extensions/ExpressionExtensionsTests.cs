using restlessmedia.Module.Extensions;
using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class ExpressionExtensionsTests
  {
    [Fact]
    public void GetMemberInfo_finds_member()
    {
      ExpressionExtensions.GetMemberInfo<TestClass, string>(x => x.TestProp).Name.MustBe(nameof(TestClass.TestProp));
    }

    [Fact]
    public void GetValue_returns_value()
    {
      TestClass testClass = new TestClass
      {
        TestProp = "fooBar",
      };
      ExpressionExtensions.GetValue<TestClass, string>(x => x.TestProp, testClass).MustBe("fooBar");
    }

    private class TestClass
    {
      public string TestProp { get; set; }
    }
  }
}
