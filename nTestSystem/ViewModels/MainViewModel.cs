using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using nTestSystem.Class;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.Framework.Extensions;
using System.Windows.Media;
using nTestSystem.UserControls.Views;
using nTestSystem.UserControls.ViewModels;
using nTestSystem.Framework.Configurations;
using nTestSystem.UserControls.EventAggregator;
using System.Windows.Controls;
using CommonServiceLocator;

namespace nTestSystem.ViewModels
{
	class MainViewModel: INavigationAware
	{

		#region Fields
		private readonly IEventAggregator _ea;
		private readonly IContainerExtension _ce;


		#endregion

		#region Properties

		public IRegionManager RegionManager { get; }

		#endregion

		#region Command




		#endregion


		#region Execute

		/// <summary>
		/// 界面加载完毕执行事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void View_Loaded(object sender, EventArgs e)
		{
			Application.Current.MainWindow.CenterWindowOnScreen();
			_ea.GetEvent<LoadedEvent>().Publish(true);
			//LoadSlideMenus();
			AddViewItem(null, null, null, null);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			//设置当前标题
			navigationContext.Parameters.Add(RegionManage.TitleRegion, Application.Current.TryFindResource("MainTitle") as string);
		}


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

		private void AddViewItem(string regionName, string viewName, string content, Geometry image)
		{
			//var view = _ce.Resolve<ImageRadioButton>();
			//IRegion region = RegionManager.Regions[regionName];
			//var vm = new ImageRadioButtonViewModel(_ea)
			//{
			//	GroupName = "Menu",
			//	Content = content,
			//	ViewName = viewName,
			//	Image = image,
			//	IsChecked = false,
			//};
			//view.DataContext = vm;
			//region.Add(view);


			var view1 = _ce.Resolve<Expander>();
			view1.IsExpanded = true;
			view1.SetResourceReference(HeaderedContentControl.HeaderProperty, "SlideMenu2");

			RegionManager.RegisterViewWithRegion(RegionManage.SlideMenuRegion2, typeof(StackPanel));
			var view2 = _ce.Resolve<StackPanel>();

			//RegionManager.AddToRegion( RegionManage.SlideMenuRegion2,view2);
			view1.Content= view2;
			IRegion region1 = RegionManager.Regions[RegionManage.SlideMenuRegion];
			region1.Add(view1);

			var view = _ce.Resolve<ImageRadioButton>();
			IRegion region = RegionManager.Regions[RegionManage.SlideMenuRegion2];
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

		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				RegionManager.RequestNavigate(RegionManage.SubItemRegion, navigatePath);

		}
		#endregion

		public MainViewModel(IEventAggregator ea, IRegionManager rm, IContainerExtension container, IModuleCatalog mc)
		{
			_ea = ea;
			_ce = container;
			RegionManager = rm;
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
		}

	}
}
