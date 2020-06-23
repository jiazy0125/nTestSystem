using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using nTestSystem.Framework.Configurations;
using System.Collections.ObjectModel;
using nTestSystem.Models;

namespace nTestSystem
{
	class ShellViewModel:BindableBase
	{
		private readonly IRegionManager _regionManager;
		public string Label1 { get; set; } = "Test1";

		public ObservableCollection<TreeViewItemModel> SlideMenus { get; } = new ObservableCollection<TreeViewItemModel>();

		public DelegateCommand<string> Load { get; set; }
		public ShellViewModel(IRegionManager regionManager)
		{
			_regionManager = regionManager;
			Load = new DelegateCommand<string>(Navigate);
			LoadMenus();
		}

		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				_regionManager.RequestNavigate("ItemRegion", navigatePath);






		}

		private void LoadMenus()
		{
			if (ConfigurationManager.GetSection("menu-en") is SlideMenuSection config)
			{
				foreach (SlideMenuElement temp in config.SlideMenus)
				{
					SlideMenus.Add(new TreeViewItemModel() { Header=temp.MenuName, Icon= temp.Icon});
				}
			}


		}
	}
}
