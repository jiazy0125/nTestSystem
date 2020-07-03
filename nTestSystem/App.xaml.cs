using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using nTestSystem.Framework.Commons;
using nTestSystem.Views;
using Prism;
using Prism.Events;
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
			GetLanguage();
		}

		private void GetLanguage()
		{
			Language = string.Empty;
			try
			{
				
				Language = nTestSystem.Properties.Settings.Default.Language.Trim();
			}
			catch (Exception)
			{
			}
			Language = string.IsNullOrEmpty(Language) ? "en-US" : Language;

			//update Language
			UpdateLanguage();
		}

		/// <summary>
		/// 保存语言设置
		/// </summary>
		private void SaveLanguage()
		{
			try
			{
				nTestSystem.Properties.Settings.Default.Language = Language;
				nTestSystem.Properties.Settings.Default.Save();
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 更换语言包
		/// </summary>
		public static void UpdateLanguage()
		{
			List<ResourceDictionary> dictionaryList = new List<ResourceDictionary>();
			foreach (ResourceDictionary dictionary in Current.Resources.MergedDictionaries)
			{
				dictionaryList.Add(dictionary);
			}
			string requestedLanguage = string.Format(@"Language\{0}.xaml", Language);
			ResourceDictionary resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedLanguage));
			if (resourceDictionary == null)
			{
				requestedLanguage = @"Language\en-US.xaml";
				resourceDictionary = dictionaryList.FirstOrDefault(d => d.Source.OriginalString.Equals(requestedLanguage));
			}
			if (resourceDictionary != null)
			{
				Current.Resources.MergedDictionaries.Remove(resourceDictionary);
				Current.Resources.MergedDictionaries.Add(resourceDictionary);
			}
		}

		//prism函数
		protected override IModuleCatalog CreateModuleCatalog()
		{
			return new ConfigurationModuleCatalog(); //目录创建于配置文件
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
			containerRegistry.RegisterForNavigation<ConnectionView>();
			containerRegistry.RegisterForNavigation<SignInView>();
		}




	}
}
