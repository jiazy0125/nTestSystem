using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Commons;
using System.IO;

namespace nTestSystem.ConfigurationHelper
{
	public class ConfigurationHelper
	{

		public static List<object> cfgList = new List<object>();

		public static bool Injection<T>(T data)
		{

			var attr = (ConfigurationAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ConfigurationAttribute));
			if (attr != null)
			{
				string fileName = $@"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}{attr.FileName}";

				if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
				{
					File.Create(fileName);

					InIHelper.FileName = fileName;
					PropertyInfo[] properties = data.GetType().GetProperties();
					
					foreach (PropertyInfo pi in properties)
					{ 
					
					
					}




				}




			}


			return default;
		
		}

		public static Result<T> IsExist<T>()
		{
			return default;
		
		}





	}
}
