using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using nTestSystem.Framework.Attributes;
using nTestSystem.Framework.Commons;

namespace nTestSystem.DatabaseHelper
{
	public interface ISqlCommand
	{
        //重写ToString功能
        string ToString();
        //SQL表达式
        string Command { get; }
        //SQL采用参数绑定方式
        List<SqlParameter> Parameters { get; }
    }

}
