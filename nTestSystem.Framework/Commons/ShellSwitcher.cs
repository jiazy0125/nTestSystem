﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace nTestSystem.Framework.Commons
{
	public class ShellSwitcher
	{
        public static void Switch<TClosed, TShow>() where TClosed : Window where TShow : Window, new()
        {
            Show<TShow>();
            Closed<TClosed>();
        }

        public static void Show<T>(T window = null) where T : Window, new()
        {
            var shell = Application.Current.MainWindow = window ?? new T();
            shell?.Show();
        }

        public static void Closed<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell?.Close();
        }

        public static void Hide<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell?.Hide();
        }

        public static void Standard<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell?.Show();
        }
    }
}
