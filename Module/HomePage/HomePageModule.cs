using HomePage.Views;
using nTestSystem.Framework.Commons;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.DataHelper.Class;

namespace HomePage
{
	[Module(ModuleName ="HomePage")]
	public class HomePageModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.TopMenuRegion, typeof(HomeBtn));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<Views.HomePage>();
		}
	}

}