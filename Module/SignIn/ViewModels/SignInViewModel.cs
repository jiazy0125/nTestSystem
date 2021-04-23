using System;
using System.IO;
using Prism.Mvvm;
using System.Linq;
using Prism.Events;
using Prism.Commands;
using SignIn.Resources;
using nTestSystem.Resource;
using Prism.Services.Dialogs;
using nTestSystem.Aggregator;
using nTestSystem.DataHelper.Models;
using nTestSystem.Framework.Commons;
using nTestSystem.Framework.Extensions;
using nTestSystem.Framework.Configurations;
using nTestSystem.DataHelper.DatabaseContext;

namespace SignIn.ViewModels
{
	public class SignInViewModel : BindableBase, IDialogAware
	{
		#region Fields
		private readonly IDialogService dialogService;
		private readonly string defaultPhoto = @".\Images\default.jpg";
		#endregion

		#region Properties

		public ProfileInfo ProfileInfo { get; private set; }

		public bool AutoLogin
		{
			get=> AppSettingHelper.ReadKey("AutoLogin", "false") == "true";

			set=>AppSettingHelper.WriteKey("AutoLogin", value ? "true" : "false");			
		}


		public DelegateCommand SignIn { get; set; }

		#endregion

		#region Execute
		/// <summary>
		/// when user input, update head image
		/// </summary>
		private void UpdateProfilePtoto()
		{
			if (ProfileInfo.Name.Length <= 0) return;
			//Read profile photo from user directory
			string photoPath = @$".\Users\{ProfileInfo.Name}\head.jpg";
			if (File.Exists(photoPath)) 
				ProfileInfo.Photo = photoPath;
		}
		//主动关闭登录对话框
		private void ExecuteCloseDialogCommand()
		{
			ButtonResult result = ButtonResult.No;
			RaiseRequestClose(new DialogResult(result));
		}

		//登录成功后，下载用户相关信息至本地
		private void DownloadProfile( ProfileInfo pfi)
		{
			var userFile = @$".\Users\{pfi.Name}";
			if (!Directory.Exists(userFile))
			{
				Directory.CreateDirectory(userFile);
			}
			var photo = @$"{userFile}\head.jpg";
			//下载头像图片
			if (!File.Exists(photo))
				if (ImageHelper.StringToImage(pfi.Photo, photo))
					pfi.Photo = photo;


		}

		#endregion
		public SignInViewModel(IEventAggregator ea, IDialogService ds)
		{
			dialogService = ds;
			//initialize profile
			ProfileInfo = new ProfileInfo()
			{
				NameChanged = UpdateProfilePtoto,
				Photo = defaultPhoto
			};

			//initialize sign in button click event
			SignIn = new DelegateCommand(()=> 
			{
				var msg = ResourceHandler.Instance.Get(ResKeys.EmptyWarning);
				//输入用户名或密码为空,弹出警告窗口
				if (ProfileInfo.Name.Length <= 0 || ProfileInfo.Password.Length <= 0)
				{

					dialogService.ShowDialog("WarningDialog", new DialogParameters($"message={msg}"), null);
					return;
				}
				//查询当前用户输入是否正确
				var pwd = ProfileInfo.Password.GenerateMD5();
				var db = new DatabaseContext<ProfileInfo>();
				var res = (from a in db.DbInst where a.Name == ProfileInfo.Name && a.Password == pwd select a).FirstOrDefault();
				//输入用户名或密码错误,弹出警告窗口
				if (res is null)
				{
					msg = ResourceHandler.Instance.Get(ResKeys.ErrorInput);
					dialogService.ShowDialog("WarningDialog", new DialogParameters($"message={msg}"), null);
					return;
				}
				DownloadProfile(res);
				//更新个人资料变量
				//ProfileInfo.CopyFrom(res);
				res.Logged = true;
				//更新个人信息至主界面
				ea.GetEvent<ProfileUpdate>().Publish(res);
				//关闭登录窗口
				ExecuteCloseDialogCommand();

			});

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
