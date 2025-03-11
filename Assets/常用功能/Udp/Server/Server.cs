using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    public void Receive(string Msg)
    {
        if (Msg == "Gb")
        {

        }
        Debug.Log(Msg);
    }
}
