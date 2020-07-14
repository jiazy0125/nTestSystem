using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace nTestSystem.Framework.Converters
{
	public class ExpanderVisibilityConverters : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return Visibility.Collapsed;
			var child = (UIElementCollection)value;
			if (child.Count > 0) return Visibility.Visible;
			return Visibility.Collapsed;
			
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null; 
		}
	}
}
