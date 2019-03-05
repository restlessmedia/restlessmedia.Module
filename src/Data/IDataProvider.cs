namespace restlessmedia.Module.Data
{
  public interface IDataProvider
  {
    IDataContext DataContext { get; }
  }
}