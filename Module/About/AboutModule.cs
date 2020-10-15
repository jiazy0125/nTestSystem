using About.Views;
using nTestSystem.DataHelper.Class;
using nTestSystem.Framework.Commons;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace About
{
	public class AboutModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.SystemMenuRegion, typeof(AboutBtn));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<AboutPage>();
		}
	}
}