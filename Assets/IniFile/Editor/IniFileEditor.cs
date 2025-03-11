using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class IniFileEditor
{
    [MenuItem("Tools/IniFile/创建配置文件")]
    public static void CreateInfFile()
    {

        File.Create(IniFileCtrl.IniPath).Close();
        using (StreamWriter sw = new StreamWriter(IniFileCtrl.IniPath))
        {
            sw.WriteLine("[Section1]");
            sw.WriteLine("Key1=Value1");
            sw.WriteLine("Key2=Value2");
        }

        AssetDatabase.Refresh();
    }
    [MenuItem("Tools/IniFile/打开配置文件")]
    public static void OpenConfig()
    {
        if (!File.Exists(IniFileCtrl.IniPath))
        {
            CreateInfFile();
        }
        Application.OpenURL(IniFileCtrl.IniPath);
    }

    [MenuItem("Tools/IniFile/创建ScriptableObject配置文件")]
    public static void CreateScriptableObject()
    {
        IniFileConfig_SO config = ScriptableObject.CreateInstance<IniFileConfig_SO>();

        config.iniPath = "/Config.ini";
        config.scriptPath = "Assets/IniFile/Runtime/IniFileCtrlPartial.cs";

        // 定义保存路径
        string savePath = "Assets/Resources/IniFileConfig.asset";

        // 确保路径存在
        string directory = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 保存 Config 实例
        AssetDatabase.CreateAsset(config, savePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    [MenuItem("Tools/IniFile/创建配置文件代码")]
    public static void RefreshIni()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("public partial class IniFileCtrl");
        stringBuilder.AppendLine("{");

        foreach (var section in IniFileCtrl.Instance.IniConfigDic)
        {
            string key = section.Key;
            Dictionary<string, string> val = section.Value;
            stringBuilder.AppendLine($"#region {key}");
            foreach (var item in val)
            {
                stringBuilder.AppendLine($"    private string {item.Key};");
                stringBuilder.AppendLine("    public string " + "_" + item.Key);
                stringBuilder.AppendLine("    {");
                stringBuilder.AppendLine("        get");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine($"            if ({item.Key} == null)");
                stringBuilder.AppendLine("            {");
                stringBuilder.AppendLine($"                {item.Key} = GetString(\"{key}\", \"{item.Key}\", \"\");");
                stringBuilder.AppendLine("            }");
                stringBuilder.AppendLine($"            return {item.Key};");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        set { GetString(\"" + key + "\", \"" + item.Key + "\"" + ", \"\"); }");
                stringBuilder.AppendLine("    }");
            }
            stringBuilder.AppendLine("#endregion");
        }
        stringBuilder.AppendLine("}");

        string path = IniFileCtrl.IniFileConfig.scriptPath;
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            fs.Write(bytes);
        }

        AssetDatabase.Refresh();
        Debug.Log("创建配置文件代码成功: " + path);
    }
}
