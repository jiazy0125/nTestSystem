using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Commons;
using nTestSystem.Framework.Extensions;

namespace nTestSystem.DatabaseHelper
{
	/// <summary>
	/// ISqlCommand扩展方法
	/// </summary>
	public static class SqlExpressionExtensions
    {

        /// <summary>
        /// 返回数据模型对应数据库中的多个字段
        /// </summary>
        /// <typeparam name="T">数据模型</typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="columns">欲查询的字段索引</param>
        /// <returns></returns>
        public static List<string> Columns<T>(this ISqlExpression sql, int[] columns)
        {

            var lt = new List<string>();
            //只获取类本身定义的属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                try
                {
                    //只处理被标记为Column特性的属性
                    var attr = property?.GetCustomAttribute<ColumnAttribute>();
                    if ((attr?.Flag & columns.ToFlags()) != 0 && attr != null)
                        lt.Add(property.Name);
                }
                catch { }
            }
            return lt;
        }

        /// <summary>
        /// 返回数据模型对应数据库中的一个字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string Column<T>(this ISqlExpression sql, int column)
        {
            //只获取类本身定义的属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                try
                {
                    //只处理被标记为Column特性的属性
                    var attr = property?.GetCustomAttribute<ColumnAttribute>();

                    if ((attr?.Flag | column.ToFlag()) != 0 && attr != null)
                        return property.Name;
                }
                catch { }
            }
            return null;
        }

        /// <summary>
        /// 创建唯一的绑定ID
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public static string CreateParaID(this ISqlExpression sql)
        {
            //return $"@{Guid.NewGuid():N}";

            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= (b + 1);
            }
            return $"@{string.Format("{0:x}", i - DateTime.Now.Ticks)}";
        }

        /// <summary>
        /// 数据模型中对应数据库字段拼接，格式xx, xx
        /// </summary>
        /// <typeparam name="T">欲拼接的数据模型</typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="columns">多个列索引</param>
        /// <returns></returns>
        public static string ColumnConnection<T>(this ISqlExpression sql, int[] columns)
        {

            string columnStr = null;
            //只处理类本身定义的属性字段
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                //只处理被标记为Column特性的属性字段
                var attr = property?.GetCustomAttribute<ColumnAttribute>();

                if ((attr?.Flag & columns.ToFlags()) != 0 && attr != null)
                    columnStr = columnStr == null ? property.Name : columnStr + "," + property.Name;
            }

            return columnStr;
        }

        /// <summary>
        /// 多个条件语句拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="columns">欲拼接的字段索引</param>
        /// <param name="data">欲拼接的数据</param>
        /// <param name="paras">欲拼接的Sql参数</param>
        /// <param name="conn">欲拼接的链接方式</param>
        /// <returns></returns>
        public static string MultiEqualCondition<T>(this ISqlExpression sql, int[] columns, T data, List<SqlParameter> paras, Connection conn = Connection.Default)
        {
            var str = "";
            var connStr = conn == Connection.Default ? "," : conn.ToString();
            //只处理类本身定义的属性字段
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                //只处理被标记为Column特性的属性字段
                var attr = property.GetCustomAttribute<ColumnAttribute>();

                if ((attr?.Flag & columns.ToFlags()) != 0 && attr != null)
                {
                    var paraName = CreateParaID(null);

                    str = str.Length > 0 ? $"{str} {connStr} {property.Name}={paraName} " : $"{property.Name}={paraName}";

                    paras.Add(new SqlParameter(paraName, property.GetValue(data)));

                }
            }

            return str;
        }


    }
}
