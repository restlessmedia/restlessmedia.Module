using System.Configuration;

namespace restlessmedia.Module.Configuration
{
	public class ActivityItem : ConfigurationElement
	{
		[ConfigurationProperty(nameProperty, IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)this[nameProperty];
			}
		}

		/// <summary>
		/// If true, the activity uses advanced CRUD access checking, otherwise it's basic on/off access.
		/// </summary>
		[ConfigurationProperty(advancedProperty, IsRequired = false, DefaultValue = false)]
		public bool Advanced
		{
			get
			{
				return (bool)this[advancedProperty];
			}
		}

    private const string nameProperty = "name";

    private const string advancedProperty = "advanced";
	}
}