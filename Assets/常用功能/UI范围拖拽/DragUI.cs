using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    public bool IsReturnZero = false;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    // begin dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    // during dragging
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }
    // end dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        // Debug.Log("1111111111");
        if (this.transform.localPosition.y >= 0 && this.transform.localPosition.y <= 500)
        {
            this.transform.localPosition = new Vector3(0, 0, 0);

        }
    }

    /// <summary>
    /// set position of the dragged game object
    /// </summary>
    /// <param name="eventData"></param>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        // transform the screen point to world point int rectangle
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            float x = globalMousePos.x, y = globalMousePos.y;
            // Debug.Log("x:" + x + "y:" + y);
            rt.position = IsReturnZero ? CheckPos(x, y) : new Vector2(x, y);
        }
    }
    Vector2 CheckPos(float x, float y)
    {
        if (x < 960)
            x = 960;
        else if (x > 960)
            x = 960;
        if (y > 2000)
            y = 2000;
        else if (y < 540)
            y = 540;
        return new Vector2(x, y);
    }
}