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
	public class TopWindowViewModel : BindableBase
	{
		#region Fields
		private readonly IEventAggregator _ea;

		
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
		}


		//右侧工作区域导航切换
		private void Navigate(string navigatePath)
		{
			if (navigatePath != null)
				RegionManager.RequestNavigate(RegionManage.SubItemRegion, navigatePath);

		}
		#endregion

		public TopWindowViewModel(IEventAggregator ea, IRegionManager rm)
		{
			_ea = ea;
			RegionManager = rm;
			_ea.GetEvent<NavigateEvent>().Subscribe(Navigate);
		}

	}
}
