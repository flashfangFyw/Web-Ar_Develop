using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using sy;
using ProtoBuf;
namespace ffDevelopmentSpace
{

    public class NetworkManagerController : SingletonMB<NetworkManagerController>
    {
        //断线重连

        //private static NetworkManagerController instance;
        //public static NetworkManagerController GetInstance()
        //{
        //    if (null == instance)
        //    {
        //        instance = new NetworkManagerController();
        //        //instance.Init();
        //    }
        //    return instance;
        //}

        private Queue<KeyValuePair<int, byte[]>> receiveQueue = new Queue<KeyValuePair<int, byte[]>>();
        private Queue<ByteBuffer> sendQueue = new Queue<ByteBuffer>();

        private SocketClient client = new SocketClient();
        public delegate void SocketHandle(MemoryStream stream);
        private Dictionary<int, SocketHandle> handleList = new Dictionary<int, SocketHandle>();

        ByteBuffer tokenBuffer;

        void Start()
        {
            AddHandle(NET_TYPE.CONNECT, OnConnect);
        }
        void Upate()
        {
            handleSend();
            handleReceive();
        }



       

        public void AddEvent(int _event, byte[] data)
        {
            receiveQueue.Enqueue(new KeyValuePair<int, byte[]>(_event, data));
        }

      
        private void handleReceive()
        {
            int count = receiveQueue.Count;
            if (count <= 0)
                return;
            float time = Time.realtimeSinceStartup;
            while (count > 0)
            {
                KeyValuePair<int, byte[]> _event = receiveQueue.Dequeue();
                OnSocket(_event.Key, _event.Value);
                if ((Time.realtimeSinceStartup - time) > 0.02f) break;
                count--;
            }
        }

        private void handleSend()
        {
            if (!client.isConnect())
                return;
            int count = sendQueue.Count;
            if (count <= 0)
                return;
            while (count > 0)
            {
                ByteBuffer buffer = sendQueue.Dequeue();
                //			Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent("协议发送：" + buffer.getMsgId().ToString("X"));
                client.WriteMessage(buffer.ToBytes());
                buffer.Close();
                count--;
            }
        }

        public void StartConnect()
        {
            string ip = Singleton<ServiceModel>.GetInstance().GetCurSeverInfo().IP;
            int port = Singleton<ServiceModel>.GetInstance().GetCurSeverInfo().Port;
            client.ConnectServer(ip, port);
        }

        public void AddHandle(int msgId, SocketHandle handle)
        {
            if (handleList.ContainsKey(msgId))
            {
                Debuger.LogError("重复添加协议处理：" + msgId);
                Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent("重复添加协议处理：" + msgId);
                return;
            }
            handleList[msgId] = handle;
        }

        public void OnDestroy()
        {
            client.Logout();
            Debuger.Log("~NetworkManagerController was destroy");
        }
        //==================================================
        bool islogging = false;

        //--Socket消息--
        private void OnSocket(int key, byte[] buffer)
        {
            if (handleList.ContainsKey(key))
            {
                SocketHandle handle = handleList[key];
                if (handle != null)
                {
                    if (buffer != null)
                    {
                        MemoryStream stream = new MemoryStream();   //可以用对象池
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Position = 0;
                        handle(stream);
                        stream.Dispose();
                    }
                    else
                    {
                        //					string str = "协议收到处理：" + key.ToString("X");
                        //					Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent(str);
                        //					Debuger.Log(str);
                        handle(null);
                    }
                }
            }
            else
            {
                Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent("协议未处理：" + key.ToString("X"));
            }
        }

        public T Deserialize<T>(MemoryStream ms)
        {
            T t = Serializer.Deserialize<T>(ms);
            return t;
        }

        //--当连接建立时--
        private void OnConnect(MemoryStream memStream)
        {
            if (islogging)
                return;
            MessageRequestLogin login = new MessageRequestLogin();
            ////login.account = Singleton<GameModel>.GetInstance().Account;
            ////login.server_id = Singleton<ServiceModel>.GetInstance().GetCurSeverInfo().ID;
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize<MessageRequestLogin>(stream, login);
            SendMessage((ushort)MSG_CS.MSG_CS_REQUEST_LOGIN_C, stream);
            islogging = true;
        }

