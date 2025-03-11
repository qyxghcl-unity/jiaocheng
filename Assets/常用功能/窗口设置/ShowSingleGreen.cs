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
    const int GWL_STYLE = -16;  //�߿��õ�
    const int WS_BORDER = 1;
    const int WS_POPUP = 0x800000;
    int ScreenPosition_X;
    int ScreenPosition_Y;
    // ��������������Ҫ�Ĵ��ڿ�
    int ScreenWidth;
    // ��������������Ҫ�Ĵ��ڸ�
    int ScreenHeight;
    bool  MouseTrueOrFalse;
    bool IsFullScreen;
    void Start()
    {
    
        Config.GetConfigIni();

        MouseTrueOrFalse = bool.Parse(Config.MouseTrueOrFalse);
        Cursor.visible = MouseTrueOrFalse; // �������

        ScreenPosition_X = Config.ScreenPosition_X;
        ScreenPosition_Y = Config.ScreenPosition_Y;
        ScreenWidth = Config.ScreenWidth;
        ScreenHeight = Config.ScreenHeight;

        IsFullScreen = bool.Parse(Config.IsFullScreen);
       //Screen.fullScreen = IsFullScreen;  //���ó�ȫ��,

        Screen.SetResolution(ScreenWidth, ScreenHeight, IsFullScreen);
        
       
        if (Application.platform != RuntimePlatform.WindowsEditor)
        {
            StartCoroutine("Setposition");
            Debug.Log("���������");
        }
        else
        {
            Debug.Log("WINDOWS EDITOR");
        }
       
        //Screen.SetResolution(_Txtwith, _Txtheight, false);		//�����Unity���������Ļ��С��
    }
    IEnumerator Setposition()
    {
        yield return new WaitForSeconds(0.1f);		//��֪��Ϊʲô�������к�����λ�õĲ�����Ч�����ӳ�0.1��Ϳ���
        SetWindowLong(GetForegroundWindow(), GWL_STYLE, WS_POPUP);      //�ޱ߿�
        bool result = SetWindowPos(GetForegroundWindow(), 0, ScreenPosition_X, ScreenPosition_Y, ScreenWidth, ScreenHeight, SWP_SHOWWINDOW);       //������Ļ��С��λ��
    }
}