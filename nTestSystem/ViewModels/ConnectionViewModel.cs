using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using nTestSystem.Framework.Commons;
using nTestSystem.Views;
using System.Configuration;
using System.Windows;

namespace nTestSystem.ViewModels
{
	class ConnectionViewModel: BindableBase
	{

		#region Properties

		private bool isFirstRun = false;




		//是否显示在任务栏
		private bool showInTaskbar = false;
		public bool ShowInTaskbar
		{
			get => showInTaskbar;
			set => SetProperty(ref showInTaskbar, value);
		}
		//窗口状态
		private WindowState windowState = WindowState.Minimized;
		public WindowState WindowState
		{
			get => windowState;
			set => SetProperty(ref windowState, value);
		}
		
		//窗口可见
		private Visibility visibility = Visibility.Hidden;

		public Visibility Visibility
		{
			get => visibility;
			set => SetProperty(ref visibility, value);
		}

		#endregion

		#region Command

		//保存配置信息命令
		public DelegateCommand SaveConnection { get; set; }


		#endregion

		#region Execute
		public void View_Loaded(object sender, EventArgs e)
		{
			if (!isFirstRun) ShellSwitcher.Switch<ConnectionView, Shell>();
		}


		#endregion


		public ConnectionViewModel()
		{

			var first = ConfigurationManager.AppSettings["FirstRun"];
			if (first == null || first?.ToLower() == "true") 
			{
				ShowInTaskbar = true;
				WindowState = WindowState.Normal;
				Visibility = Visibility.Visible;
				isFirstRun = true;
			}

			SaveConnection = new DelegateCommand(SaveToConfiguration);

		}





		private void SaveToConfiguration()
		{
			Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (!cfa.AppSettings.Settings.AllKeys.Contains("FirstRun"))
			{
				cfa.AppSettings.Settings.Add("FirstRun", "True");
			}
			cfa.AppSettings.Settings["FirstRun"].Value="False";
			cfa.Save();
			ShellSwitcher.Switch<ConnectionView, Shell>();
		}
	}
}
