using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using nTestSystem.DataHelper;
namespace nTestSystem
{
	/// <summary>
	/// Shell.xaml 的交互逻辑

	/// </summary>
	public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
			dd();
        }

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();

		}
		private void dd()
		{

		}

	}

}
