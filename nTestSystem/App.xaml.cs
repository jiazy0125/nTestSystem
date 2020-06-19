﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;

namespace nTestSystem
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		[Obsolete]
		protected override void OnStartup(StartupEventArgs e)

		{
			base.OnStartup(e);
			Bootstrapper bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
