using ProductManage.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.DataHelper.Class;
using nTestSystem.Framework.Commons;

namespace ProductManage
{
	public class ProductManageModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionManage.TopMenuRegion, typeof(ProdutManageBtn));
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<ProductManagePage>();
		}
	}
}