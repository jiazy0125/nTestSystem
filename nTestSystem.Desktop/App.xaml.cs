using Prism.Ioc;
using System.IO;
using Prism.Regions;
using System.Windows;
using Prism.Modularity;
using System.Diagnostics;
using System.Windows.Controls;
using nTestSystem.Desktop.Views;
using nTestSystem.Framework.Commons;
using nTestSystem.Resource;
using System.Globalization;
using nTestSystem.Framework.Configurations;

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

#if DEBUG
			//Resolving harmless binding errors in WPF
			PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Critical;
#endif

			ResourceHandler.Instance.Add(Desktop.Resources.Resource.ResourceManager);
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

		}




	}
}
