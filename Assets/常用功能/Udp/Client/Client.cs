using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System;
public class Client : MonoBehaviour
{
    public int SendPort;
    public string ReceiveIP;
    public int ReceivePort;
   
    void Start()
    {
      
        Config.GetConfigIni();
        ReceiveIP = Config.IP;
        SendPort = Config.Port;
        ReceivePort = Config.PortReceive;
      
    }  
    public void SendString(string Str)
    {
        TimeSpan start = new TimeSpan(DateTime.Now.Ticks);  //��ȡ��ǰʱ��Ŀ̶���
        UdpNetClient.Instance.SendMsg(Str, ReceiveIP, ReceivePort);
        TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //��ȡ��ǰʱ��Ŀ̶���
        TimeSpan abs = end.Subtract(start).Duration();      //ʱ���ľ���ֵ
        print(string.Format(":  ����ִ��ʱ�䣺{0}", abs.TotalMilliseconds));
    }
}
