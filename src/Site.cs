namespace restlessmedia.Module
{
  public class Site
  {
    public Site()
    {
      Active = true;
    }

    public Marker[] Markers { get; set; }

    public bool Active { get; set; }
  }
}