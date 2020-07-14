using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using nTestSystem.Framework.Extensions;
using System.Windows;
using Prism.Regions;
using nTestSystem.Class;
using Prism.Commands;
using Prism.Events;
using nTestSystem.UserControls.EventAggregator;

namespace nTestSystem.ViewModels
{
	public class SignInViewModel :BindableBase, INavigationAware
	{
		#region Fields
		private readonly IEventAggregator _ea;

		#endregion

		#region Properties


		#endregion

		#region Command

		public DelegateCommand SignIn { get; set; }


		#endregion

		#region Execute

		public void View_Loaded(object sender, EventArgs e)
		{
			Application.Current.MainWindow.CenterWindowOnScreen();
		}
		/// <summary>
		/// 登录按钮事件
		/// </summary>
		private void UserSignIn()
		{

			_ea.GetEvent<NavigateEvent>().Publish("MainView");

		}





		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			//设置当前标题
			navigationContext.Parameters.Add(RegionManage.TitleRegion, Application.Current.TryFindResource("SignInTitle") as string);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{

		}

		#endregion


		public SignInViewModel(IEventAggregator ea)
		{
			_ea = ea;
			SignIn = new DelegateCommand(UserSignIn);
		}

	}
}
