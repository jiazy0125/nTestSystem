using Initialization.Views;
using nTestSystem.Framework.Commons;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Initialization
{
	public class InitializationModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<InitializationView>();
		}

		public InitializationModule()
		{

		}
	}
}
