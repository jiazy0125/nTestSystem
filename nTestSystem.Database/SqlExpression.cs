using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using nTestSystem.Framework.Attributes;

namespace nTestSystem.Database
{
	public class SqlExpression : ISqlExpression
	{
        public string Command { get; private set; }
        public List<SqlParameter> Parameters { get; private set; }

        public SqlExpression()
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
        /// 查询指定字段信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlExpression Select<T>(int[] columns)
        {

            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));
            var columnsStr = columns == null ? "*" : this.ColumnConnection<T>(columns);
            Command = $"select {columnsStr} from {dbAttr?.Table} ";
            return this;

        }

        /// <summary>
        /// 查询指定表字段信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public SqlExpression Select<T>(string table, int[] columns)
        {
            var columnsStr = columns == null ? "*" : this.ColumnConnection<T>(columns);
            Command = $"select {columnsStr} from {table} ";
            return this;
        }

        public SqlExpression Select(string table, string[] columns)
        {
            var tempCol = "";
            foreach (var item in columns)
            {
                tempCol += $"{(tempCol.Length <= 0 ? "" : ",")} {item}";
            }
            if (tempCol.Length == 0) tempCol = "*";                                                             
            Command = $"select {tempCol} from {table} ";
            return this;
        }
        /// <summary>
        /// 删除表中的内容，需要结合Where条件函数一起使用                    
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SqlExpression Delete<T>()
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"delete from {dbAttr.Table} ";

            return this;
        }
        /// <summary>
        /// 更新表中数据,需要结合Set函数一起使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public SqlExpression Update<T>()
        {
            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"update {dbAttr.Table} ";


            return this;


        }

        public SqlExpression Set<T>(int[] columns, T data)
        {
            Command += $"set {this.MultiEqualCondition(columns, data, Parameters)} ";

            return this;

        }
        public SqlExpression Set<T>(int[] columns, object[] values)
        {
            Command += "set ";
            var i = 0;
            foreach (var column in columns)
            {
                var paraName = this.CreateParaID();
                Command += $"{this.Column<T>(column)} = {paraName}{(i == columns.Length - 1 ? "" : ",")} ";
                Parameters.Add(new SqlParameter(paraName, values?[i]));
                i++;
            }


            return this;
        }
        /// <summary>
        /// 向数据库中插入数据,需要结合values函数使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <returns></returns>
        public SqlExpression Insert<T>()
        {

            var dbAttr = (DatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseAttribute));

            Command = $"insert into {dbAttr.Table} ";

            return this;
        }

        public SqlExpression Values<T>(int[] columnsIndex, T data)
        {
            var values = "";
            var cmd = "";
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attr = property.GetCustomAttribute<ColumnAttribute>();

                if ((attr?.Flag & this.ToFlag(columnsIndex)) != 0 && attr != null)
                {
                    var paraName = this.CreateParaID();

                    cmd = cmd.Length > 0 ? $"{cmd}, {attr.Name} " : $"{attr.Name}";
                    values = values.Length > 0 ? $"{values}, {paraName} " : $"{paraName}";

                    Parameters.Add(new SqlParameter(paraName, property.GetValue(data)));

                }
            }
            Command += $"({cmd}) values ({values})";


            return this;

        }


        public SqlExpression Where(ISqlCommand[] conditions)
        {
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
