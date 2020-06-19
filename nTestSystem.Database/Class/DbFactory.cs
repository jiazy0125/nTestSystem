using System.Reflection;
using System.Configuration;

namespace nTestSystem.DatabaseHelper
{
    /// <summary>
    /// 数据库工厂模式创建接口
    /// </summary>
    
	public class DbFactory
	{
        
        //IDatabaseHelper实例创建
        public static IDatabaseHelper Execute()
        {
            var className = ConfigurationManager.AppSettings["DBHelperName"]; 
            var np = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            var fullName = $"{ np }.{ className }";
            var result = (IDatabaseHelper)Assembly.Load(np).CreateInstance(fullName);
            return result;
        }
    }
}
