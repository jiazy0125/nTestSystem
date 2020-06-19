using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace nTestSystem
{
	class ShellViewModel:BindableBase
	{
		private readonly IRegionManager _regionManager;
		public string Label1 { get; set; } = "Test1";

		public DelegateCommand<string> Load { get; set; }
		public ShellViewModel(IRegionManager regionManager)
		{
			_regionManager = regionManager;
			Load = new DelegateCommand<string>(Navigate);
		}

		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				_regionManager.RequestNavigate("ItemRegion", navigatePath);
		}
	}
}
