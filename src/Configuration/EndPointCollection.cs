using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  public class EndPointCollection : TypedCollection<EndPoint>
  {
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((EndPoint)element).Host;
    }

    public override void Remove(EndPoint item)
    {
      if (BaseIndexOf(item) > 0)
      {
        BaseRemove(item.Host);
      }
    }
  }
}
