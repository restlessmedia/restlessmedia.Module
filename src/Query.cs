using System.Collections.Generic;

namespace restlessmedia.Module
{
  public class Query
  {
    public Query(int page, int maxPerPage)
    {
      Page = page;
      MaxPerPage = maxPerPage;
      Columns = new List<string>();
    }

    public Query()
      : this(1, 10) { }

    [Ignore]
    public int Page { get; set; }

    public virtual int MaxPerPage { get; set; }

    public string Filter { get; set; }

    public readonly IList<string> Columns;

    [Ignore]
    public int StartRow
    {
      get
      {
        return (Page - 1) * MaxPerPage;
      }
    }
  }
}