using nTestSystem.Framework.Configurations;
using nTestSystem.Framework.Extensions;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using nTestSystem.Aggregator;
using System.Configuration;
using Prism.Commands;
using System.Windows;
using Prism.Events;
using Prism.Mvvm;
using System;



namespace DataConnection.ViewModels
{
	public class DataConnectionViewModel : BindableBase
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
			Application.Current.MainWindow.CenterWindowOnScreen();
		}
		/// <summary>
		/// 存储当前数据连接配置
		/// </summary>
		private void SaveToConfiguration()
		{
			try
			{
				AppSettingHelper.WriteKey("FirstStart", "False");
				AppSettingHelper.WriteKey("DBHelperName", _nc[DatabaseSelected]);
				AppSettingHelper.WriteKey("Connection", ConnectString);
				_ea.GetEvent<NavigateEvent>().Publish("TopWindow");
			}
			catch { }
		}


		#endregion


		public DataConnectionViewModel(IEventAggregator ea)
		{

			//加载数据库类型列表
			_nc = (NameValueCollection)ConfigurationManager.GetSection("databaseList");
			DBList = new ObservableCollection<string>(_nc.AllKeys);
			_ea = ea;
			ApplySettings = new DelegateCommand(SaveToConfiguration);

		}





	}
}
