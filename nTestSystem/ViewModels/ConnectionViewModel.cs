using System;
using Prism.Commands;
using Prism.Mvvm;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Prism.Events;
using nTestSystem.Class;
using nTestSystem.UserControls.EventAggregator;
using Prism.Regions;
using System.Windows;

namespace nTestSystem.ViewModels
{
	public class ConnectionViewModel: BindableBase, INavigationAware
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
			
		}
		private void SaveToConfiguration()
		{
			try
			{

				//Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				//cfa.AppSettings.Settings["FirstStart"].Value = "False";
				//cfa.AppSettings.Settings["DBHelperName"].Value = _nc[DatabaseSelected];
				//cfa.AppSettings.Settings["Connection"].Value = ConnectString;
				//cfa.Save();
				_ea.GetEvent<NavigateEvent>().Publish("SignInView");
			}
			catch { }
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			//设置当前标题
			navigationContext.Parameters.Add(RegionManage.TitleRegion, Application.Current.TryFindResource("ConnectionSettingTitle") as string);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

		}

		#endregion

		public ConnectionViewModel(IEventAggregator ea)
		{
			//加载数据库类型列表
			_nc= (NameValueCollection)ConfigurationManager.GetSection("databaseList");
			DBList = new ObservableCollection<string>(_nc.AllKeys);
			_ea = ea;
			ApplySettings = new DelegateCommand(SaveToConfiguration);

		}






	}
}
