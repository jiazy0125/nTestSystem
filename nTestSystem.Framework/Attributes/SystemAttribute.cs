using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using nTestSystem.Framework.Commons;

namespace nTestSystem.Framework.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class SystemAttribute : Attribute
	{






	}
	
	/// <summary>
	/// configuration attribute
	/// 配置文件属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class ConfigurationAttribute : Attribute
	{

		public ConfigurationAttribute(string fileName, string section)
		{
			FileName = fileName;
			Section = section;
		}

		//configuration file name， such as *.ini
		//配置文件名称*.ini
		public string FileName { get; }
		
		//Section name
		//配置文件内段名
		public string Section { get; }

	}



	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ColumnAttribute : Attribute
	{
		public ColumnAttribute(int index)
		{
			//Name = name;
			Flag = (ColumnFlags)(index >= 0 ? 1 << index : 0);
		}
		//public string Name { get; }

		public ColumnFlags Flag { get; }

		public bool IsInDatabase { get; set; }

	}


	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DatabaseAttribute : Attribute
	{

		public DatabaseAttribute(string table)
		{
			Table = table;
		}
		public string Table { get; }

	}



}
