using nTestSystem.Framework.Configurations;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Settings.ViewModels
{
	public class GeneralPageViewModel : BindableBase
	{


		public bool StartUp
		{
			get => AppSettingHelper.ReadKey("FirstStart", "false") == "true";

			set => AppSettingHelper.WriteKey("FirstStart", value ? "true" : "false");
		}

		public GeneralPageViewModel()
		{





		}
	}
}
