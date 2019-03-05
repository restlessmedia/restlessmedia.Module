namespace restlessmedia.Module.Configuration
{
  public interface IFeature
  {
    string Name { get; }

    bool Enabled { get; }
  }
}