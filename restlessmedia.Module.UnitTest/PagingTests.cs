using restlessmedia.Test;
using Xunit;

namespace restlessmedia.Module.UnitTest
{
  public class PagingTests
  {
    [Fact]
    public void TestPagingHasRecordCountUsedWithConstructor()
    {
      int totalCount = 10;
      Paging paging = new Paging(totalCount);

      paging.TotalCount.MustBe(totalCount);
    }

    [Fact]
    public void TestPagingHasValidPageCount()
    {
      int totalCount = 8;
      int maxPerPage = 4;
      Paging paging = new Paging(totalCount)
      {
        MaxPerPage = maxPerPage
      };

      paging.Pages.MustBe(2);
    }

    [Fact]
    public void TestPagingHasValidPageCountWhenUsingOddMaxPagesAndRecordCount()
    {
      int totalCount = 11;
      int maxPerPage = 3;
      Paging paging = new Paging(totalCount)
      {
        MaxPerPage = maxPerPage
      };

      paging.Pages.MustBe(4);
    }

    [Fact]
    public void TestPagingIsOnLastPage()
    {
      int totalCount = 10;
      int maxPerPage = 5;
      Paging paging = new Paging(totalCount)
      {
        MaxPerPage = maxPerPage,
        Page = 2
      };

      paging.IsLastPage.MustBeTrue();
    }

    [Fact]
    public void TestPagingIsOnFirstPage()
    {
      int totalCount = 10;
      int maxPerPage = 5;
      Paging paging = new Paging(totalCount)
      {
        MaxPerPage = maxPerPage,
        Page = 1
      };

      paging.IsFirstPage.MustBeTrue();
    }

    [Fact]
    public void TestPagingIsOnFirstPageByDefault()
    {
      Paging paging = new Paging();
      paging.IsFirstPage.MustBeTrue();
    }
  }
}