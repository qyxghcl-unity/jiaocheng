using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System;

public class ImageAdaptive : MonoBehaviour
{
    public Button BtnLeft;//���л�
    public Button BtnRight;
    public string ImageRoad;//�ļ�·��
    public Image Introduce;//��ʾUI
    private void Start()
    {
        Load();
        BtnLeft.onClick.AddListener(() =>
        {
            StartCoroutine(LeftClick());
        });
        BtnRight.onClick.AddListener(() =>
        {
            StartCoroutine(RightClick());
        });
        //for (int i = 0; i < imas .Length; i++)
        //{
        //    imas[i].gameObject.transform.GetComponent<Image>().color = Color.black;
        //}
    }
    IEnumerator LeftClick()
    {
        yield return new WaitForSeconds(0.2f);
        index--;
        if (index < 0)
        {
            // index = characterList.Count - 1;    //�����Ѿ��ǵ�һ��ͼƬ���ٴε�����������һ��ͼƬ
            index = 0;
        }
        Introduce.sprite = characterList[index];
    }
    IEnumerator RightClick()
    {
        yield return new WaitForSeconds(0.2f);
        index++;
        if (index >= characterList.Count)
        {
            // index = 0;//�����Ѿ������һ��ͼƬ���ٴε�������ǵ�һ��ͼƬ
            index = characterList.Count - 1;
        }
        Introduce.sprite = characterList[index];
    }
    private void Update()
    {
        Introduce.ZoomImage(750, 1000);
        if (index == 0)
        {
            BtnLeft.interactable = false;
            BtnRight.interactable = true;
        }
        else if (index == characterList.Count - 1)
        {
            BtnRight.interactable = false;
            BtnLeft.interactable = true;
        }
        else
        {
            BtnRight.interactable = true;
            BtnLeft.interactable = true;
        }
    }
    void Load()
    {
        if (!Application.runInBackground)
            Application.runInBackground = true;
        string path = Application.streamingAssetsPath + ImageRoad;
        Debug.Log(path);
        string[] picPath = Directory.GetFiles(path);
        for (int i = 0; i < picPath.Length; i++)
        {
            if (!picPath[i].Contains("meta"))
            {
                characterPathList.Add(picPath[i]);
            }
        }
        //ѭ������ÿһ��ͼƬ
        for (int i = 0; i < characterPathList.Count; i++)
        {
            StartCoroutine(LoadWordPic(characterPathList[i], i));
        }
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    //����·��
    List<Sprite> characterList = new List<Sprite>();
    List<string> characterPathList = new List<string>();
    int index;
    //����ͼƬ
    IEnumerator LoadWordPic(string path, int num)
    {
        string url = "file://" + path;
        WWW www = new WWW(url);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture2D texture = www.texture;
            Sprite spr = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            characterList.Add(spr);
            if (num == 0)
            {
                Introduce.sprite = characterList[0];
                Introduce.sprite = spr;
            }
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Back()
    {
        characterList.Clear();
        characterPathList.Clear();
        index = 0;
        Introduce.sprite = null;
    }

}

