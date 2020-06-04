using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using nTestSystem.DatabaseHelper;
using nTestSystem.Framework.Extensions;
using nTestSystem.DataHelper;
using nTestSystem.Framework.Attributes;

namespace nTestSystem
{
    /// <summary>
    /// Shell.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : Window, IDataHelper
    {
        public Shell()
        {
            InitializeComponent();

            var sql = new SqlExpression().Select<test>();

            var dd = this.GetData<test>(sql);

        }
    }


    [Database("nTestSystem") ]
    public class test 
    {
        [Column(0)]
        public string Guid { get; set; } = "";

        [Column(1)]
        public string PartNumber { get; set; } = "";

        [Column(2)]
        public string SerialNumber { get; set; } = "";
    
    }


}
