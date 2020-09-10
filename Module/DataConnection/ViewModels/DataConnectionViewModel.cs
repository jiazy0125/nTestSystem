using nTestSystem.Aggregator;
using nTestSystem.DataHelper.Class;
using nTestSystem.Framework.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows;

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
			_ea.GetEvent<TitleChangedEvent>().Publish(Resources.Resource.ApplyBtn);
		}
		private void SaveToConfiguration()
		{
			try
			{

				Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				cfa.AppSettings.Settings["FirstStart"].Value = "False";
				cfa.AppSettings.Settings["DBHelperName"].Value = _nc[DatabaseSelected];
				cfa.AppSettings.Settings["Connection"].Value = ConnectString;
				cfa.Save();
				_ea.GetEvent<NavigateEvent>().Publish("TopWindow");
			}
			catch { }
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{

			//设置当前标题
			//navigationContext.Parameters.Add(RegionManage.TitleRegion, Application.Current.TryFindResource("ConnectionSettingTitle") as string);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

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
