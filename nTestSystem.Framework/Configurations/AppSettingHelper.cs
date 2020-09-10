using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace nTestSystem.Framework.Configurations
{
	public class AppSettingHelper
	{
		private static Configuration cfa;
		public static string ConfigurationFile { private get; set; }

		private static void OpenCfg()
		{
			if (cfa is null)
			{
				if (ConfigurationFile is null)

					cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				else
					cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationFile);
			}
		}
		/// <summary>
		/// 读取key对应的value值
		/// </summary>
		/// <param name="key"></param>
		/// <returns>找不到key返回null</returns>
		public static string ReadKey(string key)
		{
			OpenCfg();
			if (!cfa.AppSettings.Settings.AllKeys.Contains(key))			
				return null;					
			else		
				return ConfigurationManager.AppSettings[key];
						
		}

		/// <summary>
		/// 读取key对应的value值,若无法找到key,则写入该Key及value
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ReadKey(string key, string value)
		{
			WriteKey(key, value, replacedExist: false);
			return ConfigurationManager.AppSettings[key];

		}

		/// <summary>
		/// 写入或新建key
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="replacedExist"></param>
		/// <param name="createNotExist">不存在则创建新key</param>
		public static void WriteKey(string key, string value, bool replacedExist = true, bool createNotExist = true)
		{
			OpenCfg();
			if (!cfa.AppSettings.Settings.AllKeys.Contains(key))
			{
				if (createNotExist)
					cfa.AppSettings.Settings.Add(key, value);

			}
			else
			{
				if (replacedExist)
					cfa.AppSettings.Settings[key].Value = value;
			}
			cfa.Save();

		}

		/// <summary>
		/// 更新多个key
		/// </summary>
		/// <param name="keys"></param>
		/// <param name="values"></param>
		public static void WriteKey(IEnumerable<string> keys, IEnumerable<string> values)
		{
			for (var i = 0; i < keys.Count(); i++)
			{
				WriteKey(keys.ElementAt(i), values.ElementAt(i));
			}
		}


	}
}
