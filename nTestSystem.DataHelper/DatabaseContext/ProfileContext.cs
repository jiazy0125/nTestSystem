using Microsoft.EntityFrameworkCore;
using nTestSystem.DataHelper.Models;


namespace nTestSystem.DataHelper.DatabaseContext
{

	public class ProfileContext : DatabaseContext<ProfileInfo>
	{
		public DbSet<ProfileInfo> Profiles { get; set; }

	}
}
