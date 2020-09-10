using nTestSystem.Aggregator;
using nTestSystem.Framework.Extensions;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;
using nTestSystem.DataHelper.Class;


namespace TopWindow.ViewModels
{
	public class TopWindowViewModel : BindableBase, INavigationAware
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
			_ea.GetEvent<TitleChangedEvent>().Publish(Resources.Resource.TopWindowTitle);
			_ea.GetEvent<LoadedEvent>().Publish(true);
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
			//navigationContext.Parameters.Add(RegionManage.TitleRegion, Resources.Resource.TopWindowTitle);
		}

		//右侧工作区域导航切换
		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				RegionManager.RequestNavigate(RegionManage.SubItemRegion, navigatePath);

		}
		#endregion

		public TopWindowViewModel(IEventAggregator ea, IRegionManager rm, IContainerExtension container, IModuleCatalog mc)
		{
			_ea = ea;
			_ce = container;
			RegionManager = rm;
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
		}

	}
}
