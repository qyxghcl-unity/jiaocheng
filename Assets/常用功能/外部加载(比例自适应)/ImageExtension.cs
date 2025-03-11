using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class ImageExtension
{
    /// <summary>
    /// 缩放图片
    /// </summary>
    /// <param name="image"></param>
    /// <param name="destHeight"></param>
    /// <param name="destWidth"></param>
    public static void ZoomImage(this Image image, float destWidth, float destHeight)
    {
        image.SetNativeSize();//先将图片还原到原大小


        float width = 0, height = 0;
        //按比例缩放
        var sizeDelta = image.GetComponent<RectTransform>().sizeDelta;
        float sourWidth = sizeDelta.x;
        float sourHeight = sizeDelta.y;

        if (sourHeight > destHeight || sourWidth > destWidth)
        {

            if ((sourWidth * destHeight) > (sourHeight * destWidth))
            {
                width = destWidth;
                height = (destWidth * sourHeight) / sourWidth;
            }
            else
            {
                height = destHeight;
                width = (sourWidth * destHeight) / sourHeight;
            }

        }
        else
        {
            width = sourWidth;
            height = sourHeight;
        }
        Vector2 size = new Vector2(width, height);
        image.GetComponent<RectTransform>().sizeDelta = size;
        // image.transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(width, height),0.3f);
        //return size;
    }
}
