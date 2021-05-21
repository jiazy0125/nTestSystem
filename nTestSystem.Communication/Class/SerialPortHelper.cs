using nTestSystem.Communication.Interfaces;
using nTestSystem.Communication.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace nTestSystem.Communication.Class
{
	public class SerialPortHelper: CommunicationHelper
	{
        private SerialPort _sp;

        public SerialPortHelper()
        {
            _sp = new SerialPort();
        }

        public override void Close()
        {
            try
            {
                if (_sp.IsOpen) _sp.Close();
                IsOpen = _sp.IsOpen;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public override bool Open()
        {
            //if (IsAsync)
            //{
            //    _sp.DataReceived += _sp_DataReceived;
            //    _sp.ErrorReceived += _sp_ErrorReceived;
            //}

            try
            {
                if (!_sp.IsOpen)
                {
                    if (Configuration == null)
                        throw new Exception("Configuration is not correctlly.");
                    var cfg = (SerialPortConfigModel)Configuration;
                    _sp.PortName = cfg.PortName;
                    _sp.BaudRate = cfg.BaudRate;
                    _sp.DataBits = cfg.DataBits;
                    _sp.StopBits = cfg.StopBits;
                    _sp.Parity = cfg.Parity;
                    _sp.Open();
                    IsOpen = _sp.IsOpen;
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return _sp.IsOpen;
        }

        public override T ReceiveData<T>()
        {
            object ret = default(T);
            try
            {
                int count = 0;
                int bytes = 0;
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(Interval);
                    if (bytes == _sp.BytesToRead) count++;
                    else
                    {
                        bytes = _sp.BytesToRead;
                        count = 0;
                    }
                    if (count >= 3) break;               
                }

                var byteData = new byte[bytes]; //定义缓冲区大小 
                _sp.Read(byteData, 0, byteData.Length); //从串口读取数据 
                //返回byte[]类型数据
                if (typeof(T) == typeof(byte[]))
                    ret = byteData;
                //返回string类型数据
                if (typeof(T) == typeof(string))
                    ret = Encoding.UTF8.GetString(byteData, 0, byteData.Length);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return (T)ret;
        }

        public override bool TransmitData<T>(T data)
        {

            Open();

            try
            {
                _sp.Write(data as byte[], 0, (data as byte[]).Length);
            }
            catch
            {
                try
                {
                    _sp.Write(data as string);
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
            return true;

        }
    }
}
