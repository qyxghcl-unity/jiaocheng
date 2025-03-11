
using UnityEngine;
[CreateAssetMenu(fileName = "IniFileConfig", menuName = "IniFileConfig", order = 0)]
public class IniFileConfig_SO : ScriptableObject
{
    [Header("配置文件路径")]
    public string iniPath = "/Config.ini";

    [Header("代码生成路径，名字固定")]
    public string scriptPath = "Assets/IniFile/Runtime/";
}
