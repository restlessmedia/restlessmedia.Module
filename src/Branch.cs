namespace restlessmedia.Module
{
  public class Branch<TOffice> : IBranch
    where TOffice : Office
  {
    public Branch() { }

    public Branch(TOffice office)
    {
      Office = office;
    }

    public string Name { get; set; }

    public int BranchGuid { get; set; }

    public int BranchId { get; set; }

    public byte Type { get; set; }

    public string ContactNumber { get; set; }

    public string ContactEmail { get; set; }

    public TOffice Office { get; internal set; }

    public string Reference { get; set; }
  }

  public class Branch : Branch<Office>
  {
    public Branch() { }

    public Branch(Office office)
      : base(office) { }
  }
}