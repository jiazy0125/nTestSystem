using nTestSystem.Aggregator;
using nTestSystem.Framework.Configurations;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media;

namespace InstrumentManage.ViewModels
{
	public class InstrBtnViewModel : BindableBase
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
					_ea.GetEvent<NavigateEvent>().Publish(typeof(Views.InstrPage).Name);

			}

		}

		public Geometry Icon
		{
			get => Geometry.Parse(AppSettingHelper.ReadKey("Instrument"));
		}

		public InstrBtnViewModel(IEventAggregator ea)
		{
			_ea = ea;
		}
	}
}
