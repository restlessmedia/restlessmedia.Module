using restlessmedia.Module.Extensions;
using restlessmedia.Test;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class TypeExtensionsTests
  {
    [Fact]
    public void GetMembers_finds_members_with_attribute()
    {
      TypeExtensions.GetMembers<ReadOnlyAttribute>(typeof(TestClass), x => x != null && x.IsReadOnly).Count().MustBe(1);
    }

    [Fact]
    public void New_creates_instance()
    {
      // set-up & call
      TestClass test = TypeExtensions.New<string, bool, TestClass>(typeof(TestClass), "foo", true);
      TestClass test2 = TypeExtensions.New<string, TestClass>(typeof(TestClass), "fooBar");

      // assert
      test.TestString.MustBe("foo");
      test.TestBool.MustBe(true);

      test2.TestString.MustBe("fooBar");
      test2.TestBool.MustBe(false); // false is the default value for TestBool
    }

    [Fact]
    public void IsNullable()
    {
      typeof(int?).IsNullable().MustBeTrue();
      typeof(int).IsNullable().MustBeFalse();
    }

    private class TestClass
    {
      public TestClass(string testString, bool testBool)
      {
        TestString = testString;
        TestBool = testBool;
      }

      public TestClass(string testString)
      {
        TestString = testString;
      } 

      public string TestString;

      public bool TestBool;

      [ReadOnly(true)]
      public void Foo() { }
    }
  }
}