using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nTestSystem.Framework.Extensions
{
	public static class GuidExtensions
	{
		/// <summary>
		/// 产生16位唯一GUID
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public static string Guid16(this Guid guid)
		{
			long i = 1;
			foreach (byte b in guid.ToByteArray())
			{
				i *= (b + 1);
			}
			return $"{string.Format("{0:x}", i - DateTime.Now.Ticks).ToUpper()}";

		}
	}
}
