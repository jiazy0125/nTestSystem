using nTestSystem.Framework.Extensions;
using nTestSystem.Framework.Configurations;
using nTestSystem.Aggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows;
using nTestSystem.Framework.Commons;
using Initialization.Resources;

namespace Initialization.ViewModels
{
	class InitializationViewModel: BindableBase, INavigationAware
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
		
		/// <summary>
		/// 区域信息变化时处理函数,未绑定属性不会自动根据区域切换语言
		/// </summary>
		private void CultureChanged()
		{
			//通知标题更改对应语言
			_ea.GetEvent<TitleChangedEvent>().Publish(ResourceHandler.Instance.Get(ResKeys.Title) as string);
			//居中屏幕
			//Application.Current.MainWindow.CenterWindowOnScreen();
		}
		/// <summary>
		/// 应用按钮Click事件
		/// </summary>
		private void SaveToConfiguration()
		{
			try
			{
				//初始化完成,置位首次启动标志
				AppSettingHelper.WriteKey("FirstStart", "false");
				//保存数据库类型
				AppSettingHelper.WriteKey("DBHelperName", _nc[DatabaseSelected]);
				//保存数据库连接字符串
				AppSettingHelper.WriteKey("Connection", ConnectString);
				//通知更改导航
				_ea.GetEvent<NavigateEvent>().Publish(AppSettingHelper.ReadKey("SignIn"));
			}
			catch { }
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			//第一次导航需要通知标题更改
			CultureChanged();
			//居中
			Application.Current.MainWindow.CenterWindowOnScreen();
			//注册区域更改委托
			ResourceHandler.Instance.CulturetInfoChanged += CultureChanged;

		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			//导航离开时,取消注册区域更改委托
			ResourceHandler.Instance.CulturetInfoChanged -= CultureChanged;
		}

		#endregion

		public InitializationViewModel(IEventAggregator ea)
		{
			//加载资源管理器
			ResourceHandler.Instance.Add(Resources.Resources.ResourceManager);

			//加载数据库类型列表
			_nc = (NameValueCollection)ConfigurationManager.GetSection("databaseList");
			DBList = new ObservableCollection<string>(_nc.AllKeys);
			_ea = ea;
			ApplySettings = new DelegateCommand(SaveToConfiguration);

		}
	}
}
