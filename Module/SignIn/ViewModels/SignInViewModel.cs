using nTestSystem.Framework.Configurations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using nTestSystem.Framework.Extensions;
using nTestSystem.DataHelper.Class;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace SignIn.ViewModels
{
	public class SignInViewModel : BindableBase, IDialogAware
	{
		#region Fields


		#endregion

		#region Properties

		public UserInfoModel UserInfo { get; private set; } = new UserInfoModel() { Name="",Password=""};

		public bool AutoLogin
		{
			get => AppSettingHelper.ReadKey("AutoLogin","false") == "true";
			set => AppSettingHelper.WriteKey("AutoLogin", value ? "true" : "false");

		}


		public DelegateCommand SignIn { get; set; }

		#endregion


		public SignInViewModel()
		{
			SignIn = new DelegateCommand(()=> 
			{ 
			
			
			
			
			});
			string userHeadPath = @$".\Users\{UserInfo.Name}\head.jpg";
			if (File.Exists(userHeadPath))
				UserInfo.HeadSource = new BitmapImage(new Uri(userHeadPath, UriKind.Relative));
			else
				UserInfo.HeadSource= new BitmapImage(new Uri(@".\Images\default.jpg", UriKind.Relative));
		}



		#region IDialogImplement
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
			
		}

		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}
		#endregion
	}
}
