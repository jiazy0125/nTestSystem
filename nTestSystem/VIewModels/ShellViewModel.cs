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
using System.Windows.Input;
using CommonServiceLocator;
using nTestSystem.Framework.Extensions;

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

		private double minWidth = 100;
		public double MinWidth
		{
			get => minWidth;
			set => SetProperty(ref minWidth, value);
		}

		private double minHeight = 100;
		public double MinHeight
		{
			get => minHeight;
			set => SetProperty(ref minHeight, value);
		}

		private SizeToContent sizeToContent = SizeToContent.WidthAndHeight;
		public SizeToContent SizeToContent
		{
			get => sizeToContent;
			set => SetProperty(ref sizeToContent, value);
		}

		private ResizeMode resizeMode = ResizeMode.NoResize;
		public ResizeMode ResizeMode
		{
			get => resizeMode;
			set => SetProperty(ref resizeMode, value);
		}


		#endregion

		public ShellViewModel(IRegionManager regionManager, IEventAggregator ea, IContainerExtension container, IModuleCatalog mc)
		{
			_mc = mc;
			_ea = ea;
			_ce = container;
			_rm = regionManager;
			_ea.GetEvent<LoadedEvent>().Subscribe(NavigationLoaded);
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
			
		}

		#region Command
		public DelegateCommand<object> Load { get; set; }


		#endregion

		#region Execute

		private void NavigationLoaded(bool b)
		{
			//初始化、登录界面采用自适应
			//主界面时取消自适应，否则最大化窗口会有问题
			//主界面时手动指定窗口最小区域值
			Visibility = b ? Visibility.Visible : Visibility.Collapsed;
			SizeToContent = SizeToContent.Manual;
			ResizeMode = ResizeMode.CanResizeWithGrip;
			MinWidth = 300;
			MinHeight = 300;
			Application.Current.MainWindow.CenterWindowOnScreen();
			_ea.GetEvent<LoadedEvent>().Unsubscribe(NavigationLoaded);
			_ea.GetEvent<NavigateEvent>().Unsubscribe(Navigate);
		}


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


	public class LoadedEvent : PubSubEvent<bool>
	{

	}
}
