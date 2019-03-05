using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using restlessmedia.Module.Security;
using System.Configuration;
using System.Linq;

namespace restlessmedia.Module.Configuration
{
  internal class RoleSettings : SerializableConfigurationSection, IRoleSettings
  {
    public Activity[] ActivityCollection
    {
      get
      {
        return SystemActivities.Union(Activities.Cast<ActivityItem>().Select(x => new Activity(x.Name, x.Advanced))).ToArray();
      }
    }

    [ConfigurationProperty(_activitiesProperty, IsRequired = false)]
    [ConfigurationCollection(typeof(ActivityItemCollection), AddItemName = "add")]
    private ActivityItemCollection Activities
    {
      get { return (ActivityItemCollection)this[_activitiesProperty]; }
    }

    private static Activity[] SystemActivities = new Activity[3]
    {
      new Activity("Admin", false, isSystem: true ),
      new Activity("User", true, isSystem: true ),
      new Activity("Role", true, isSystem: true )
    };

    private const string _activitiesProperty = "activities";
  }
}