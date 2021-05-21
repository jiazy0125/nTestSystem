using System;
using System.Collections.Generic;

namespace nTestSystem.Communication.Interfaces
{
    public interface ICommunication
    {

        /// <summary>
        /// 打开通讯接口
        /// </summary>
        /// <returns></returns>
        bool Open();
        /// <summary>
        /// 关闭通讯接口
        /// </summary>
        /// <returns></returns>
        void Close();

        /// <summary>
        /// 读取返回数据,T只支持string.byte[]两种格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T1 ReceiveData<T1>() where T1 : IEnumerable<string>, IEnumerable<byte[]>;

        /// <summary>
        /// 发送数据,只支持string.byte[]两种格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool TransmitData<T1>(T1 data) where T1 : IEnumerable<string>, IEnumerable<byte[]>;

        IComConfig Configuration { set; }

        bool IsOpen { get; }
        int Interval { set; } //ms
    }
}
