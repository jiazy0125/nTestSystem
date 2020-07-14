using nTestSystem.UserControls.Class;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace nTestSystem.UserControls.Controls
{
	public class MetroPasswordBox : TextBox
    {
        #region private fields

        private ToggleButton PART_SeePassword;

        /// <summary>
        /// 该属性是为了防止明文转化为密文后，设置Text时，再次触发Text_Changed事件
        /// </summary>
        private bool mIsHandledTextChanged = true;
        private StringBuilder mPasswordBuilder;

        #endregion

        #region DependencyProperty
        #region Watermark

        /// <summary>
        /// 获取或者设置水印
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(MetroPasswordBox));

        #endregion
        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MetroPasswordBox), new PropertyMetadata(CornerRadiusChanged));

        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MetroPasswordBox textbox = d as MetroPasswordBox;
            if (textbox != null && e.NewValue != null)
            {
                textbox.OnCornerRadiusChanged((CornerRadius)e.NewValue);
            }
        }
        public void OnCornerRadiusChanged(CornerRadius newValue)
        {
            //根据密码框边框圆角自动设置图标背景框圆角
            this.IconCornerRadius = new CornerRadius(newValue.TopLeft, 0, 0, newValue.BottomLeft);
        }

        #endregion
        #region IsShowIcon

        /// <summary>
        /// 获取或者设置是否显示图标
        /// </summary>
        [Bindable(true), Description("获取或者设置是否显示图标")]
        public bool IsShowIcon
        {
            get { return (bool)GetValue(IsShowIconProperty); }
            set { SetValue(IsShowIconProperty, value); }
        }

        public static readonly DependencyProperty IsShowIconProperty =
            DependencyProperty.Register("IsShowIcon", typeof(bool), typeof(MetroPasswordBox), new PropertyMetadata(true));

        #endregion

        #region IconBackground

        /// <summary>
        /// 获取或者设置图标边框背景色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框背景色")]
        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register("IconBackground", typeof(Brush), typeof(MetroPasswordBox));

        #endregion

        #region IconForeground

        /// <summary>
        /// 获取或者设置图标的颜色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的颜色")]
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); }
            set { SetValue(IconForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(MetroPasswordBox));

        #endregion

        #region IconBorderBrush

        /// <summary>
        /// 获取或者设置图标边框的颜色
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框背景色")]
        public Brush IconBorderBrush
        {
            get { return (Brush)GetValue(IconBorderBrushProperty); }
            set { SetValue(IconBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty IconBorderBrushProperty =
            DependencyProperty.Register("IconBorderBrush", typeof(Brush), typeof(MetroPasswordBox));

        #endregion

        #region IconBorderThickness

        /// <summary>
        /// 获取或者设置图标边框的粗细与大小
        /// </summary>
        public Thickness IconBorderThickness
        {
            get { return (Thickness)GetValue(IconBorderThicknessProperty); }
            set { SetValue(IconBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty IconBorderThicknessProperty =
            DependencyProperty.Register("IconBorderThickness", typeof(Thickness), typeof(MetroPasswordBox));

        #endregion

        #region IconWidth

        /// <summary>
        /// 获取或者设置图标的大小
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的大小")]
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(MetroPasswordBox));

        #endregion

        #region IconPadding

        /// <summary>
        /// 获取或者设置图标的内边距
        /// </summary>
        [Bindable(true), Description("获取或者设置图标的内边距")]
        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly DependencyProperty IconPaddingProperty =
            DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(MetroPasswordBox));

        #endregion

        #region IconCornerRadius

        /// <summary>
        /// 获取或者设置图标边框的圆角（可以不用手动设置，系统会根据密码框的圆角值自动设置该值）
        /// </summary>
        [Bindable(true), Description("获取或者设置图标边框的圆角（可以不用手动设置，系统会根据密码框的圆角值自动设置该值）")]
        public CornerRadius IconCornerRadius
        {
            get { return (CornerRadius)GetValue(IconCornerRadiusProperty); }
            set { SetValue(IconCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty IconCornerRadiusProperty =
            DependencyProperty.Register("IconCornerRadius", typeof(CornerRadius), typeof(MetroPasswordBox));

        #endregion

        #region IconPathData

        /// <summary>
        /// 获取或者设置密码框图标
        /// </summary>
        [Bindable(true), Description("获取或者设置密码框图标")]
        public PathGeometry IconPathData
        {
            get { return (PathGeometry)GetValue(IconPathDataProperty); }
            set { SetValue(IconPathDataProperty, value); }
        }

        public static readonly DependencyProperty IconPathDataProperty =
            DependencyProperty.Register("IconPathData", typeof(PathGeometry), typeof(MetroPasswordBox));

        #endregion
        #region IsCanSeePassword

        /// <summary>
        /// 获取或者设置是否能看见密码
        /// </summary>
        [Bindable(true), Description("获取或者设置是否能看见密码")]
        public bool IsCanSeePassword
        {
            get { return (bool)GetValue(IsCanSeePasswordProperty); }
            set { SetValue(IsCanSeePasswordProperty, value); }
        }

        public static readonly DependencyProperty IsCanSeePasswordProperty =
            DependencyProperty.Register("IsCanSeePassword", typeof(bool), typeof(MetroPasswordBox), new PropertyMetadata(true, IsCanSeePasswordChangedCallback));

        private static void IsCanSeePasswordChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			if (d is MetroPasswordBox passowrdBox && passowrdBox.PART_SeePassword != null)
			{
				passowrdBox.PART_SeePassword.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
			}
		}

        #endregion

        #region Password

        /// <summary>
        /// 获取或者设置当前密码值
        /// </summary>
        [Bindable(true), Description("获取或者设置当前密码值")]
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(MetroPasswordBox), new PropertyMetadata(string.Empty));

        #endregion

        #region PasswordChar

        /// <summary>
        /// 获取或者设置PasswordBox的屏蔽字符
        /// </summary>
        [Bindable(true), Description("获取或者设置PasswordBox的屏蔽字符")]
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(MetroPasswordBox), new PropertyMetadata('●'));

        #endregion

        #endregion

        #region Private DependencyProperty

        #region ShowPassword
        public static readonly DependencyProperty MouseMoveBackgroundProperty = ElementBase.Property<MetroPasswordBox, Brush>("MouseMoveBackgroundProperty");
        public Brush MouseMoveBackground { get { return (Brush)GetValue(MouseMoveBackgroundProperty); } set { SetValue(MouseMoveBackgroundProperty, value); } }
        public bool ShowPassword
        {
            get { return (bool)GetValue(ShowPasswordProperty); }
            private set { SetValue(ShowPasswordProperty, value); }
        }

        public static readonly DependencyProperty ShowPasswordProperty =
            DependencyProperty.Register("ShowPassword", typeof(bool), typeof(MetroPasswordBox), new PropertyMetadata(false, ShowPasswordChanged));

        private static void ShowPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			if (d is MetroPasswordBox passwordBox)
			{
				passwordBox.SelectionStart = passwordBox.Text.Length + 1;
			}
		}

        #endregion

        #endregion

        #region Constructors
        public MetroPasswordBox()
        {
            Utility.Refresh(this);
        }

        static MetroPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroPasswordBox), new FrameworkPropertyMetadata(typeof(MetroPasswordBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_SeePassword = GetTemplateChild("PART_SeePassword") as ToggleButton;
            if (PART_SeePassword != null)
            {
                PART_SeePassword.Visibility = IsCanSeePassword ? Visibility.Visible : Visibility.Collapsed;
            }
            SetEvent();

            //回显密码
            SetText(ConvertToPasswordChar(Password.Length));

            //密码框禁止复制
            CommandBindings.Add(new System.Windows.Input.CommandBinding(ApplicationCommands.Copy, CommandBinding_Executed, CommandBinding_CanExecute));
        }

        //public override void OnCornerRadiusChanged(CornerRadius newValue)
        //{
        //    //根据密码框边框圆角自动设置图标背景框圆角
        //    this.IconCornerRadius = new CornerRadius(newValue.TopLeft, 0, 0, newValue.BottomLeft);
        //}
        #endregion

        #region private function

        private void SetEvent()
        {
            this.TextChanged += ZPasswordBox_TextChanged;
            if (PART_SeePassword != null)
            {
                PART_SeePassword.Checked += (o, e) =>
                {
                    SetText(Password);
                    ShowPassword = true;
                };
                PART_SeePassword.Unchecked += (o, e) =>
                {
                    SetText(ConvertToPasswordChar(Password.Length));
                    ShowPassword = false;
                };
            }
        }

        private void SetText(string str)
        {
            mIsHandledTextChanged = false;
            Text = str;
            mIsHandledTextChanged = true;
        }

        /// <summary>
        /// 明文密码转化为特定字符
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToPasswordChar(int length)
        {
            if (mPasswordBuilder != null)
            {
                mPasswordBuilder.Clear();
            }
            else
            {
                mPasswordBuilder = new StringBuilder();
            }

            for (var i = 0; i < length; i++)
            {
                mPasswordBuilder.Append(PasswordChar);
            }

            return mPasswordBuilder.ToString();
        }

        #endregion

        #region Event Implement Function

        private void ZPasswordBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!mIsHandledTextChanged)
                return;

            foreach (TextChange c in e.Changes)
            {
                //从密码文中根据本次Change对象的索引和长度删除对应个数的字符
                Password = Password.Remove(c.Offset, c.RemovedLength);
                //将Text新增的部分记录给密码文
                Password = Password.Insert(c.Offset, Text.Substring(c.Offset, c.AddedLength));
            }

            if (!ShowPassword)
            {
                SetText(ConvertToPasswordChar(Text.Length));
            }

            //将光标放到最后面
            SelectionStart = Text.Length + 1;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = true;
        }
        #endregion
    }
}
