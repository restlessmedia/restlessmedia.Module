using System.Configuration;

namespace restlessmedia.Module.Configuration
{
  internal class ActivityItemCollection : TypedCollection<ActivityItem>
  {
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ActivityItem)element).Name;
    }

    public override void Remove(ActivityItem item)
    {
      if (BaseIndexOf(item) > 0)
      {
        BaseRemove(item.Name);
      }
    }
  }
}