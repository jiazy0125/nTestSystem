using nTestSystem.Communication.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Model
{
	public class TcpIpConfigModel: BindableBase, IComConfig
	{
        private string _ip = "127.0.0.0";
        public string IP
        {
            get => _ip;
            set => SetProperty(ref _ip, value);
        }

        private int _port = 80;
        public int Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }
    }
}
