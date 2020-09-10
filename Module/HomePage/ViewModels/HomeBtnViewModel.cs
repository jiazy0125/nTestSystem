using nTestSystem.Aggregator;
using nTestSystem.Framework.Configurations;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace HomePage.ViewModels
{
	public class HomeBtnViewModel : BindableBase
	{
		private readonly IEventAggregator _ea;

		private bool _isChecked = false;

		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				SetProperty(ref _isChecked, value);
				if (_isChecked)
					_ea.GetEvent<NavigateEvent>().Publish(typeof(Views.HomePage).Name);

			}

		}

		public Geometry Icon
		{
			get => Geometry.Parse(AppSettingHelper.ReadKey("Home"));
		}

		public HomeBtnViewModel(IEventAggregator ea)
		{
			_ea = ea;
		}
	}
}
