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
using nTestSystem.Aggregator;
using System.Windows.Controls;
using CommonServiceLocator;
using System.Configuration;

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
			LoadSlideMenus();
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

		/// <summary>
		/// 加载左侧菜单按钮
		/// </summary>
		private void LoadSlideMenus()
		{
			if (ConfigurationManager.GetSection(AppSettingHelper.ReadKey("Language")) is SlideMenuSection config)
			{
				var exps = new Dictionary<string, Expander>();
				foreach (SlideMenuElement temp in config.SlideMenus)
				{
					//AddViewItem(temp.GroupName, temp.ViewName, temp.MenuName, temp.Icon);
					var view = _ce.Resolve<ImageRadioButton>();
					var vm = new ImageRadioButtonViewModel(_ea)
					{
						GroupName = "Menu",
						Content = temp.MenuName,
						ViewName = temp.ViewName,
						Image = temp.Icon,
						IsChecked = false,
					};
					view.DataContext = vm;
					//group name为空,默认将控件添加至主菜单下
					if (temp.GroupName == "" || temp.GroupName is null)
					{
						//添加至主菜单视图下
						IRegion region = RegionManager.Regions[RegionManage.SlideMenuMainRegion];
						region.Add(view);
					}
					else
					{
						//若不为空，则查找是否存在对应group name的菜单
						if (exps.ContainsKey(temp.GroupName))
						{
							((StackPanel)exps[temp.GroupName].Content).Children.Add(view);
						}
						else
						{
							//不存在group name，则新建并添加至词典
							//stackpanel
							var sp = _ce.Resolve<StackPanel>();
							sp.Children.Add(view);
							//expander
							var exp = _ce.Resolve<Expander>();
							exp.Content = sp;
							exp.IsExpanded = true;
							//绑定header
							var str = Application.Current.TryFindResource(temp.GroupName) as string;
							if (str is null || str == "") exp.Header = Application.Current.TryFindResource("UnTitled") as string;
							//绑定至动态资源标签
							else exp.SetResourceReference(HeaderedContentControl.HeaderProperty, temp.GroupName);
							exps.Add(temp.GroupName, exp);
						}
					}

				}
				//将所有新添加View添加至SlideMenu中
				foreach (KeyValuePair<string, Expander> kvp in exps)
				{
					RegionManager.Regions[RegionManage.SlideMenuRegion].Add(kvp.Value);
				}
			}
		}
		//右侧工作区域导航切换
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
