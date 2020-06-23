using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
