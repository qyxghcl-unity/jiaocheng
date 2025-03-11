using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System;
public class ScreenTouchDetection : MonoBehaviour
{
   
    // 多长时间隐藏鼠标
    public float durationToHide;
    // 最后一次操作鼠标的位置
    Vector3 lastMousePos;
    // 最后一次操作的时间
    float lastTime;
    // Use this for initialization
    void Start()
    {
        lastMousePos = Input.mousePosition;
        lastTime = Time.time;
        Config.GetConfigIni();
     
        durationToHide = Config.Time;
    }
    bool Ist;
    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        if (mousePos.Equals(lastMousePos))
        {
            if (Ist == true)
            {
                if ((Time.time - lastTime) >= durationToHide)
                {
                  
                  
                    Debug.Log("关闭");
                    Ist = false;
                }
            }
        }
        else
        {
            lastMousePos = mousePos;
            lastTime = Time.time;
            Ist = true;
        }
    }
   
}
