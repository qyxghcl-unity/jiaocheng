using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
public class UdpNetServer : MonoBehaviour
{
    public static UdpNetServer Instance;
    protected UdpClient udpClient;
    public bool isSend { get; private set; }
    private Queue<SendDataClass> dataBuffer;
    public Server server;
    public Queue<string> strQue;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init(10002);
    }
    public void Init(int port)
    {
        if (udpClient != null)
        {
            Debug.Log("已开启UDP控制端");
            return;
        }
        udpClient = new UdpClient(port);
        strQue = new Queue<string>();
        Debug.Log(udpClient.Client.LocalEndPoint);
        dataBuffer = new Queue<SendDataClass>();
        isSend = true;
        ReceiveFile();
    }
    public void SendMsg(string msg, string ip, int port)
    {
        byte[] data = Encoding.UTF8.GetBytes(msg);
        SendData(data, ip, port);
    }
    public void SendData(byte[] data, string ip, int port)
    {
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        dataBuffer.Enqueue(new SendDataClass(data, iPEndPoint));
    }
    private void ReceiveFile()
    {
        udpClient.BeginReceive(ReceiveFileAsyncBackCall, null);
    }
    private void ReceiveFileAsyncBackCall(IAsyncResult ar)
    {
        if (udpClient != null)
        {
            IPEndPoint remoteIp = null;
            byte[] data = udpClient.EndReceive(ar, ref remoteIp);
            ReceiveMsg(remoteIp, data);
            ReceiveFile();
        }
    }
    private void SendFileAsyncCallBack(IAsyncResult ar)
    {
        SendDataClass sendData = ar.AsyncState as SendDataClass;
        if (!ar.IsCompleted)
        {
            Debug.LogError($"{sendData.iPEndPoint}：发送失败");
        }
        udpClient.EndSend(ar);
        isSend = true;
    }
    protected virtual void ReceiveMsg(IPEndPoint remoteIp, byte[] data)
    {
        try
        {
            string msg = Encoding.UTF8.GetString(data);
            // Debug.Log($"{remoteIp}: " + msg);
            strQue.Enqueue(msg);
            //imaset.Imas(msg);
            //MsgHandler.SendMsg(MsgHandlerType.Net, new Msg(MsgType.receiveNet, data));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    protected virtual void CloseUdp()
    {
        if (udpClient != null)
        {
            udpClient.Close();
            udpClient.Dispose();
            udpClient = null;
        }
        Debug.Log("Udp已关闭");
    }
    private void Update()
    {
        if (isSend && dataBuffer.Count > 0)
        {
            isSend = false;
            SendDataClass sendData = dataBuffer.Dequeue();
            udpClient.BeginSend(sendData.data, sendData.data.Length, sendData.iPEndPoint, SendFileAsyncCallBack, sendData);
        }

        if (strQue.Count > 0)
        {
            server.Receive(strQue.Dequeue());
        }
    }
    private void OnDestroy()
    {
        CloseUdp();
    }



    internal class SendDataClass
    {
        public SendDataClass(byte[] data, IPEndPoint iPEndPoint)
        {
            this.data = data;
            this.iPEndPoint = iPEndPoint;
        }
        public byte[] data;
        public IPEndPoint iPEndPoint;
    }
}

