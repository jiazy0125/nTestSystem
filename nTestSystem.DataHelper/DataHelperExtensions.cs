using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using nTestSystem.DatabaseHelper;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Commons;
using nTestSystem.Framework.Extensions;


namespace nTestSystem.DataHelper
{
	public static class DataHelperExtensions
	{

		/// <summary>
		/// 从数据库中获取数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataHelper"></param>
		/// <param name="sql">SQL语句</param>
		/// <returns></returns>
		public static Result<List<T>> GetData<T>(this IDataHelper dataHelper, ISqlCommand sql)
		{
			var lt = new List<T>();
			try
			{
				DataTable dt = DbFactory.Execute().ExecuteTable(sql.ToString(), CommandType.Text, sql.Parameters?.ToArray());
				if (dt.Rows.Count > 0)
				{
					foreach (DataRow dr in dt.Rows)
					{
						var model = (T)Assembly.Load(typeof(T).Namespace).CreateInstance(typeof(T).FullName);
						//仅查找类本身声明的属性，不包括继承
						//PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
						foreach (DataColumn dc in dr.Table.Columns)
						{
							model.GetType().GetProperty(dc.ColumnName)?.SetValue(model, dr[dc.ColumnName]);
						}

						lt.Add(model);
					}
				}
				return Result<List<T>>.Success(data:lt);
			}
			catch (Exception e)
			{
				return Result<List<T>>.Error(e.Message);
			}		
		}
		/// <summary>
		/// 向数据库中插入数据、更新数据、删除数据
		/// </summary>
		/// <param name="dataHelper"></param>
		/// <param name="sql">SQL语句表达式</param>
		/// <returns>返回受影响的行数</returns>
		public static Result<int> SaveData(this IDataHelper dataHelper, ISqlCommand sql)
		{
			var res = DbFactory.Execute().ExecuteNonQuery(sql.Command, CommandType.Text, sql.Parameters?.ToArray());
			return Result<int>.Success(data: res);			
		}
		/// <summary>
		/// 向数据库中插入多条数据、更新多条数据、删除多条数据,采用事务模式
		/// </summary>
		/// <param name="dataHelper"></param>
		/// <param name="sqls">SQL语句集合</param>
		/// <returns>返回受影响的行数</returns>
		public static Result<Exception> SaveData(this IDataHelper dataHelper, IEnumerable<ISqlCommand> sqls)
		{
			try
			{
				var db = DbFactory.Execute();
				db.BeginTransaction();

				foreach (ISqlCommand sql in sqls)
				{
					db.ExecuteNonQuery(sql.Command, CommandType.Text, sql.Parameters?.ToArray());
				}

				db.Commit();
				return Result<Exception>.Success();
			}
			catch (Exception exp)
			{
				return Result<Exception>.Error(exp.Message, exp);
			}
			
		}
	}
}
