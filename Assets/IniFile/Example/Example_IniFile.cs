using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tools/IniFile/创建配置文件                    Config.ini
//Tools/IniFile/打开配置文件                    Config.ini
//Tools/IniFile/创建ScriptableObject配置文件    配置Config.ini的路径 和参数代码的生成
//Tools/IniFile/刷新配置文件代码                根据Config.ini里的参数生成代码
public class Example_IniFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(IniFileCtrl.Instance._最大检测距离.ToFloat());
        Debug.Log(IniFileCtrl.Instance._最小检测距离.ToFloat());
        Debug.Log(IniFileCtrl.Instance._静音.ToBool());
        Debug.Log(IniFileCtrl.Instance._音频.ToInt());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
