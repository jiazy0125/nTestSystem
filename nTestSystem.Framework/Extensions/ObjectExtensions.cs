using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using nTestSystem.Framework.Commons;

namespace nTestSystem.Framework.Extensions
{
	/// <summary>
	/// Object扩展方法库
	/// </summary>
	public static class ObjectExtensions
	{
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <typeparam name="T">类型为string</typeparam>
        /// <param name="obj"></param>
        /// <returns>经过MD5加密的字符串</returns>
        public static string GenerateMD5<T>(this T obj) where T: IComparable<string>
        {
            var txt = (string)(object)obj;
            using (var mi = MD5.Create())
            {
                var buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                var newBuffer = mi.ComputeHash(buffer);
                var sb = new StringBuilder();
                for (var i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }



    }
}
