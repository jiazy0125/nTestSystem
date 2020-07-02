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
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Prism.Events;
using nTestSystem.Class;

namespace nTestSystem.ViewModels
{
	public class ConnectionViewModel: BindableBase
	{

		#region Fields

		private readonly NameValueCollection _nc;
		private readonly IEventAggregator _ea;

		#endregion

		#region Properties

		//数据库类型列表
		public ObservableCollection<string> DBList { get; }

		//已选择数据类型
		public string DatabaseSelected { private get; set; }

		//数据库连接字符串

		public string ConnectString { private get; set; }

		#endregion

		#region Command

		//保存配置信息命令
		public DelegateCommand ApplySettings { get; set; }


		#endregion

		#region Execute
		public void View_Loaded(object sender, EventArgs e)
		{
			//if (!isFirstRun) ShellSwitcher.Switch<ConnectionView, Shell>();
		}
		private void SaveToConfiguration()
		{
			//Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//if (!cfa.AppSettings.Settings.AllKeys.Contains("FirstRun"))
			//{
			//	cfa.AppSettings.Settings.Add("FirstRun", "True");
			//}
			//cfa.AppSettings.Settings["FirstRun"].Value="False";
			//cfa.Save();
			_ea.GetEvent<NavigateTransform>().Publish("");
		}

		#endregion


		public ConnectionViewModel(IEventAggregator ea)
		{

			_nc= (NameValueCollection)ConfigurationManager.GetSection("databaseList");

			DBList = new ObservableCollection<string>(_nc.AllKeys);

			//var first = ConfigurationManager.AppSettings["FirstRun"];
			//if (first == null || first?.ToLower() == "true") 
			//{

			//}
			_ea = ea;
			ApplySettings = new DelegateCommand(SaveToConfiguration);

		}






	}
}
