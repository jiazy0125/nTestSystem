using nTestSystem.Communication.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Model
{
	public class GPIBConfigModel : BindableBase, IComConfig
	{
        private string _gpibAddress;
        public string GPIBAddress
        {
            get => _gpibAddress;
            set => SetProperty(ref _gpibAddress, value);
        }
    }
}
