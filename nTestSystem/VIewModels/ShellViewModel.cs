using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using nTestSystem.Framework.Configurations;
using nTestSystem.UserControls.EventAggregator;
using Prism.Events;
using Prism.Ioc;
using nTestSystem.UserControls.ViewModels;
using nTestSystem.UserControls.Views;
using System.Windows.Media;
using System;
using Prism.Modularity;
using nTestSystem.Class;
using System.Windows;
using System.Configuration;
using System.Linq;

namespace nTestSystem.ViewModels
{
	class ShellViewModel: BindableBase
	{
		private readonly IEventAggregator _ea;
		private readonly IContainerExtension _ce;
		private readonly IModuleCatalog _mc;
		#region Properties

		public IRegionManager RegionMannager { get; private set; }

		//根据界面设置部分控件隐藏显示
		private Visibility visibility = Visibility.Collapsed;
		public Visibility Visibility 
		{
			get => visibility;
			set => SetProperty(ref visibility, value);
		}


		private string title; 
			//Application.Current.TryFindResource("资源字典的KEY") as string;
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}
		#endregion

		public ShellViewModel(IRegionManager regionManager, IEventAggregator ea, IContainerExtension container, IModuleCatalog mc)
		{
			_mc = mc;
			_ea = ea;
			_ce = container;
			RegionMannager = regionManager;
			_ea.GetEvent<MessageSentEvent>().Subscribe(Navigate);
			_ea.GetEvent<NavigateTransform>().Subscribe(Navigate);
			if (CheckFirstStart())
				RegionMannager.RequestNavigate(RegionManage.ShellRegion, "ConnectionView");
			else
				RegionMannager.RequestNavigate(RegionManage.ShellRegion, "SignInView");

		}

		#region Command
		public DelegateCommand<object> Load { get; set; }


		#endregion

		#region Execute
		public void View_Loaded(object sender, EventArgs e)
		{
			LoadSlideMenus();
		}



		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				RegionMannager.RequestNavigate("ItemRegion", navigatePath);

		}

		//动态添加Slidemenu
		private void AddViewItem(string regionName, string viewName, string content, Geometry image)
		{
			//var view = _ce.Resolve<ImageRadioButton>();
			//IRegion region = RegionMannager.Regions[regionName];
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




		}

		//从appcongfig中读取SlideMenu列表
		private void LoadSlideMenus()
		{
			RegionMannager.RequestNavigate(RegionManage.ShellRegion, "ConnectionView");



			//if (System.Configuration.ConfigurationManager.GetSection("menu-en") is SlideMenuSection config)
			//{
			//	foreach (SlideMenuElement temp in config.SlideMenus)
			//	{
			//		AddViewItem(temp.RegionName, temp.ViewName, temp.MenuName, temp.Icon);
			//	}
			//}
		}

		/// <summary>
		/// 检查程序是否首次启动
		/// </summary>
		/// <returns></returns>
		private bool CheckFirstStart()
		{
			Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (!cfa.AppSettings.Settings.AllKeys.Contains("FirstStart"))
			{
				cfa.AppSettings.Settings.Add("FirstStart", "True");
				return true;
			}
			else
			{
				return ConfigurationManager.AppSettings["FirstStart"].Trim().ToLower() == "true";
			}
			//cfa.AppSettings.Settings["FirstStart"].Value = "False";
			//cfa.Save();

		}

		#endregion

	}
}
