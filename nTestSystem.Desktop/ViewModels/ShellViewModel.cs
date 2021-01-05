using nTestSystem.Aggregator;
using nTestSystem.DataHelper.Class;
using nTestSystem.Desktop.Resources;
using nTestSystem.Framework.Commons;
using nTestSystem.Framework.Configurations;
using nTestSystem.Framework.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace nTestSystem.Desktop.ViewModels
{
	public class ShellViewModel : BindableBase
	{
		#region fields

		private readonly IEventAggregator _ea;
		private readonly IRegionManager _rm;

		#endregion

		#region Properties

		//public IRegionManager RegionMannager { get; private set; }

		//根据界面设置部分控件隐藏显示
		private Visibility visibility = Visibility.Collapsed;
		public Visibility Visibility
		{
			get => visibility;
			set => SetProperty(ref visibility, value);
		}


		private string title = ResourceHandler.Instance.Get(ResKeys.Title) as string;
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

		private string signInLabel;
		public string SignInLabel
		{
			get => signInLabel;
			set => SetProperty(ref signInLabel, value);
		}

		private ImageSource headImg;

		public ImageSource HeadImg
		{
			get => headImg;
			set => SetProperty(ref headImg, value);
		}
		#endregion

		#region Command

		public DelegateCommand ChangedToCN { get; }
		public DelegateCommand ChangedToEn { get; }

		//signin click event handler
		public DelegateCommand SignInClick { get; }

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
			MinWidth = 800;
			MinHeight = 600;
			Application.Current.MainWindow.CenterWindowOnScreen();
			_ea.GetEvent<LoadedEvent>().Unsubscribe(NavigationLoaded);
			_ea.GetEvent<NavigateEvent>().Unsubscribe(Navigate);
			
		}

		public void View_Loaded(object sender, EventArgs e)
		{
			//不存在该参数则添加并设置为首次启动
			//存在则判断是否为首次启动
			if (AppSettingHelper.ReadKey("FirstStart", "true") == "true")
			{
				var aa = AppSettingHelper.ReadKey("Startup");
				Navigate(aa);
			}
			else
				Navigate(AppSettingHelper.ReadKey("Main"));
		}

		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				_rm.RequestNavigate(RegionManage.ShellRegion, navigatePath);
		}
		//更新登录用户信息
		private void UserInfoUpdate(UserInfoModel uim)
		{
			SignInLabel = uim.Name;
			HeadImg = uim.HeadImg;
		}
		#endregion

		public ShellViewModel(IRegionManager regionManager, IEventAggregator ea)
		{

			_ea = ea;
			_rm = regionManager;
			_ea.GetEvent<LoadedEvent>().Subscribe(NavigationLoaded);
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
			_ea.GetEvent<UserInfoTransmit>().Subscribe(UserInfoUpdate);

			ResourceHandler.Instance.Add(Resource.ResourceManager);
			ResourceHandler.Instance.CurrentUICulture = new CultureInfo(AppSettingHelper.ReadKey("Language", "en-US"));



			SignInClick = new DelegateCommand(() => { MessageBox.Show("Yes"); });

			SignInLabel = ResourceHandler.Instance.Get(ResKeys.SignInLabel) as string;

			ChangedToCN = new DelegateCommand(() => { ResourceHandler.Instance.CurrentUICulture = new CultureInfo("zh-CN"); });
			ChangedToEn = new DelegateCommand(() => { ResourceHandler.Instance.CurrentUICulture = new CultureInfo("en-US"); });


		}
	}
}
