using System;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DialogModule.ViewModels
{
	public class WarningDialogViewModel : BindableBase, IDialogAware
    {
        #region Fields

        public event Action<IDialogResult> RequestClose;

        #endregion

        #region Properties

        public ImageSource Warning { get; } = new BitmapImage(new Uri(@".\Images\alert.png", UriKind.Relative));

        private string _title = "Notification";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _closeDialogCommand;
        public DelegateCommand CloseDialogCommand =>
            _closeDialogCommand ??= new DelegateCommand(ExecuteCloseDialogCommand);

        #endregion

        #region  Excutes

        async void ExecuteCloseDialogCommand()
        {
            ButtonResult result = ButtonResult.No;
            await RaiseRequestClose(new DialogResult(result));
        }

        #endregion


        public async virtual Task RaiseRequestClose(IDialogResult dialogResult)
        {
            await Task.Delay(1500);
            RequestClose?.Invoke(dialogResult);
        }


        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}
