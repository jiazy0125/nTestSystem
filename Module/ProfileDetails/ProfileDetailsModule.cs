using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProfileDetails.Views;
using ProfileDetails.ViewModels;
using ProfileDetails.Resources;
using nTestSystem.Resource;

namespace ProfileDetails
{
	public class ProfileDetailsModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			ResourceHandler.Instance.Add(Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterDialog<ProfilePage, ProfilePageViewModel>("ProfilePage");
		}
	}
}