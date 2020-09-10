using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using nTestSystem.UserControls.Class;

namespace nTestSystem.UserControls.Controls
{
	public class FlatButton : Button
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type"
            , typeof(FlatButtonSkin), typeof(FlatButton));
        /// <summary>
        /// 鼠标悬浮时按钮的背景色
        /// </summary>
        public FlatButtonSkin Type
        {
            get { return (FlatButtonSkin)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        #region 按钮属性
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground"
            , typeof(Brush), typeof(FlatButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(79, 193, 233))));
        /// <summary>
        /// 鼠标悬浮时按钮的背景色
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground"
            , typeof(Brush), typeof(FlatButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(74, 137, 220))));
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(CornerRadius), typeof(FlatButton));
        /// <summary>
        /// 按钮四周圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackground1Property = DependencyProperty.Register("MouseOverBackground1"
            , typeof(Color), typeof(FlatButton), new FrameworkPropertyMetadata(Color.FromRgb(79, 193, 233)));
        /// <summary>
        /// 鼠标悬浮时按钮的背景色
        /// </summary>
        public Color MouseOverBackground1
        {
            get { return (Color)GetValue(MouseOverBackground1Property); }
            set { SetValue(MouseOverBackground1Property, value); }
        }
        #endregion

        static FlatButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatButton), new FrameworkPropertyMetadata(typeof(FlatButton)));
        }
    }
}
