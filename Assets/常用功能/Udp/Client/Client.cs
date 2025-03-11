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
        TimeSpan start = new TimeSpan(DateTime.Now.Ticks);  //获取当前时间的刻度数
        UdpNetClient.Instance.SendMsg(Str, ReceiveIP, ReceivePort);
        TimeSpan end = new TimeSpan(DateTime.Now.Ticks);    //获取当前时间的刻度数
        TimeSpan abs = end.Subtract(start).Duration();      //时间差的绝对值
        print(string.Format(":  程序执行时间：{0}", abs.TotalMilliseconds));
    }
}
