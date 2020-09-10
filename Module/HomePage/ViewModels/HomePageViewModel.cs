using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomePage.ViewModels
{
	public class HomePageViewModel : BindableBase, INavigationAware
	{
		public HomePageViewModel()
		{

		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			 
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			 
		}
	}
}
