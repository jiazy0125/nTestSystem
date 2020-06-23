using System.Configuration;

namespace nTestSystem.Framework.Configurations
{
	public class SlideMenuCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new SlideMenuElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var ep = (SlideMenuElement)element;
			return ep.MenuName;
		}


	}
}
