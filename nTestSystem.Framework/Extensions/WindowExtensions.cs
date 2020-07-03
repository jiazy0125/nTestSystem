using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace nTestSystem.Framework.Extensions
{
	public static class WindowExtensions
	{
		/// <summary>
		/// 设置应用程序窗口居中
		/// </summary>
		/// <param name="win"></param>
		public static void CenterWindowOnScreen(this Window win)
		{

			//获取当前屏幕
			Screen currentMonitor = Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(win).Handle);
			//判断程序是否被屏幕进行比例缩放
			PresentationSource source = PresentationSource.FromVisual(win);
			double dpiScaling = (source != null && source.CompositionTarget != null ? source.CompositionTarget.TransformFromDevice.M11 : 1);

			//获取屏幕可用区域，除去任务栏
			Rectangle workArea = currentMonitor.WorkingArea;
			var workAreaWidth = (int)Math.Floor(workArea.Width * dpiScaling);
			var workAreaHeight = (int)Math.Floor(workArea.Height * dpiScaling);

			//设置窗口位置
			win.Left = (((workAreaWidth - (win.Width * dpiScaling)) / 2) + (workArea.Left * dpiScaling));
			win.Top = (((workAreaHeight - (win.Height * dpiScaling)) / 2) + (workArea.Top * dpiScaling));

		}

	}
}
