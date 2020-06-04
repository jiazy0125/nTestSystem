using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Extensions;

namespace nTestSystem.DatabaseHelper
{
	public class SqlExpression : ISqlCommand, ISqlExpression
    {
        public string Command { get; private set; }
        public List<SqlParameter> Parameters { get; private set; }
        public SqlExpression()
        {
            Parameters = new List<SqlParameter>();
        }
        public SqlExpression(string sql)
        {
            Parameters = new List<SqlParameter>();
        }
        public override string ToString()
        {
            return Command;
        }
        public object[] ToParameters()
        {
            return Parameters?.ToArray();
        }

        /// <summary>
        /// 从数据库中选择满足条件的数据
        /// </summary>
        /// <typeparam name="T">欲查询的数据模型</typeparam>
        /// <param name="columns">欲查询的字段索引，可以为null</param>
        /// <param name="conditions">欲查询数据满足的条件</param>
        /// <returns>Sql表达式</returns>
        public SqlExpression Select<T>(int[] columns=null, ISqlCommand[] conditions = null)
        {

            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));
            var columnsStr = columns == null ? "*" : this.ColumnConnection<T>(columns);
            Command = $"select {columnsStr} from {dbAttr?.Table} ";
            return this.Where(conditions);

        }

        /// <summary>
        /// 删除数据库中的数据
        /// </summary>
        /// <typeparam name="T">欲删除数据的模型</typeparam>
        /// <param name="conditions">条件</param>
        /// <returns>SQL表达式</returns>
        public SqlExpression Delete<T>(ISqlCommand[] conditions)
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"delete from {dbAttr.Table} ";

            return this.Where(conditions);
        }

        /// <summary>
        /// 更新数据中的一条数据
        /// </summary>
        /// <typeparam name="T">欲更新数据的模型</typeparam>
        /// <param name="columns">字段索引</param>
        /// <param name="data">数据类</param>
        /// <param name="conditions">条件</param>
        /// <returns>SQL表达式</returns>
        public SqlExpression Update<T>(int[] columns, T data, ISqlCommand[] conditions)
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"update {dbAttr.Table} set {this.MultiEqualCondition(columns, data, Parameters)} ";


            return this.Where(conditions);

        }

        /// <summary>
        /// 更新数据中的一条数据
        /// </summary>
        /// <typeparam name="T">欲更新数据的模型</typeparam>
        /// <param name="columns">字段索引</param>
        /// <param name="values">字段值</param>
        /// <param name="conditions">条件</param>
        /// <returns>SQL表达式</returns>
        public SqlExpression Update<T>(int[] columns, object[] values, ISqlCommand[] conditions)
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"update {dbAttr.Table} set ";
            var i = 0;
            foreach (var column in columns)
            {
                var paraName = this.CreateParaID();
                Command += $"{this.Column<T>(column)} = {paraName}{(i == columns.Length - 1 ? "" : ",")} ";
                Parameters.Add(new SqlParameter(paraName, values?[i]));
                i++;
            }

            return this.Where(conditions);
        }

        /// <summary>
        /// 向数据库中插入一条数据
        /// </summary>
        /// <typeparam name="T">欲插入数据的模型</typeparam>
        /// <param name="columns">欲插入字段的索引</param>
        /// <param name="data">欲插入的数据，类型为class</param>
        /// <returns>Sql表达式</returns>
        public SqlExpression Insert<T>(int[] columns, T data)
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));
            var values = "";
            var cols = "";
            //拼接column和value
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly|BindingFlags.Public|BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                var attr = property?.GetCustomAttribute<ColumnAttribute>();

                if ((attr?.Flag & columns.ToFlags()) != 0 && attr != null)
                {
                    var paraName = this.CreateParaID();
                    //拼接column
                    cols = cols.Length > 0 ? $"{cols}, {property.Name} " : $"{property.Name}";
                    //拼接value
                    values = values.Length > 0 ? $"{values}, {paraName} " : $"{paraName}";

                    Parameters.Add(new SqlParameter(paraName, property.GetValue(data)));

                }
            }
            Command += $"insert into {dbAttr.Table} ({cols}) values ({values})";

            return this;
        }

        /// <summary>
        /// SQL条件语句整合
        /// </summary>
        /// <param name="conditions">条件集合</param>
        /// <returns>SQL表达式</returns>
        private SqlExpression Where(ISqlCommand[] conditions)
        {
            if (conditions == null) return this;
            string con = "";
            foreach (ISqlCommand isc in conditions)
            {

                con += isc.ToString();

                if (isc.Parameters?.Count > 0)
                    (Parameters = Parameters ?? new List<SqlParameter>()).AddRange(isc.Parameters);

            }
            if (con.Length > 0)
                Command += $"where {con} ";
            return this;
        }
    }
}
