using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

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

        [ConfigurationProperty("regionName", IsRequired = true)]
        public string RegionName
        {
            get { return (string)base["regionName"]; }
            set { base["regionName"] = value; }
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
