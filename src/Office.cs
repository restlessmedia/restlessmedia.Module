using System.Collections.Generic;
using System.Linq;

namespace restlessmedia.Module
{
  public class Office<TBranch> : Site
    where TBranch : Branch
  {
    public virtual string Name { get; set; }

    public virtual IList<TBranch> Branches
    {
      get
      {
        return _branches;
      }
    }

    public IList<string> Notes
    {
      get
      {
        return _notes = _notes ?? new List<string>();
      }
      set
      {
        _notes = value;
      }
    }

    public virtual void AddBranch(TBranch branch)
    {
      _branches = _branches ?? new List<TBranch>();
      _branches.Add(branch);
    }

    public string GetContactNumbers()
    {
      return string.Join(", ", Branches.Select(x => x.ContactNumber).Distinct());
    }

    private IList<string> _notes;

    private IList<TBranch> _branches;
  }

  public class Office : Office<Branch>
  {
    public virtual Branch NewBranch()
    {
      Branch branch = new Branch(this);
      AddBranch(branch);
      return branch;
    }

    public override void AddBranch(Branch branch)
    {
      branch.Office = this;
      base.AddBranch(branch);
    }
  }
}