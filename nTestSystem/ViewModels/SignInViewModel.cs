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

namespace nTestSystem.ViewModels
{
	public class SignInViewModel :BindableBase, INavigationAware
	{

		#region Execute
		public void View_Loaded(object sender, EventArgs e)
		{
			Application.Current.MainWindow.CenterWindowOnScreen();
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
		public SignInViewModel()
		{ 
		
		}

	}
}
