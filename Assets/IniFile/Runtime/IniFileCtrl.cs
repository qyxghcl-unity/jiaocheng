using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

// 单例模式的配置文件控制器类，用于读取和修改INI文件
public sealed partial class IniFileCtrl
{
    private static IniFileConfig_SO _IniFileConfig;

    public static IniFileConfig_SO IniFileConfig
    {
        get
        {
            if (_IniFileConfig == null)
            {
                _IniFileConfig = Resources.Load<IniFileConfig_SO>("IniFileConfig");
            }
            return _IniFileConfig;
        }
    }

    // 私有的静态实例，用于实现单例模式
    private static IniFileCtrl _instance;
    public static string IniPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return Application.persistentDataPath + IniFileConfig.iniPath;
            }
            return Application.streamingAssetsPath + IniFileConfig.iniPath;
        }
    }
    // 提供对外的静态属性，通过这个属性获取INI文件的实例
    public static IniFileCtrl Instance
    {
        get
        {
            // 如果实例为空，则根据不同的平台选择配置文件的路径并解析
            if (_instance == null)
            {

                _instance = new IniFileCtrl();

            }
            return _instance;
        }
    }
    private IniFile iniFile;
    public IniFileCtrl()
    {
        if (Application.platform == RuntimePlatform.Android && !File.Exists(IniPath))
        {
            CopyToPersistentPath();
        }

        iniFile = new IniFile(IniPath, true);
        iniFile.ParseIni();

    }

    /// <summary>
    /// 在安卓平台下,将配置文件从 StreamingAssets 复制到 PersistentDataPath
    /// </summary>
    private void CopyToPersistentPath()
    {
        string sourcePath = Application.streamingAssetsPath + IniFileConfig.iniPath;
        string destinationPath = Application.persistentDataPath + IniFileConfig.iniPath;


        // 如果目标文件不存在，则从 StreamingAssets 复制
        if (Application.platform == RuntimePlatform.Android)
        {
            // 使用 UnityWebRequest 读取 StreamingAssets 中的文件
            UnityWebRequest www = UnityWebRequest.Get(sourcePath);
            DownloadHandler handler = new DownloadHandlerFile(destinationPath);
            www.downloadHandler = handler;

            www.SendWebRequest();

            while (!www.isDone)
            {
                // 等待下载完成
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("ini文件移动失败: " + www.error);
            }
            else
            {
                Debug.Log("ini文件移动成功: " + destinationPath);

                FileStream fs = File.Create(IniPath);
                fs.Write(www.downloadHandler.data);
                fs.Close();

            }
        }


    }
    /// <summary>
    /// 全部的配置文件
    /// </summary>
    public Dictionary<string, Dictionary<string, string>> IniConfigDic { get => iniFile.IniConfigDic; }
    /// <summary>
    /// 读取字符串配置文件
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="defaultVale">默认值，配置表里没有就返回这个</param>
    /// <returns>返回对应键值的字符串，如果没有找到则返回默认值</returns>
    public string GetString(string section, string key, string defaultVale = null)
    {
        return iniFile.GetString(section, key, defaultVale);
    }

    /// <summary>
    /// 读取浮点数
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="defaultVale">默认值，配置表里没有就返回这个</param>
    /// <returns>返回对应键值的浮点数，如果没有找到则返回默认值</returns>
    public float GetFloat(string section, string key, float defaultVale = 0)
    {
        return iniFile.GetFloat(section, key, defaultVale);
    }

    /// <summary>
    /// 读取整数
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="defaultVale">默认值，配置表里没有就返回这个</param>
    /// <returns>返回对应键值的整数，如果没有找到则返回默认值</returns>
    public int GetInt(string section, string key, int defaultVale = 0)
    {
        return iniFile.GetInt(section, key, defaultVale);
    }
    /// <summary>
    /// 读取布尔值
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="defaultVale">默认值，配置表里没有就返回这个</param>
    /// <returns>返回对应键值的布尔值，如果没有找到则返回默认值</returns>
    public bool GetBool(string section, string key, bool defaultVale = false)
    {
        return iniFile.GetBool(section, key, defaultVale);
    }
    /// <summary>
    /// 设置字符串
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要设置的字符串值</param>
    public void SetString(string section, string key, string value)
    {
        iniFile.SetString(section, key, value);
    }

    /// <summary>
    /// 设置浮点数
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要设置的浮点数值</param>
    public void SetFloat(string section, string key, float value)
    {
        iniFile.SetFloat(section, key, value);
    }

    /// <summary>
    /// 设置布尔变量
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要设置的布尔值</param>
    public void SetBool(string section, string key, bool value)
    {
        iniFile.SetBool(section, key, value);
    }
    /// <summary>
    /// 设置整数
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要设置的整数值</param>
    public void SetInt(string section, string key, int value)
    {
        iniFile.SetInt(section, key, value);
    }

    /// <summary>
    /// 添加字符串配置项
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要添加的字符串值</param>
    public void AddString(string section, string key, string value)
    {
        iniFile.AddString(section, key, value);
    }

    /// <summary>
    /// 添加浮点数配置项
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要添加的浮点数值</param>
    public void AddFloat(string section, string key, float value)
    {
        iniFile.AddFloat(section, key, value);
    }

    /// <summary>
    /// 添加整数配置项
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要添加的整数值</param>
    public void AddInt(string section, string key, int value)
    {
        iniFile.AddInt(section, key, value);
    }
    /// <summary>
    /// 添加整数配置项
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <param name="value">要添加的整数值</param>
    public void AddBool(string section, string key, bool value)
    {
        iniFile.AddBool(section, key, value);
    }
    /// <summary>
    /// 移除指定的配置项
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <param name="key">对应的键值</param>
    /// <returns>返回是否成功移除配置项</returns>
    public bool RemoveConfig(string section, string key)
    {
        return iniFile.RemoveConfig(section, key);
    }

    /// <summary>
    /// 移除指定的配置段
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <returns>返回是否成功移除配置段</returns>
    public bool RemoveSection(string section)
    {
        return iniFile.RemoveSection(section);
    }

    /// <summary>
    /// 获取指定配置段的所有配置信息
    /// </summary>
    /// <param name="section">配置文件的段名</param>
    /// <returns>返回一个字典，包含指定配置段的所有键值对</returns>
    public Dictionary<string, string> GetSectionInfo(string section)
    {
        return iniFile.GetSectionInfo(section);
    }

    /// <summary>
    /// 保存INI文件的更改
    /// </summary>
    public void SaveIni()
    {
        iniFile.SaveIni();
    }



}