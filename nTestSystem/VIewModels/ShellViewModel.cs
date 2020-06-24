using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using nTestSystem.Framework.Configurations;
using System.Collections.ObjectModel;
using nTestSystem.Models;
using nTestSystem.UserControls.EventAggregator;
using Prism.Events;
using Prism.Ioc;
using nTestSystem.UserControls.ViewModels;
using nTestSystem.UserControls.Views;
using System.Windows.Media;
using System;
using System.Windows;

namespace nTestSystem
{
	class ShellViewModel:BindableBase
	{
		private readonly IRegionManager _rm;
		private readonly IEventAggregator _ea;
		private readonly IContainerExtension _ce;
		public string Label1 { get; set; } = "Test1";

		public DelegateCommand<object> Load { get; set; }
		public ShellViewModel(IRegionManager regionManager, IEventAggregator ea, IContainerExtension container)
		{
			_ea = ea;
			_ce = container;
			_rm = regionManager;
			_ea.GetEvent<MessageSentEvent>().Subscribe(Navigate);
		}
		public void View_Loaded(object sender, EventArgs e)
		{
			LoadSlideMenus();
		}


		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				_rm.RequestNavigate("ItemRegion", navigatePath);

		}

		//动态添加Slidemenu
		private void AddViewItem(string regionName, string viewName, string content, Geometry image)
		{
			var view = _ce.Resolve<ImageRadioButton>();
			IRegion region = _rm.Regions[regionName];
			var vm = new ImageRadioButtonViewModel(_ea)
			{
				GroupName = "Menu",
				Content = content,
				ViewName = viewName,
				Image = image,
				IsChecked = false,
			};
			view.DataContext = vm;
			region.Add(view);
		}

		//从appcongfig中读取SlideMenu列表
		private void LoadSlideMenus()
		{
			if (System.Configuration.ConfigurationManager.GetSection("menu-en") is SlideMenuSection config)
			{
				foreach (SlideMenuElement temp in config.SlideMenus)
				{
					AddViewItem(temp.RegionName, temp.ViewName, temp.MenuName, temp.Icon);
				}
			}
		}

	}
}
