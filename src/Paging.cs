using System;

namespace restlessmedia.Module
{
  [Serializable]
  public class Paging
  {
    public Paging() { }

    public Paging(int totalCount)
    {
      TotalCount = totalCount;
    }

    public Paging(int totalCount, int page)
      : this(totalCount)
    {
      Page = page;
    }

    /// <summary>
    /// Maximum records per page, default is 10
    /// </summary>
    public int MaxPerPage
    {
      get
      {
        return _maxPerPage;
      }
      set
      {
        _maxPerPage = value;
        Recalculate();
      }
    }

    /// <summary>
    /// The total count of the records in the datasource
    /// </summary>
    public int TotalCount
    {
      get
      {
        return _totalCount;
      }
      set
      {
        _totalCount = value;
        Recalculate();
      }
    }

    public int Page = 1;

    public int Pages = 0;

    public bool IsLastPage
    {
      get
      {
        return Page.Equals(Pages);
      }
    }

    public bool IsFirstPage
    {
      get
      {
        return Page.Equals(1);
      }
    }

    public int NextPage
    {
      get
      {
        return IsLastPage ? Pages : Page + 1;
      }
    }

    public int PreviousPage
    {
      get
      {
        return IsFirstPage ? 1 : Page - 1;
      }
    }

    public bool CanNext
    {
      get
      {
        if (Pages < 2)
        {
          return false;
        }

        return !IsLastPage;
      }
    }

    public bool CanPrevious
    {
      get
      {
        if (Pages < 2)
        {
          return false;
        }

        return !IsFirstPage;
      }
    }

    /// <summary>
    /// Paging counts are only calculated once when the totalcount is set - this ecalculates the paging counts in case they have changed.
    /// </summary>
    private void Recalculate()
    {
      Pages = _totalCount > 0 ? (int)Math.Ceiling((decimal)_totalCount / MaxPerPage) : 0;
    }

    private int _totalCount = 0;

    private int _maxPerPage = 10;
  }
}