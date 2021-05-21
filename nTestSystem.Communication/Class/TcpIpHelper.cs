﻿using nTestSystem.Communication.Interfaces;
using nTestSystem.Communication.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace nTestSystem.Communication.Class
{
	public class TcpIpHelper : CommunicationHelper
	{
        private Socket socket = null;

        public TcpIpHelper()
        {
        }

        public override void Close()
        {
            try
            {
                if (socket.Connected)
                {
                    socket.Close();
                    socket.Dispose();
                }
            }
            catch (Exception exp)
            { throw exp; }
        }



        public override bool Open()
        {
            if (Configuration == null) throw new Exception("Configuration is not correctlly.");
            if (IsOpen) return true;

            //定义一个套接字监听
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //将获取的IP地址和端口号绑定在网络节点上
            var point = new IPEndPoint(IPAddress.Parse(((TcpIpConfigModel)Configuration).IP), ((TcpIpConfigModel)Configuration).Port);
            //Msg = "Connecting to the server " + ip + ":" + port;
            try
            {
                //客户端套接字连接到网络节点上，用的是Connect
                socket.Connect(point);
            }
            catch (Exception exp)
            {
                IsOpen = false;
                throw exp;
            }

            return true;
        }

        //接收服务端发来信息的方法
        public override T ReceiveData<T>()
        {
            object ret = default(T);
            try
            {
                //定义一个1M的内存缓冲区，用于临时性存储接收到的消息
                var byteData = new byte[1024 * 1024];

                //将客户端套接字接收到的数据存入内存缓冲区，并获取长度
                socket.ReceiveTimeout = 5000;
                var length = socket.Receive(byteData);
                if (typeof(T) == typeof(byte[]))
                    ret = byteData;
                //返回string类型数据
                if (typeof(T) == typeof(string))
                    ret = Encoding.UTF8.GetString(byteData, 0, byteData.Length);

            }
            catch (Exception exp)
            {
                if (((SocketException)exp).SocketErrorCode != SocketError.TimedOut)
                {
                    IsOpen = false;
                    throw exp;
                }
                throw new Exception("Timeout");
            }
            return (T)ret;
        }

        //发送字符信息到服务端的方法
        public override bool TransmitData<T>(T data)
        {
            Open();
            try
            {
                socket.SendTimeout = 2000;
                //调用客户端套接字发送字节数组
                //将输入的内容字符串转换为机器可以识别的字节数组
                socket.Send(Encoding.UTF8.GetBytes(data as string));
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
                        //
                        socket.SendTimeout = 2000;
                        //调用客户端套接字发送字节数组
                        socket.Send(data as byte[]);
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
    }
}
