using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShowSingleGreen : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hwnd, int _nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();
    const uint SWP_SHOWWINDOW = 0x0040;
    const int GWL_STYLE = -16;  //边框用的
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    int ScreenPosition_X;
    int ScreenPosition_Y;
    // 在这里设置你想要的窗口宽
    int ScreenWidth;
    // 在这里设置你想要的窗口高
    int ScreenHeight;
    bool  MouseTrueOrFalse;
    bool IsFullScreen;
    void Start()
    {
    
        Config.GetConfigIni();

        MouseTrueOrFalse = bool.Parse(Config.MouseTrueOrFalse);
        Cursor.visible = MouseTrueOrFalse; // 鼠标隐藏

        ScreenPosition_X = Config.ScreenPosition_X;
        ScreenPosition_Y = Config.ScreenPosition_Y;
        ScreenWidth = Config.ScreenWidth;
        ScreenHeight = Config.ScreenHeight;

        IsFullScreen = bool.Parse(Config.IsFullScreen);
       //Screen.fullScreen = IsFullScreen;  //设置成全屏,

        Screen.SetResolution(ScreenWidth, ScreenHeight, IsFullScreen);
        
       
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            StartCoroutine("Setposition");
            Debug.Log("打包出来了");
        }
        else
        {
            Debug.Log("WINDOWS EDITOR");
        }
       
        //Screen.SetResolution(_Txtwith, _Txtheight, false);		//这个是Unity里的设置屏幕大小，
    }
    IEnumerator Setposition()
    {
        yield return new WaitForSeconds(0.1f);		//不知道为什么发布于行后，设置位置的不会生效，我延迟0.1秒就可以
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);      //无边框
        bool result = SetWindowPos(GetForegroundWindow(), 0, ScreenPosition_X, ScreenPosition_Y, ScreenWidth, ScreenHeight, SWP_SHOWWINDOW);       //设置屏幕大小和位置
    }
}