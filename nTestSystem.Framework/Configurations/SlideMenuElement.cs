using System.Configuration;
using System.Windows.Media;

namespace nTestSystem.Framework.Configurations
{
	public class SlideMenuElement: ConfigurationElement
    {
        [ConfigurationProperty("parent", IsRequired = false)]
        public string ParentExpander
        {
            get { return (string)base["parent"]; }
            set { base["parent"] = value; }
        }
        [ConfigurationProperty("menuName", IsRequired = true)]
        public string MenuName
        {
            get { return (string)base["menuName"]; }
            set { base["menuName"] = value; }
        }

        [ConfigurationProperty("groupName", IsRequired = false)]
        public string GroupName
        {
            get { return (string)base["groupName"]; }
            set { base["groupName"] = value; }
        }
        [ConfigurationProperty("viewName", IsRequired = true)]
        public string ViewName
        {
            get { return (string)base["viewName"]; }
            set { base["viewName"] = value; }
        }
        [ConfigurationProperty("icon", IsRequired = false)]
        public Geometry Icon
        {
            get { return (Geometry)base["icon"]; }
            set { base["icon"] = value; }

        }

    }
}
