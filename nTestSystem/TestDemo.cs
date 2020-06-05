using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nTestSystem.DatabaseHelper;
using nTestSystem.DataHelper;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Extensions;

namespace nTestSystem
{
	public class TestDemo:IDataHelper
	{

		public TestDemo()
		{
            var condition = new ConditionalExpression<Test>().Eq(0, "123");

            var sql = new SqlExpression().Select<Test>(new[] { 1 }, new[] { condition });

            var dd = this.GetData<Test>(sql);

            var data = new Test() { Guid = Guid.NewGuid().Guid16(), PartNumber = "Q123AS", SerialNumber = "SER1254" };

            sql = new SqlExpression().Insert(null, data);

            var res = this.SaveData(sql);

            var sqls = new List<ISqlCommand>();
            for (var i = 0; i < 10; i++)
            {
                data = new Test() { Guid = Guid.NewGuid().Guid16() };
                data.PartNumber = $"QSE234{i}";
                data.SerialNumber = $"SERIL{i}";
                sqls.Add(new SqlExpression().Insert(null, data));
            }
            var res1 = this.SaveData(sqls);

            condition = new ConditionalExpression<Test>().Eq(2, "SER1254");
            sql = new SqlExpression().Update<Test>(new[] { 1 }, new[] { "Test1" }, new[] { condition });
            res = this.SaveData(sql);

            condition = new ConditionalExpression<Test>().Eq(2, "SERIL0");
            data = new Test() { Guid = Guid.NewGuid().Guid16(), PartNumber = "Test2" };
            sql = new SqlExpression().Update(new[] { 0, 1 }, data, new[] { condition });
            res = this.SaveData(sql);
            sqls.Clear();
            for (var i = 0; i < 10; i++)
            {
                condition = new ConditionalExpression<Test>().Eq(2, $"SERIL{i}");
                sqls.Add(new SqlExpression().Update<Test>(new[] { 1 }, new[] { $"TTTTTT{i}" }, new[] { condition }));
            }
            res1 = this.SaveData(sqls);


            condition = new ConditionalExpression<Test>().Eq(2, "SERIL0");
            sql = new SqlExpression().Delete<Test>(new[] { condition });
            res = this.SaveData(sql);

            sqls.Clear();
            for (var i = 0; i < 10; i++)
            {
                condition = new ConditionalExpression<Test>().Eq(2, $"SERIL{i}");
                sqls.Add(new SqlExpression().Delete<Test>(new[] { condition }));
            }
            res1 = this.SaveData(sqls);



        }


	}

    [Database("nTestSystem")]
    public class Test
    {
        [Column(0)]
        public string Guid { get; set; } = "";
        [Column(1)]
        public string PartNumber { get; set; } = "";
        [Column(2)]
        public string SerialNumber { get; set; } = "";
    }
}
