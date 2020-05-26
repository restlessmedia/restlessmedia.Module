using System.Collections.Generic;

namespace restlessmedia.Module
{
  public class Company<TBranch>
    where TBranch : Branch
  {
    public string Name { get; set; }

    public virtual IList<TBranch> Branches
    {
      get
      {
        return _branches = _branches ?? new List<TBranch>();
      }
      set
      {
        _branches = value;
      }
    }

    private IList<TBranch> _branches;
  }
}