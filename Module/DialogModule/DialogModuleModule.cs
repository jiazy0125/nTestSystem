using DialogModule.Views;
using DialogModule.ViewModels;
using nTestSystem.Resource;
using Prism.Ioc;
using Prism.Modularity;

namespace DialogModule
{
	public class DialogModuleModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			ResourceHandler.Instance.Add(Resources.Resource.ResourceManager);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
			containerRegistry.RegisterDialog<SuccessDialog, SuccessDialogViewModel>();
			containerRegistry.RegisterDialog<WarningDialog, WarningDialogViewModel>();

			containerRegistry.RegisterDialogWindow<DialogWindow>();//注册自定义对话框窗体
		}
	}
}