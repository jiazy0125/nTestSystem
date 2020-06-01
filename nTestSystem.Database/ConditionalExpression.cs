using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nTestSystem.Framework.Commons;

namespace nTestSystem.Database
{
	public class ConditionalExpression<T> : ISqlExpression
    {
        public string Command { get; private set; }
        public List<SqlParameter> Parameters { get; private set; }

        public ConditionalExpression()
        {
            Parameters = new List<SqlParameter>();
        }

        public override string ToString()
        {
            return Command;
        }
        /// <summary>
        /// 条件连接语句,and
        /// </summary>
        /// <returns></returns>
        public ConditionalExpression<T> And()
        {

            Command += "and ";

            return this;


        }
        /// <summary>
        /// 单字段操作函数,Between条件语句
        /// </summary>
        /// <param name="column"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public ConditionalExpression<T> Between(int column, object minValue, object maxValue)
        {
            var min = this.CreateParaID();
            var max = this.CreateParaID();
            Command += $"{this.Column<T>(column)} between {min} and {max} ";

            Parameters.Add(new SqlParameter(min, minValue));
            Parameters.Add(new SqlParameter(max, maxValue));

            return this; ;


        }
        /// <summary>
        /// 单字段操作函数,等于条件语句
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionalExpression<T> Eq(int column, object value)
        {
            var paraName = this.CreateParaID();
            Command += $"{this.Column<T>(column)} = {paraName} ";

            Parameters.Add(new SqlParameter(paraName, value));

            return this;

        }
        public ConditionalExpression<T> Eq(string column, object value)
        {
            var paraName = this.CreateParaID();
            Command += $"{column} = {paraName} ";
            Parameters.Add(new SqlParameter(paraName, value));
            return this;

        }


        public ConditionalExpression<T> Eq(int[] columns, T data, Connection conn = Connection.And)
        {
            Command += this.MultiEqualCondition(columns, data, Parameters, conn);

            return this;
        }




        /// <summary>
        /// 单字段操作函数,字段相等条件语句
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public ConditionalExpression<T> EqColumn(int column1, int column2)
        {
            Command += $"{this.Column<T>(column1)} = {this.Column<T>(column2)} ";
            return this;

        }
        /// <summary>
        /// 单字段操作函数,In条件语句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ConditionalExpression<T> In(int column, object[] values)
        {

            var paraName = this.CreateParaID();
            Command += $"{this.Column<T>(column)} in ({paraName}) ";
            Parameters.Add(new SqlParameter(paraName, values));

            return this;
        }
        /// <summary>
        /// 单字段操作函数,In条件语句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ConditionalExpression<T> In(int column, SqlExpression sql)
        {
            Command += $"{this.Column<T>(column)} in ({sql.Command}) ";
            Parameters.AddRange(sql.Parameters);
            return this;
        }
        /// <summary>
        /// 单字段操作函数,Like条件语句
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="like"></param>
        /// <returns></returns>
        public ConditionalExpression<T> Like(int column, string value, SqlLike like)
        {
            var paraName = this.CreateParaID();

            switch (like)
            {
                case SqlLike.StartWith:
                    value += "%";
                    break;
                case SqlLike.EndWith:
                    value = "%" + value;
                    break;
                case SqlLike.AnyWhere:
                    value = "%" + value + "%";
                    break;
                default: break;
            }

            Command += $"{this.Column<T>(column)} like {paraName} ";
            Parameters.Add(new SqlParameter(paraName, value));


            return this;
        }
        /// <summary>
        /// 单字段操作函数,Like条件语句
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ConditionalExpression<T> Like(int column, string value)
        {

            Command += $"{this.Column<T>(column)} like {value} ";

            return this;
        }
        /// <summary>
        /// 条件连接语句,or
        /// </summary>
        /// <returns></returns>
        public ConditionalExpression<T> Or()
        {
            Command += "or ";
            return this;
        }

        public ConditionalExpression<T> OrderBy(int[] columns, Sequence seq)
        {
            Command += $"order by {this.ColumnConnection<T>(columns)} {seq}";

            return this;
        }

        public static ConditionalExpression<T> operator &(ConditionalExpression<T> cexp1, ConditionalExpression<T> cexp2)
        {
            cexp1.Command += $" and {cexp2.Command} ";
            cexp1.Parameters.AddRange(cexp2.Parameters);
            return cexp1;
        }
        public static ConditionalExpression<T> operator |(ConditionalExpression<T> cexp1, ConditionalExpression<T> cexp2)
        {
            cexp1.Command += $" or {cexp2.Command} ";
            cexp1.Parameters.AddRange(cexp2.Parameters);
            return cexp1;
        }
    }


}
