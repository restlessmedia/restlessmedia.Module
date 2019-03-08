using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace restlessmedia.Module.UnitTest
{
  [TestClass]
  public class PagingTests
  {
    [TestMethod]
    public void TestPagingHasRecordCountUsedWithConstructor()
    {
      int totalCount = 10;
      Paging paging = new Paging(totalCount);

      Assert.AreEqual(paging.TotalCount, totalCount);
    }

    [TestMethod]
    public void TestPagingHasValidPageCount()
    {
      int totalCount = 8;
      int maxPerPage = 4;
      Paging paging = new Paging(totalCount);

      paging.MaxPerPage = maxPerPage;

      Assert.AreEqual(paging.Pages, 2);
    }

    [TestMethod]
    public void TestPagingHasValidPageCountWhenUsingOddMaxPagesAndRecordCount()
    {
      int totalCount = 11;
      int maxPerPage = 3;
      Paging paging = new Paging(totalCount);

      paging.MaxPerPage = maxPerPage;

      Assert.AreEqual(paging.Pages, 4);
    }

    [TestMethod]
    public void TestPagingIsOnLastPage()
    {
      int totalCount = 10;
      int maxPerPage = 5;
      Paging paging = new Paging(totalCount);

      paging.MaxPerPage = maxPerPage;
      paging.Page = 2;

      Assert.IsTrue(paging.IsLastPage);
    }

    [TestMethod]
    public void TestPagingIsOnFirstPage()
    {
      int totalCount = 10;
      int maxPerPage = 5;
      Paging paging = new Paging(totalCount);

      paging.MaxPerPage = maxPerPage;
      paging.Page = 1;

      Assert.IsTrue(paging.IsFirstPage);
    }

    [TestMethod]
    public void TestPagingIsOnFirstPageByDefault()
    {
      Paging paging = new Paging();
      Assert.IsTrue(paging.IsFirstPage);
    }
  }
}