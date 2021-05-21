using nTestSystem.UserControls.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.UserControls.Controls
{
	public class MetroIconButton : MetroButtonBase
	{
        public MetroIconButton()
        {
            Utility.Refresh(this);
        }

        static MetroIconButton()
        {
            ElementBase.DefaultStyle<MetroIconButton>(DefaultStyleKeyProperty);
        }






    }
}
