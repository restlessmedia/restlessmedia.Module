using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class StringHelperTests
  {
    [Fact]
    public void strips_out_xml_tags()
    {
      string html = "<strong>a test</strong>";
      StringHelper.StripXmlTags(html).MustBe("a test");
    }

    [Fact]
    public void strips_out_nested_xml_tags()
    {
      string html = "<strong>a test <span>nested</span></strong>";
      StringHelper.StripXmlTags(html).MustBe("a test nested");
    }
  }
}