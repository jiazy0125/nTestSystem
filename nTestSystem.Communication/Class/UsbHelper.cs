using LibUsbDotNet.LibUsb;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Text;
using nTestSystem.Communication.Interfaces;
using nTestSystem.Communication.Model;

namespace nTestSystem.Communication.Class
{
	public class UsbHelper : CommunicationHelper
	{
		private IUsbDevice _dev;
		private UsbEndpointWriter _writer = null;
		private UsbEndpointReader _reader = null;

		public override void Close()
		{
			if (_reader is object)
				_reader.Dispose();
			if (_writer is object)
				_writer.Dispose();
			if (_dev is object)
			{
				_dev.ReleaseInterface(0);
				_dev.Close();
				_dev = null;
				
			}
		}


		public override bool Open()
		{
			try
			{
				UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(((UsbConfigModel)Configuration).PID, ((UsbConfigModel)Configuration).VID);
				UsbRegistry myUsbRegistry = UsbDevice.AllWinUsbDevices.Find(MyUsbFinder);

				if (myUsbRegistry is null) return false;
				// Open this usb device.

				_dev = UsbDevice.OpenUsbDevice(MyUsbFinder) as IUsbDevice;

				_dev.SetConfiguration(1);

				_dev.ClaimInterface(0);
				_writer = _dev.OpenEndpointWriter(WriteEndpointID.Ep01);
				_reader = _dev.OpenEndpointReader(ReadEndpointID.Ep01);

			}
			catch(Exception exp)
			{
				throw new Exception(exp.ToString());
			}
			return true;
		}

		public override T ReceiveData<T>()
		{
			object ret = default(T);
			byte[] readBuffer = new byte[1024];
			try
			{
				_reader.Read(readBuffer, 100, out int bytesRead);
				if (bytesRead == 0)
					throw new Exception("No more bytes!");
				if (typeof(T) == typeof(byte[]))
					ret = bytesRead;
				if (typeof(T) == typeof(string))
					ret = Encoding.Default.GetString(readBuffer, 0, bytesRead);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			return (T)ret;
		}

		public override bool TransmitData<T>(T data)
		{
			ErrorCode ec = ErrorCode.None;
			try
			{
				if(typeof(T)==typeof(string))
				 ec = _writer.Write(Encoding.Default.GetBytes(data as string), 2000, out int bytesWritten);
				if (typeof(T) == typeof(byte[]))
					ec = _writer.Write(data as byte[], 2000, out int bytesWritten);
				if (ec != ErrorCode.None)
					throw new Exception(UsbDevice.LastErrorString);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}

			return true;
		}


		public static List<string> UsbList()
		{
			var list = new List<string>();

			foreach (UsbRegistry usb in UsbDevice.AllDevices)
			{
				list.Add(usb.ToString());
			
			}



			return list;
		}
	}
}
