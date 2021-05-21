using nTestSystem.Communication.Interfaces;
using nTestSystem.Communication.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace nTestSystem.Communication.Class
{
	/// <summary>
	/// 通讯方式基类
	/// </summary>
	public class CommunicationHelper : ICommunication
	{
		public bool IsOpen { get; set; } = false;
		public int Interval {  get; set; } = 100;//ms

		public IComConfig Configuration { get; set; }

		public virtual void Close()
		{
			throw new Exception("null instance founded.");
		}

		public virtual bool Open()
		{
			throw new Exception("Open Failed.");
		}

		public virtual T ReceiveData<T>() where T : IEnumerable<string>, IEnumerable<byte[]>
		{
			throw new Exception("null instance founded.");
		}

		public virtual bool TransmitData<T>(T data) where T : IEnumerable<string>, IEnumerable<byte[]>
		{
			throw new Exception("null instance founded.");
		}


		/// <summary>
		/// 创建通讯接口实例
		/// </summary>
		/// <param name="type">通讯类型</param>
		/// <returns></returns>
		public static ICommunication CreateInstance(ComType type)
		{
			switch (type)
			{
				case ComType.SerialPort:
					return new SerialPortHelper();
				case ComType.TCP:
					return new TcpIpHelper();
				case ComType.UDP:
					return new UdpHelper();
				case ComType.GPIB:
					return new GPIBHelper();
				case ComType.USB:
					return new UsbHelper();
				case ComType.CAN:
					return new CANHelper();
				default:
					return null;
			}
		}

		/// <summary>
		/// 根据通讯类型格式化配置实例
		/// </summary>
		/// <param name="type"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IComConfig FormatConfiguration(ComType type, object instance)
		{
			switch (type)
			{
				case ComType.SerialPort:
					return (SerialPortConfigModel)instance;
				case ComType.TCP:
					return (TcpIpConfigModel)instance;
				case ComType.UDP:
					return (UdpConfigModel)instance;
				case ComType.GPIB:
					return (GPIBConfigModel)instance;
				case ComType.USB:
					return (UsbConfigModel)instance;
				case ComType.CAN:
					return (CANConfigModel)instance;
				default:
					return null;
			}
		}

		public static string GetLocalIP()
		{
			var name = Dns.GetHostName();
			IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
			foreach (IPAddress ipa in ipadrlist)
			{
				if (ipa.AddressFamily == AddressFamily.InterNetwork)
					return ipa.ToString();
			}

			return null;
		}

	}

}
