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

    private class TestClass
    {
      [ReadOnly(true)]
      public void Foo() { }
    }
  }
}