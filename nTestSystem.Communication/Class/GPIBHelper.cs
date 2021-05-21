using Ivi.Visa;
using NationalInstruments.Visa;
using nTestSystem.Communication.Model;
using System;
using System.Text;

namespace nTestSystem.Communication.Class
{
	public class GPIBHelper:CommunicationHelper
	{
		private NIInstrumentBase _GpibInstr;

		protected IMessageBasedSession Session { private set; get; }

        public override bool Open()
        {
            if (IsOpen) return true;
            try
            {
                _GpibInstr = new GPIBPortHelper(((GPIBConfigModel)Configuration).GPIBAddress);

                IsOpen = _GpibInstr.Open();
            }
            catch (Exception exp)
            {

                throw exp;
            }
            return true;
        }

        public override bool TransmitData<T>(T data)
        {
            Open();

            try
            {
                _GpibInstr.Write(data as string);
            }
            catch (Exception exp)
            {
                try
                {
                    _GpibInstr.Write(data as byte[]);
                }
                catch (Exception exp1)
                {
                    throw new Exception($"{exp.Message}\r\n{exp1.Message}");
                }
            }

            return true;
        }

        public override T ReceiveData<T>()
        {
            object ret = default(T);
            try
            {
                if (typeof(T) == typeof(byte[]))
                    ret = (T)(object)_GpibInstr.ReadByte();
                //返回string类型数据
                if (typeof(T) == typeof(string))
                    ret = (T)(object)_GpibInstr.Read();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return (T)ret;
        }

        public override void Close()
        {
            try
            {
                IsOpen = _GpibInstr.Close();
                 
            }
            catch { }

        }

    }
}
