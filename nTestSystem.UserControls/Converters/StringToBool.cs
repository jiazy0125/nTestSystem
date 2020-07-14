using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace nTestSystem.UserControls.Converters
{
	class StringToBool : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || string.IsNullOrEmpty(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
