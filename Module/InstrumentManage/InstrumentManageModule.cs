using InstrumentManage.Views;
using nTestSystem.DataHelper.Class;
using nTestSystem.Resource;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace InstrumentManage
{
	public class InstrumentManageModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.SettingMenuRegion, typeof(InstrBtn));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<InstrPage>();
		}
	}
}