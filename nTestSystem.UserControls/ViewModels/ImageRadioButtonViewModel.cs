using System.Windows.Media;
using nTestSystem.Aggregator;
using Prism.Events;
using Prism.Mvvm;

namespace nTestSystem.UserControls.ViewModels
{
	public class ImageRadioButtonViewModel:BindableBase
	{
		private readonly IEventAggregator _ea;

		private object _content = "ImageRadioButton";
		public object Content 
		{
			get => _content;
			set => SetProperty(ref _content, value); 
		}

		private string _groupName = "Menu";
		public string GroupName
		{
			get => _groupName;
			set => SetProperty(ref _groupName, value);
		}

		private Geometry _image = Geometry.Empty;

		public Geometry Image
		{
			get => _image;
			set => SetProperty(ref _image, value);
		}

		private bool _isChecked = false;

		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				SetProperty(ref _isChecked, value);
				if (_isChecked) SendMessage();

			}

		}

		public string ViewName { private get; set; }

		public ImageRadioButtonViewModel(IEventAggregator ea)
		{
			_ea = ea;
		}
		private void SendMessage()
		{
			_ea.GetEvent<NavigateEvent>().Publish(ViewName);
		}
	}
}
