using nTestSystem.Communication.Class;
using nTestSystem.Communication.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Model
{
	public class UdpConfigModel : BindableBase, IComConfig
	{
        public UdpConfigModel()
        {
            _localIp = CommunicationHelper.GetLocalIP();
        }

        private string _remoteIp = "127.0.0.1";
        public string RemoteIP
        {
            get => _remoteIp;
            set => SetProperty(ref _remoteIp, value);
        }

        private int _remotePort = 3030;
        public int RemotePort
        {
            get => _remotePort;
            set => SetProperty(ref _remotePort, value);
        }

        private string _localIp;
        public string LocalIP
        {
            get => _localIp;
            set => SetProperty(ref _localIp, value);
        }

        private int _localPort = 8080;
        public int LocalPort
        {
            get => _localPort;
            set => SetProperty(ref _localPort, value);
        }

    }
}