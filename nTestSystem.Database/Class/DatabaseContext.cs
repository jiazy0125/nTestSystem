using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.DatabaseHelper.Class
{
	public class DatabaseContext<TEntity> :DbContext where TEntity:DatabaseModel
	{

		public DbSet<TEntity> Model { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("Data Source=info.db");

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{

		//}

	}
}
