using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System;
public class ScreenTouchDetection : MonoBehaviour
{
   
    // �೤ʱ���������
    public float durationToHide;
    // ���һ�β�������λ��
    Vector3 lastMousePos;
    // ���һ�β�����ʱ��
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
                  
                  
                    Debug.Log("�ر�");
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
