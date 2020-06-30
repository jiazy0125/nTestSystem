using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using Prism.Interactivity.InteractionRequest;
using Prism.Services.Dialogs;

namespace nTestSystem.UserControls.Class
{
    public class ChildWindowActionBase : TriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
			InteractionRequestedEventArgs arg = parameter as InteractionRequestedEventArgs;

            if (arg == null)
                return;

            var windows = this.GetChildWindow(arg.Context);

            var callback = arg.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    windows.Closed -= handler;
                    callback();
                };
            windows.Closed += handler;

            windows.ShowDialog();

        }

        Window GetChildWindow(INotification notification)
        {
            var childWindow = this.CreateDefaultWindow(notification);
            childWindow.DataContext = notification;

            return childWindow;
        }

        Window CreateDefaultWindow(INotification notification)
        {
            return null;
            //return (Window)new ChildWindow.ChildWindow();
        }
    }
}
