using nTestSystem.Aggregator;
using nTestSystem.Framework.Configurations;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media;

namespace ProcessDesign.ViewModels
{
	public class ProcessDesignBtnViewModel : BindableBase
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
					_ea.GetEvent<NavigateEvent>().Publish(typeof(Views.ProcessDesignPage).Name);

			}

		}

		public Geometry Icon
		{
			get => Geometry.Parse(AppSettingHelper.ReadKey("ProcessDesign"));
		}

		public ProcessDesignBtnViewModel(IEventAggregator ea)
		{
			_ea = ea;
		}
	}
}
