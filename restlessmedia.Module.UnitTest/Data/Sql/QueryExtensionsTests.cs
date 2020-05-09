using restlessmedia.Module.Data.Sql;
using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest.Data.Sql
{
  public class QueryExtensionsTests
  {
    [Fact]
    public void Like_adds_chars()
    {
      QueryExtensions.Like(new TestQuery
      {
        PostCode = "tn2 4hp"
      }, x => x.PostCode).MustBe("%tn2 4hp%");

      QueryExtensions.Like(new TestQuery
      {
        PostCode = "tn2 4hp"
      }, x => x.PostCode).MustBe("%tn2 4hp%");
    }

    public class TestQuery : Query
    {
      public string PostCode { get; set; }
    }
  }
}
