using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ProfileDetails.Resources;
using nTestSystem.Resource;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProfileDetails.ViewModels
{
	public class ProfilePageViewModel : BindableBase, IDialogAware
	{
		#region Properties
		private double top = 0;
		public double Top 
		{ 
			get=>top;
			set => SetProperty(ref top, value); 
		}

		private double left = 0;
		public double Left 
		{
			get => left;
			set => SetProperty(ref left, value);
		}

		public DelegateCommand Test { get; set; }

		public DelegateCommand UpdatePhoto { get; set; }
		#endregion

		public ProfilePageViewModel()
		{
			Test = new DelegateCommand(() => { RequestClose?.Invoke(new DialogResult(ButtonResult.No)); });

			UpdatePhoto = new DelegateCommand(() => 
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Title = ResourceHandler.Instance.Get(ResKeys.Description) as string,
					Filter = $"{ResourceHandler.Instance.Get(ResKeys.Filter) as string}(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp",
					FileName = string.Empty,
					FilterIndex = 1,
					RestoreDirectory = true,
					DefaultExt = ".jpg"
				};
				bool? result = openFileDialog.ShowDialog();
				if (result == false) return;

				string fileName = openFileDialog.FileName;




			});





		}


		#region Impletement
		public string Title => "";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			var msg = parameters.GetValue<string>("msg");
			Top = Convert.ToDouble(msg.Substring(0, msg.IndexOf(",")));
			Left = Convert.ToDouble(msg[(msg.IndexOf(",") + 1)..]);

		}
		#endregion
	}
}
