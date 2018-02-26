using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using LZ4Sharp;
using System.Threading;
using ffDevelopmentSpace;

public class SocketClient
{
    private Socket client;
	private EventWaitHandle allDone = new EventWaitHandle(false, EventResetMode.ManualReset);
    private MemoryStream memStream;
    private BinaryReader reader;
    private const int MAX_READ = 1024;
    private byte[] byteBuffer = new byte[MAX_READ];
	private const int PROTOCOL_HEAD_LENGTH = 6;
	private const int PROTOCOL_HEAD_LENGTH_MASK = 0xFFFFFF;
	private const int PROTOCOL_HEAD_COMPRESS_MASK = 0x1000000;
//	private const int PROTOCOL_HEAD_ENCRYPT_MASK = 0x2000000;
	AsyncCallback readCallBack;
	AsyncCallback writeCallBack;
    // Use this for initialization
	public SocketClient() {
        memStream = new MemoryStream();
        reader = new BinaryReader(memStream);
    }

    /// 连接服务器
	public void ConnectServer(string host, int port) {
		IPAddress ipAddress = Hostname2ip (host);
//		IPAddress ipAddress = IPAddress.Parse(host);
		IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);
		client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//		client.SendTimeout = 1000;	//Receive和Send等同步方法可以设置超时时间，超时时间对BeginXXX无效，对于BeginXXX，只能自己处理长时间未响应的动作
//		client.ReceiveTimeout = 1000;
		client.NoDelay = true;

		readCallBack = new AsyncCallback (OnRead);
		writeCallBack = new AsyncCallback (OnWrite);
        try 
		{
			Debuger.Log("连接服务器开始");
			allDone.Reset();
			client.BeginConnect(ipEndpoint, new AsyncCallback(OnConnect), null);
			if (!allDone.WaitOne(15000, true))
			{
				Debuger.Log("连接失败");
				throw new SocketException(10060);
			}
        } 
		catch (Exception e) 
		{
			OnDisconnected(NET_TYPE.DISCONNECT,e.Message);
        }
    }

	/// <summary>
	/// 域名转换为IP地址
	/// </summary>
	/// <param name="hostname">域名或IP地址</param>
	/// <returns>IP地址</returns>
	public IPAddress Hostname2ip(string hostname)
	{
		try
		{
			IPAddress ip;
			if (IPAddress.TryParse(hostname, out ip))
				return ip;
			else
				return Dns.GetHostEntry(hostname).AddressList[0];
		}
		catch (Exception)
		{
			throw new Exception("IP Address Error");
		}
	}

    /// 连接上服务器
    void OnConnect(IAsyncResult asr) 
	{
		if (client == null || !client.Connected)
			return;
		allDone.Set ();
		client.EndConnect(asr);
		client.BeginReceive(byteBuffer, 0, MAX_READ, SocketFlags.None,readCallBack, null);
		SingletonMB<NetworkManagerController>.GetInstance().AddEvent(NET_TYPE.CONNECT,null);
    }

    /// 写数据
    public void WriteMessage(byte[] message) {
		if (client != null && client.Connected) {
//			client.Send(message);
			client.BeginSend(message, 0, message.Length, SocketFlags.None,writeCallBack, null);
        } else {
			OnDisconnected(NET_TYPE.DISCONNECT,"client.connected----->>false");
        }
    }

    /// <summary>
    /// 读取消息
    /// </summary>
    void OnRead(IAsyncResult asr) {
		if(client == null || !client.Connected) return;
        int bytesRead = 0;
		try {
			bytesRead = client.EndReceive(asr);
		}
		catch (Exception ex) {
			OnDisconnected(NET_TYPE.EXCEPTION, ex.Message);
			return;
		}
		if (bytesRead < 1) {                //包尺寸有问题，断线处理
			OnDisconnected(NET_TYPE.DISCONNECT, "bytesRead < 1");
			return;
		}
		OnReceive(byteBuffer, bytesRead);   //分析数据包内容，抛给逻辑层
        Array.Clear(byteBuffer, 0, byteBuffer.Length);   //清空数组
		client.BeginReceive(byteBuffer, 0, MAX_READ, SocketFlags.None,readCallBack, null);
    }

    /// <summary>
    /// 丢失链接
    /// </summary>
    void OnDisconnected(ushort dis, string msg) {
		Logout();
        SingletonMB<NetworkManagerController>.GetInstance().AddEvent(dis, null);
		Debuger.Log ("Connection was closed :>" + msg + " Distype:>" + dis);
//		Debuger.LogWarning("Connection was closed by the server:>" + msg + " Distype:>" + dis);
    }

    /// <summary>
    /// 向链接写入数据流
    /// </summary>
    void OnWrite(IAsyncResult r) {
        try {
			int len = client.EndSend(r);
//			Debuger.Log("OnWrite--->>>" + len);
        } catch (Exception ex) {
			OnDisconnected(NET_TYPE.EXCEPTION, ex.Message);
			Debuger.LogWarning("OnWrite--->>>" + ex.Message);
        }
    }

	ILZ4Decompressor decompressor = LZ4DecompressorFactory.CreateNew ();
    /// <summary>
    /// 接收到消息
    /// </summary>
    void OnReceive(byte[] bytes, int length) {
        memStream.Seek(0, SeekOrigin.End);
        memStream.Write(bytes, 0, length);
        //Reset to beginning
        memStream.Seek(0, SeekOrigin.Begin);
		bool isFullMsg = false;
		int totalLen = 0;
		//这里要优化，协议HeadStruct已经解析完毕，下次就不需要解析了 memStream.Seek(0, 6);
		while (RemainingBytes() >= PROTOCOL_HEAD_LENGTH) {
			totalLen = (int)reader.ReadUInt32();
			int messageLen = totalLen & PROTOCOL_HEAD_LENGTH_MASK;
			ushort mainId = reader.ReadUInt16();
            if (RemainingBytes() >= messageLen) {
				isFullMsg = true;
				int compressFlag = totalLen & PROTOCOL_HEAD_COMPRESS_MASK;
				byte[] data = reader.ReadBytes(messageLen);
				if(0 != compressFlag)
				{
					data = decompressor.Decompress(data);
				}
				NetworkManagerController.GetInstance().AddEvent(mainId,data);
			} else {
                //Back up the position two bytes
				memStream.Position = memStream.Position - PROTOCOL_HEAD_LENGTH;
                break;
            }
        }
        //Create a new stream with any leftover bytes
		if (isFullMsg) {
			byte[] leftover = reader.ReadBytes((int)RemainingBytes());
			memStream.SetLength(0);     //Clear
			memStream.Write(leftover, 0, leftover.Length);
		}		
    }

    /// <summary>
    /// 剩余的字节
    /// </summary>
    private long RemainingBytes() {
        return memStream.Length - memStream.Position;
    }

	public bool isConnect()
	{
		if(null != client)
			return client.Connected;
		return false;
	}

    /// <summary>
    /// 登出
    /// </summary>
    public void Logout() { 
		if (client != null) {
			if (client.Connected) 
			{
				client.Shutdown(SocketShutdown.Both);
				client.Close();
			}
//			_events.Clear();
			client = null;
		}
//		loggedIn = false;
	}

}
