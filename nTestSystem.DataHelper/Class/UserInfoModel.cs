using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace nTestSystem.DataHelper.Class
{
	public class UserInfoModel
	{

		public string Name { get; set; }

		public ImageSource HeadImg { get; set; }

		public AuthorityLevel Authority { get; set; }

		public DateTime LastLogon { get; set; }

		public DateTime Logon { get; set; }

		public string Location { get; set; }

		

	}

	public enum AuthorityLevel
	{ 
		Admin,
		Guest,
		User,
		SuperAdmin
	
	}
}
