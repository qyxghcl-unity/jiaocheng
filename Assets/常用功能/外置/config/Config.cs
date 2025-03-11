using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Config
{
    public static string IP { get; private set; }
    public static int Port { get; private set; }
    public static int PortReceive { get; private set; }
    public static int Time { get; private set; }
    public static int ScreenPosition_X { get; private set; }
    public static int ScreenPosition_Y { get; private set; }
    public static int ScreenWidth { get; private set; }
    public static int ScreenHeight { get; private set; }
    public static string MouseTrueOrFalse { get; private set; }
    public static string IsFullScreen { get; private set; }
    public static void GetConfigIni()
    {
        // 从配置文件读取
       string configFile = Application.dataPath + "/config.ini";//打包好的“xxx_Data”目录貌似没有读取里面的文件权限
                                                                 //所以对于打包的程序，需要把配置文件config.ini放在exe同目录下
       // string configFile=  Path.Combine(Application.streamingAssetsPath, "Config.txt");
#if !UNITY_EDITOR
    configFile = System.Environment.CurrentDirectory + "/config.ini";
#endif
        if (File.Exists(configFile))
        {
            ConfigIni ini = new ConfigIni(configFile);

            IP = ini.keyVal["Ip"];
            Port = int.Parse(ini.keyVal["Port"]);
            PortReceive = int.Parse(ini.keyVal["PortReceive"]);
            Time = int.Parse(ini.keyVal["LoadTime"]);
            ScreenPosition_X = int.Parse(ini.keyVal["ScreenPosition_X"]);
            ScreenPosition_Y = int.Parse(ini.keyVal["ScreenPosition_Y"]);
            ScreenWidth = int.Parse(ini.keyVal["ScreenWidth"]);
            ScreenHeight = int.Parse(ini.keyVal["ScreenHeight"]);
            MouseTrueOrFalse = ini.keyVal["MouseTrueOrFalse"];
            IsFullScreen = ini.keyVal["IsFullScreen"];
        }
    }
}
