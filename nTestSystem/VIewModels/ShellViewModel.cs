using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;

namespace nTestSystem
{
	class ShellViewModel:BindableBase
	{
		public string Label1 { get; set; } = "Test1";

		public ShellViewModel(IRegionManager regionManager)
		{

			var aa = regionManager.Regions;
		}
	}
}
