using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nTestSystem.DatabaseHelper
{
	public class BaseHelper :IDatabaseHelper
	{
        protected string ConnString = ConfigurationManager.AppSettings["Connection"];

        #region 常规查询
        public virtual int ExecuteNonQuery(string sql, CommandType type, object[] para)
        {
            return 0;
        }

        public virtual DataTable ExecuteTable(string sql, CommandType type, object[] para)
        {
            return null;
        }

        public virtual DataTable ExecuteTableByPage(int pageSize, int pageIndex, string sql, CommandType type, object[] para)
        {
            return null;
        }
        #endregion

        #region 事务处理
        public bool IsInTransaction
        {
            get;
            set;
        }
        public bool IsDirty
        {
            get;
            set;
        }
        public virtual void BeginTransaction()
        {
            IsInTransaction = true;
        }

        public virtual void Commit()
        {
            IsInTransaction = false;
            IsDirty = false;
        }

        public virtual void Rollback()
        {
            IsInTransaction = false;
            IsDirty = false;
        }
        #endregion
    }
}
