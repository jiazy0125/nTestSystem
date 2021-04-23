using Microsoft.EntityFrameworkCore;
using nTestSystem.Framework.Configurations;
using System;
using System.Collections.Generic;
using System.Text;


namespace nTestSystem.DataHelper.DatabaseContext
{
	public class DatabaseContext<T>:DbContext where T :class
	{
		//Database instance
		public DbSet<T> DbInst { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			//var dbType = AppSettingHelper.ReadKey("Database", "SQLite");
			//var dataSource = AppSettingHelper.ReadKey("Connection");
			//if (dbType == "SQLite")
			//	options.UseSqlite($"Data Source={dataSource}");
			//if (dbType == "MSSQLSRV")
			//	options.UseSqlServer($"{dataSource}");
			options.UseSqlite("Data Source=info.db");
		}


	}
}
