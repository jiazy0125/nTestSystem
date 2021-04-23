using Prism.Ioc;
using Prism.Modularity;
using nTestSystem.Resource;

namespace TopWindow
{
	public class TopWindowModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<Views.TopWindow>();
		}
	}
}