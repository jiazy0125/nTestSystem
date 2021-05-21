using nTestSystem.Communication.Interfaces;
using Prism.Mvvm;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Model
{
	public class SerialPortConfigModel: BindableBase, IComConfig
	{
		private string _portName = "";
		public string PortName
		{
			get => _portName;
			set => SetProperty(ref _portName, value);
		}

        private int _baudRate = 9600;
        public int BaudRate
        {
            get => _baudRate;
            set => SetProperty(ref _baudRate, value);
        }
        private int _dataBits = 8;
        public int DataBits
        {
            get => _dataBits;
            set => SetProperty(ref _dataBits, value);
        }

        private Parity _parity = Parity.None;
        public Parity Parity
        {
            get => _parity;
            set => SetProperty(ref _parity, value);
        }

        private StopBits _stopBits = StopBits.One;
        public StopBits StopBits
        {
            get => _stopBits;
            set => SetProperty(ref _stopBits, value);
        }

        private bool _isAsync = false;
        public bool IsAsync
        {
            get => _isAsync;
            set => SetProperty(ref _isAsync, value);
        }







    }
}
