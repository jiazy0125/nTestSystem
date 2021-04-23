using ProcessDesign.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.Resource;
using nTestSystem.DataHelper.Class;

namespace ProcessDesign
{
	public class ProcessDesignModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.TopMenuRegion, typeof(ProcessDesignBtn));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<ProcessDesignPage>();
		}
	}
}