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
        /// int索引值转化为ColumnFlags标志位
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="column">字段索引</param>
        /// <returns></returns>
        public static ColumnFlags ToFlag<T>(this T obj) where T : IComparable<int>
        {
            var column = (int)(object)obj;
            return (ColumnFlags)(1 << column);
        }
        /// <summary>
        /// 多个int索引值转化为ColumnFlags标志位
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="columns">多个字段索引</param>
        /// <returns></returns>
        public static ColumnFlags ToFlags<T>(this T obj) where T : IEnumerable<int>
        {
            var columns = (int[])(object)obj;
            var cf = (ColumnFlags)0;

            if (columns == null | columns?.Length == 0) return (ColumnFlags)(-1);
            foreach (var ci in columns)
            {
                cf |= (ColumnFlags)(1 << ci);
            }          
            return cf;        
        }

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
