namespace restlessmedia.Module.Security
{
  public interface IRole : IRoleInfo
  {
    string Description { get; set; }

    string[] Users { get; set; }
  }
}