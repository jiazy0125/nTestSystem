using nTestSystem.Framework.Commons;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace nTestSystem.DatabaseHelper
{
	public class ConditionalExpression<T> : ISqlCommand, ISqlExpression
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
        /// 条件连接语句，And
        /// </summary>
        /// <returns></returns>
        public ConditionalExpression<T> And()
        {
            Command += "and ";

            return this;
        }
        /// <summary>
        /// Between条件语句
        /// </summary>
        /// <param name="column">字段索引</param>
        /// <param name="minValue">区间下限值</param>
        /// <param name="maxValue">区间上限值</param>
        /// <returns></returns>
        public ConditionalExpression<T> Between(int column, object minValue, object maxValue)
        {
            var min = this.CreateParaID();
            var max = this.CreateParaID();
            Command += $"{this.Column<T>(column)} between {min} and {max} ";

            Parameters.Add(new SqlParameter(min, minValue));
            Parameters.Add(new SqlParameter(max, maxValue));

            return this;

        }
        /// <summary>
        /// 单个字段等于条件语句
        /// </summary>
        /// <param name="column">字段索引</param>
        /// <param name="value">条件值</param>
        /// <returns></returns>
        public ConditionalExpression<T> Eq(int column, object value)
        {
            var paraName = this.CreateParaID();
            Command += $"{this.Column<T>(column)} = {paraName} ";

            Parameters.Add(new SqlParameter(paraName, value));

            return this;

        }
        /// <summary>
        /// 单个字段等于条件语句
        /// </summary>
        /// <param name="column">字段名称</param>
        /// <param name="value">条件值</param>
        /// <returns></returns>
        public ConditionalExpression<T> Eq(string column, object value)
        {
            var paraName = this.CreateParaID();
            Command += $"{column} = {paraName} ";
            Parameters.Add(new SqlParameter(paraName, value));
            return this;

        }

        /// <summary>
        /// 多个字段等于条件语句
        /// </summary>
        /// <param name="columns">字段索引值集合</param>
        /// <param name="data">数据</param>
        /// <param name="conn">相等条件连接方式</param>
        /// <returns></returns>
        public ConditionalExpression<T> Eq(int[] columns, T data, Connection conn = Connection.And)
        {
            Command += this.MultiEqualCondition(columns, data, Parameters, conn);

            return this;
        }

        /// <summary>
        /// in条件语句
        /// </summary>
        /// <param name="column">字段索引</param>
        /// <param name="values">条件值集合</param>
        /// <returns></returns>
        public ConditionalExpression<T> In(int column, object[] values)
        {

            var paraName = this.CreateParaID();
            Command += $"{this.Column<T>(column)} in ({paraName}) ";
            Parameters.Add(new SqlParameter(paraName, values));

            return this;
        }
        /// <summary>
        /// in条件语句
        /// </summary>
        /// <param name="column">字段索引</param>
        /// <param name="sql">条件语句</param>
        /// <returns></returns>
        public ConditionalExpression<T> In(int column, SqlExpression sql)
        {
            Command += $"{this.Column<T>(column)} in ({sql.Command}) ";
            Parameters.AddRange(sql.Parameters);
            return this;
        }

        /// <summary>
        /// Like条件语句
        /// </summary>
        /// <param name="column">字段索引</param>
        /// <param name="value">条件值</param>
        /// <param name="like">符合like条件的位置</param>
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
        /// <param name="column">字段索引</param>
        /// <param name="value">条件值</param>
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
        /// <summary>
        /// SQL排序条件
        /// </summary>
        /// <param name="columns">需要进行排序的字段索引</param>
        /// <param name="seq">排序方式</param>
        /// <returns></returns>
        public ConditionalExpression<T> OrderBy(int[] columns, Sequence seq)
        {
            Command += $"order by {this.ColumnConnection<T>(columns)} {seq}";

            return this;
        }
        /// <summary>
        /// 重写操作符&
        /// </summary>
        /// <param name="cexp1">条件表达式1</param>
        /// <param name="cexp2">条件表达式2</param>
        /// <returns></returns>
        public static ConditionalExpression<T> operator &(ConditionalExpression<T> cexp1, ConditionalExpression<T> cexp2)
        {
            cexp1.Command += $" and {cexp2.Command} ";
            cexp1.Parameters.AddRange(cexp2.Parameters);
            return cexp1;
        }
        /// <summary>
        /// 重写操作符|
        /// </summary>
        /// <param name="cexp1">条件表达式1</param>
        /// <param name="cexp2">条件表达式2</param>
        /// <returns></returns>
        public static ConditionalExpression<T> operator |(ConditionalExpression<T> cexp1, ConditionalExpression<T> cexp2)
        {
            cexp1.Command += $" or {cexp2.Command} ";
            cexp1.Parameters.AddRange(cexp2.Parameters);
            return cexp1;
        }
    }


}
