using System.Windows;
using System.Windows.Data;
using Prism.Services.Dialogs;

namespace DialogModule.Views
{
	/// <summary>
	/// Interaction logic for DialogWindow.xaml
	/// </summary>
	public partial class DialogWindow : IDialogWindow
	{
		public DialogWindow()
		{
			InitializeComponent();
			DataContextChanged += DialogWindow_DataContextChanged;
		}

		private void DialogWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//手动绑定窗体位置属性,自动失败,未找到原因
			try
			{

				SetBinding(TopProperty, new Binding
				{
					Source = DataContext,
					Path = new PropertyPath("Top")
				});

				SetBinding(LeftProperty, new Binding
				{
					Source = DataContext,
					Path = new PropertyPath("Left")
				});
			}
			catch { }
		}

		public IDialogResult Result
		{
			get;
			set;
		}

		
	}
}
