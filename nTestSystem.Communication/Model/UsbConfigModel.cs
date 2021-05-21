using nTestSystem.Communication.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Model
{
	public class UsbConfigModel:BindableBase, IComConfig
	{


        private int _pid;
        public int PID
        {
            get => _pid;
            set => SetProperty(ref _pid, value);
        }

        private int _vid;
        public int VID
        {
            get => _vid;
            set => SetProperty(ref _vid, value);
        }

        private int _interval = 100;
        public int Interval
        {
            get => _interval;
            set => SetProperty(ref _interval, value);
        }

    }
}
