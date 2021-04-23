using nTestSystem.DataHelper.Class;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using nTestSystem.Aggregator;

namespace nTestSystem.DataHelper.Models
{
	[Table("Profile")]
	public class ProfileInfo: BindableBase 
	{
		[NotMapped]
		public Action NameChanged { get; set; }
		[NotMapped]
		public bool Logged { get; set; } = false;

		[Key,Column(Order=0)]
		public int ID { get; set; }

		private string name = "";
		[Required, MaxLength(50)]
		public string Name 
		{
			get => name;
			set
			{
				SetProperty(ref name, value, NameChanged);	
			}
		}

		private string password = "";
		[Required, MaxLength(100)]
		public string Password 
		{
			get => password;
			set => SetProperty(ref password, value); 
		}

		private string photo;
		public string Photo
		{
			get => photo;
			set => SetProperty(ref photo, value);
		}

		private AuthorityLevel autLevel = AuthorityLevel.Guest;
		public AuthorityLevel Authority 
		{
			get => autLevel;
			set => SetProperty(ref autLevel, value);
		}

		
		private string lastlogon;
		[MaxLength(20)]
		public string LastLogon 
		{
			get => lastlogon;
			set => SetProperty(ref lastlogon, value);
		}

		private string logon;
		[MaxLength(20)]
		public string Logon 
		{
			get => logon;
			set => SetProperty(ref logon, value);
		}

		private string location;
		[MaxLength(20)]
		public string Location 
		{
			get => location;
			set => SetProperty(ref location, value);
		}


		public void CopyFrom(ProfileInfo source, bool updateAll=false)
		{
			Name = source.Name;
			LastLogon = source.LastLogon;
			Password = source.Password;
			Authority = source.Authority;
			if (updateAll)
			{
				Photo = source.Photo;
				Logon = source.Logon;
				Location = source.Location;
				Logged = source.Logged;
			}
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
