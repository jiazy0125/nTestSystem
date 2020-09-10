using DataConnection.Views;
using nTestSystem.Framework.Commons;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DataConnection
{
	public class DataConnectionModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<Views.DataConnection>();
		}
	}
}