        public void InitToken()
        {
            MemoryStream tokenStream = new MemoryStream();
            MessageReportToken tokenInfo = new MessageReportToken();
            ////tokenInfo.account = Singleton<GameModel>.GetInstance().Account;
            ////tokenInfo.token = Singleton<GameModel>.GetInstance().Token;
            Serializer.Serialize<MessageReportToken>(tokenStream, tokenInfo);

            tokenBuffer = new ByteBuffer();
            tokenBuffer.setMsgId((ushort)MSG_CS.MSG_CS_REPORT_TOKEN_C);
            tokenBuffer.setNeedClose(false);
            tokenBuffer.WriteInt((int)tokenStream.Length);
            tokenBuffer.WriteShort((ushort)MSG_CS.MSG_CS_REPORT_TOKEN_C);
            tokenBuffer.WriteStream(tokenStream);
        }

        private void OnException()
        {
            //        islogging = false; 
            StartConnect();
            Debuger.Log("OnException------->>>>");
        }

        private void OnDisconnect()
        {
            islogging = false;
            Debuger.Log("关闭连接------->>>>");
        }

        //------------------发送请求------------------------
        public void SendMessage(ushort msgId, MemoryStream stream)
        {
            //		SendToken ();
            sendMsg(msgId, stream);
        }

        public void SendMessage<T>(MSG_CS msgId, T request)
        {
            //		SendToken ();
            MemoryStream stream = new MemoryStream();
            Serializer.Serialize<T>(stream, request);
            sendMsg((ushort)msgId, stream);
            string str = request.ToString() + checkObj(request);
            str = Util.getHtmlStr(str, "#00ff00");
            Singleton<ModuleEventDispatcher>.GetInstance().dispatchDebugLogEvent(str);
        }

        string checkObj(object a)
        {
            object obj;
            string str = "[";
            bool flag = false;
            foreach (System.Reflection.PropertyInfo p in a.GetType().GetProperties())
            {
                if (flag)
                    str += "," + p.Name + ":";
                else
                    str += p.Name + ":";
                flag = true;
                if (p.CanWrite)
                {
                    if (p.PropertyType.IsValueType || p.PropertyType.Name == "String")
                    {
                        str += p.GetValue(a, null);
                    }
                    else
                    {
                        obj = p.GetValue(a, null);
                        if (null != obj)
                            str += checkObj(obj);
                    }
                }
                else
                {
                    IList list = (IList)p.GetValue(a, null);
                    str += "[";
                    bool flag1 = false;
                    foreach (object b in list)
                    {
                        if (flag1) str += ",";
                        if (b.GetType().IsValueType || b.GetType() == typeof(string)) str += b;
                        else str += checkObj(b);
                        flag1 = true;
                    }
                    str += "]";
                }
            }
            str += "]";
            return str;
        }

        private void sendMsg(ushort msgId, MemoryStream stream)
        {
            ByteBuffer buffer = new ByteBuffer();
            if (stream != null) buffer.WriteInt((ushort)stream.Length);
            else buffer.WriteInt(0);
            buffer.WriteShort(msgId);
            buffer.setMsgId(msgId);
            if (stream != null) buffer.WriteStream(stream);
            sendQueue.Enqueue(buffer);
        }

        //------------------------发送Token校验-----------------------------
        private void SendToken()
        {
            if (islogging)
            {
                ////if (Singleton<GameModel>.GetInstance().Token != null &&
                ////    Singleton<GameModel>.GetInstance().Token != "")
                ////{
                ////    sendQueue.Enqueue(tokenBuffer);
                ////}
            }
        }

    }

    public struct NET_TYPE
    {
        public const ushort CONNECT = 101;
        public const ushort DISCONNECT = 102;
        public const ushort EXCEPTION = 103;
    }
}