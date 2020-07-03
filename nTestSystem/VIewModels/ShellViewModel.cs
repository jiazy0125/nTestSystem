using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using nTestSystem.UserControls.EventAggregator;
using Prism.Events;
using Prism.Ioc;
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
		private readonly IRegionManager _rm;
		#region Properties

		//public IRegionManager RegionMannager { get; private set; }

		//根据界面设置部分控件隐藏显示
		private Visibility visibility = Visibility.Collapsed;
		public Visibility Visibility 
		{
			get => visibility;
			set => SetProperty(ref visibility, value);
		}


		private string title=Application.Current.TryFindResource("Title") as string;
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
			_rm = regionManager;
			_ea.GetEvent<MessageSentEvent>().Subscribe(Navigate);
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
			
		}

		#region Command
		public DelegateCommand<object> Load { get; set; }


		#endregion

		#region Execute
		public void View_Loaded(object sender, EventArgs e)
		{
			if (CheckFirstStart())			
				Navigate("ConnectionView");			
			else			
				Navigate("SignInView");			
		}



		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				_rm.RequestNavigate(RegionManage.ShellRegion, navigatePath, new Action<NavigationResult>(
					t=> 
					{
						Title = t.Context.Parameters[RegionManage.TitleRegion] as string;
					}));
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
			_rm.RequestNavigate(RegionManage.ShellRegion, "ConnectionView");



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
			//不存在该参数则添加并设置为首次启动
			if (!cfa.AppSettings.Settings.AllKeys.Contains("FirstStart"))
			{
				cfa.AppSettings.Settings.Add("FirstStart", "True");
				return true;
			}
			else
			{
				//存在则判断是否为首次启动
				return ConfigurationManager.AppSettings["FirstStart"].Trim().ToLower() == "true";
			}
		}

	
		#endregion

	}
}
