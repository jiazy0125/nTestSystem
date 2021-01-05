using Prism.Ioc;
using nTestSystem.Desktop.Views;
using System.Windows;
using nTestSystem.Framework.Commons;
using nTestSystem.Desktop.Resources;
using System.Globalization;
using nTestSystem.Framework.Configurations;
using Prism.Modularity;
using System.IO;
using Prism.Regions;
using System.Windows.Controls;

namespace nTestSystem.Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);


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
		}




	}
}
