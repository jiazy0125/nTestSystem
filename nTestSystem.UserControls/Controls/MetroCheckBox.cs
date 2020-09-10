using System.Windows;
using System.Windows.Controls;

namespace nTestSystem.UserControls.Controls
{
	public class MetroCheckBox : CheckBox
	{
		public static readonly DependencyProperty CheckBoxSizeProperty = DependencyProperty.RegisterAttached(
			"CheckBoxSize", typeof(double), typeof(MetroCheckBox), new PropertyMetadata(18d));

		public static void SetCheckBoxSize(DependencyObject element, double value) => element.SetValue(CheckBoxSizeProperty, value);
		public static double GetCheckBoxSize(DependencyObject element) => (double)element.GetValue(CheckBoxSizeProperty);
	}
}
