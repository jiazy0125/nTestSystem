using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace nTestSystem.Class
{
	public partial class RegionManage
	{
		public static string ShellRegion = "ShellRegion";

		public static string TitleRegion = "TitleRegion";

		public static string SubItemRegion = "SubItemRegion";



		public static string SlideMenuRegion1 = ConfigurationManager.AppSettings["SlideMenuRegion1"]; 
		public static string SlideMenuRegion2 = ConfigurationManager.AppSettings["SlideMenuRegion2"]; 
		public static string SlideMenuRegion3 = ConfigurationManager.AppSettings["SlideMenuRegion3"]; 

	}
}
