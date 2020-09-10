using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using nTestSystem.Framework.Commons;
using nTestSystem.Framework.Configurations;
using nTestSystem.Resources;
using nTestSystem.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace nTestSystem
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		//软件语言类型
		public static string Language { get; set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ResourceHandler.Instance.Add(Resource.ResourceManager);
			ResourceHandler.Instance.CurrentUICulture = new CultureInfo(AppSettingHelper.ReadKey("Language", "en-US"));

		}

		//prism函数

		protected override IModuleCatalog CreateModuleCatalog()
		{
			string modulePath = @".\Modules";
			if (!Directory.Exists(modulePath))
			{
				Directory.CreateDirectory(modulePath);
			}
			return new DirectoryModuleCatalog() { ModulePath = modulePath };
		}

		protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
		{
			base.ConfigureRegionAdapterMappings(regionAdapterMappings);
			regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
		}
		protected override Window CreateShell()
		{
			return Container.Resolve<Shell>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			//containerRegistry.RegisterForNavigation<ConnectionView>();
			//containerRegistry.RegisterForNavigation<SignInView>();
			//containerRegistry.RegisterForNavigation<MainView>();
		}




	}
}
