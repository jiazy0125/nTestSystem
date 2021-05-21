﻿using nTestSystem.Communication.Interfaces;
using nTestSystem.Communication.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace nTestSystem.Communication.Class
{
	public class UdpHelper : CommunicationHelper
	{
        public UdpHelper() { }
        private Socket socket;
        
        private IPEndPoint _remote;
        private IPEndPoint _local;

        private object _data1;//string格式
        private object _data2;//byte[]格式

        private Thread _rec;

        private bool isReceived = false;


        /// <summary>
        /// 打开socket套接字连接
        /// </summary>
        /// <returns></returns>
        public override bool Open()
        {
            if (IsOpen) return true;

            if (Configuration is null) return false;
            _local = new IPEndPoint(IPAddress.Parse(((UdpConfigModel)Configuration).LocalIP), ((UdpConfigModel)Configuration).LocalPort);     //本机IP,指定端口号
            _remote = new IPEndPoint(IPAddress.Parse(((UdpConfigModel)Configuration).RemoteIP), ((UdpConfigModel)Configuration).RemotePort);  //远程IP,端口号

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(_local);//绑定本机端口号和IP
                _rec = new Thread(Receive);//开启接收消息线程
                _rec.Start();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return true;
        }

        /// <summary>
        /// 向特定ip的主机的端口发送数据报
        /// </summary>
        public override bool TransmitData<T>(T data)
        {
            try
            {
                //发送数据超时设置
                socket.SendTimeout = 2000;
                //调用客户端套接字发送字节数组
                //将输入的内容字符串转换为机器可以识别的字节数组
                socket.SendTo(Encoding.UTF8.GetBytes(data as string), _remote);
            }
            catch (Exception exp)
            {
                if (exp is SocketException se)
                {
                    if (se.SocketErrorCode != SocketError.TimedOut)
                    {
                        IsOpen = false;
                        throw exp;
                    }
                    throw new Exception("Timeout");
                }
                else
                {
                    try
                    {
                        //发送数据超时设置
                        socket.SendTimeout = 2000;
                        //调用客户端套接字发送字节数组
                        socket.SendTo(data as byte[], _remote);
                    }
                    catch (Exception exp1)
                    {
                        if (exp is SocketException se1)
                        {
                            if (se1.SocketErrorCode != SocketError.TimedOut)
                            {
                                IsOpen = false;
                                throw exp;
                            }
                            throw new Exception("Timeout");
                        }
                        throw new Exception($"{exp.Message}\r\n{exp1.Message}");
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 接收发送给本机ip对应端口号的数据报
        /// </summary>
        public override T ReceiveData<T>()
        {
            //UDP采用异步数据接收功能
            if (!isReceived) return default;
            if (typeof(T) == typeof(byte[]))
                return (T) _data2;
            //返回string类型数据
            if (typeof(T) == typeof(string))
                return(T) _data1;
            return default;
        }

        /// <summary>
        /// UDP 异步接收数据
        /// </summary>
        private void Receive()
        {

            while (true)
            {
                try
                {

                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);      //用来保存发送方的ip和端口号
                    var buffer = new byte[1024];
                    //接收返回数据
                    var length = socket.ReceiveFrom(buffer, ref point);
                    //返回字符串数据
                    _data1 = Encoding.UTF8.GetString(buffer, 0, length);
                    var temp = new byte[length];
                    buffer.CopyTo(temp, 0);
                    //返回byte[]数据
                    _data2 = temp;
                    isReceived = true;
                }
                catch { isReceived = false; }
            }

        }
        public override void Close()
        {
            try
            {
                if (_rec.IsAlive)
                    _rec.Abort();
                socket.Close();
                socket.Dispose();
            }
            catch { }
        }
    }
}
