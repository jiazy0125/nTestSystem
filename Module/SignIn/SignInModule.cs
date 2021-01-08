using SignIn.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using nTestSystem.Framework.Commons;

namespace SignIn
{
	public class SignInModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterDialog<Views.SignIn,ViewModels.SignInViewModel>("SignIn");//注册自定义对话框窗体
		}
	}
}