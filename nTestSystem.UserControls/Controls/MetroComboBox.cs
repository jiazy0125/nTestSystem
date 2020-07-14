using nTestSystem.UserControls.Class;
using System.Windows;
using System.Windows.Controls;

namespace nTestSystem.UserControls.Controls
{

	public class MetroComboBox : ComboBox
    {
        public MetroComboBox()
        {
            ///Utility.Refresh(this);
        }
        static MetroComboBox()
        {
			ElementBase.DefaultStyle<MetroComboBox>(DefaultStyleKeyProperty);
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(CornerRadius), typeof(MetroComboBox));
        /// <summary>
        /// 按钮四周圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
