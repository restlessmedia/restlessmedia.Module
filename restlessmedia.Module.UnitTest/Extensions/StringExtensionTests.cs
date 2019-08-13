using restlessmedia.Module.Extensions;
using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest.Extensions
{
  public class StringExtensionTests
  {
    [Fact]
    public void ReplaceAll_replaces_all_strings()
    {
      "foobartest".ReplaceAll(new[] { "foo", "bar" }, string.Empty).MustBe("test");
    }
  }
}
