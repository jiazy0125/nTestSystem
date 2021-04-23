using Settings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.Resource;
using nTestSystem.DataHelper.Class;

namespace Settings
{
	public class SettingsModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.SystemMenuRegion, typeof(SettingBtn));
			regionManager.RegisterViewWithRegion(RegionManage.GeneralSettingRegion, typeof(GeneralPage));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<SettingPage>();
		}
	}
}