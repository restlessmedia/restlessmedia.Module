using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  internal class FeatureCollection : TypedCollection<Feature>
  {
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((Feature)element).Name;
    }

    public override void Remove(Feature item)
    {
      if (BaseIndexOf(item) > 0)
      {
        BaseRemove(item.Name);
      }
    }
  }
}