using System.Windows;
using System.Windows.Media;

namespace nTestSystem.UserControls.Controls
{
	public class SystemCloseButton :SystemButton
	{
        Window targetWindow;
        public SystemCloseButton()
        {
            SystemButtonHoverColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

            Click += delegate {
                if (targetWindow == null)
                {
                    targetWindow = Window.GetWindow(this);
                }
                targetWindow.Close();
            };
        }
    }
}
