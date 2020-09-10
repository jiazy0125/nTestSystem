using System.Configuration;

namespace nTestSystem.Framework.Configurations
{
	public class SlideMenuSection: ConfigurationSection
	{

		[ConfigurationProperty("SlideMenus", IsRequired = false)]
		public SlideMenuCollection SlideMenus
		{
			get { return (SlideMenuCollection)base["SlideMenus"]; }
		}
	}
}
