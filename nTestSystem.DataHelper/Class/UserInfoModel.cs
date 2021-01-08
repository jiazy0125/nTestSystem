using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Drawing;
using Prism.Mvvm;

namespace nTestSystem.DataHelper.Class
{
	public class UserInfoModel: BindableBase
	{
		private string name;
		public string Name 
		{
			get => name;
			set => SetProperty(ref name, value); 
		}

		private string password;
		public string Password 
		{
			get => password;
			set => SetProperty(ref password, value); 
		}

		private ImageSource headSource;
		public ImageSource HeadSource 
		{
			get => headSource;
			set => SetProperty(ref headSource, value);
		}

		private AuthorityLevel autLevel = AuthorityLevel.Guest;
		public AuthorityLevel Authority 
		{
			get => autLevel;
			set => SetProperty(ref autLevel, value);
		}

		public string lastlogon;
		public string LastLogon 
		{
			get => lastlogon;
			set => SetProperty(ref lastlogon, value);
		}

		private string logon;
		public string Logon 
		{
			get => logon;
			set => SetProperty(ref logon, value);
		}

		public string location;
		public string Location 
		{
			get => location;
			set => SetProperty(ref location, value);
		}

		

	}

	public enum AuthorityLevel
	{ 
		Admin,
		Guest,
		User,
		SuperAdmin
	
	}
}
