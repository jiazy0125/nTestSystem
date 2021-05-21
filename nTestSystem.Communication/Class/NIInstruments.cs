using Ivi.Visa;
using NationalInstruments.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nTestSystem.Communication.Class
{

    public class GPIBPortHelper : NIInstrumentBase
    {
        public GPIBPortHelper(string address) : base(new GpibSession(address), address)
        {
            if (!address.ToUpper().Contains("GPIB"))
                throw new ArgumentException($"该地址不含GPIB字样");
        }


        public static string[] FindGPIBAddresses()
        {
            IEnumerable<string> result = new List<string>();
            try
            {
                result = GlobalResourceManager.Find($"GPIB?*INSTR");
            }
            catch (Exception ex)
            {
                if (!(ex is NativeVisaException))
                {
                    if (ex.InnerException != null) throw ex.InnerException;
                    else throw ex;
                }
            }

            return result.ToArray().Where(n => !n.Contains("//")).ToArray();
        }
    }

    public class PortEventArgs : EventArgs
    {
        public string Address { private set; get; }
        public bool Cancel { set; get; }
        public PortEventArgs(string address)
        {
            Address = address;
        }
    }

    public abstract class NIInstrumentBase 
    {
        public string Address { set; get; }

        public NIInstrumentBase(IMessageBasedSession session)
        {
            Session = session;
        }
        public NIInstrumentBase(IMessageBasedSession session, string address) : this(session)
        {
            Address = address;
        }

        public int Timeout { set; get; } = 2000;

        public event EventHandler<PortEventArgs> PortOpenning;

        public event EventHandler<PortEventArgs> PortClosing;

        protected virtual void OnPortOpenning(PortEventArgs e)
        {
            PortOpenning?.Invoke(this, e);
        }

        protected virtual void OnPortClosing(PortEventArgs e)
        {
            PortClosing?.Invoke(this, e);
        }

        public bool IsPortOpen { private set; get; } = false;

        protected IMessageBasedSession Session { private set; get; }

        public virtual bool Open()
        {
            var e = new PortEventArgs(Address);
            OnPortOpenning(e);
            if (!e.Cancel)
            {
                Session.TimeoutMilliseconds = Timeout;
                return true;
            }
            return false;
        }

        public virtual bool Close()
        {
            var e = new PortEventArgs(Address);
            OnPortClosing(e);
            if (!e.Cancel)
            {
                Session.Dispose();
                return true;
            }
            return false;
        }

        public virtual void Write(string command)
        {
            Session.RawIO.Write(command);
        }
        public virtual void Write(byte[] command)
        {
            Session.RawIO.Write(command);
        }
        public virtual void WriteLine(string command)
        {
            Write($"{command}\n");
        }

        public const int READ_BUFFER_COUNT = 1024;

        public virtual string Read()
        {
            return Encoding.UTF8.GetString(ReadByte());
        }
        public virtual byte[] ReadByte()
        {
            return Session.RawIO.Read();
        }

        public virtual string ReadLine()
        {
            var result = Read();
            return result.EndsWith("\n") ? result.TrimEnd(new char[] { '\n' }) : result;
        }

        public virtual void Clear()
        {
            Session.Clear();
        }
    }

    interface INIPort
    {
        void Open();
        void Close();
        void Write(string command);
        string Read();
        void Clear();
    }
}
