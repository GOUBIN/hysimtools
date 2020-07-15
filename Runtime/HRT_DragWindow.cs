using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HRT_DragWindow : MonoBehaviour,IDragHandler,IPointerDownHandler
{
    public RectTransform dragTarget;//拖动的目标物体
    //上下左右范围限制
    private float leftLimit;
    private float rightLimit;
    private float topLimit;
    private float downLimit;
    private Vector3 offset;
    private void Start()
    {
        //return;
        leftLimit = -Screen.width/2 + dragTarget.sizeDelta.x / 2 - dragTarget.parent.localPosition.x; ;
        rightLimit = Screen.width/2 - dragTarget.sizeDelta.x / 2 - dragTarget.parent.localPosition.x; ;

        topLimit = Screen.height/2 - dragTarget.sizeDelta.y / 2 - dragTarget.parent.localPosition.y;
        downLimit = -Screen.height/2 + dragTarget.sizeDelta.y / 2 - dragTarget.parent.localPosition.y;
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        //return;
        Vector3 temp = Input.mousePosition + offset;

        float x = Mathf.Clamp(temp.x, leftLimit, rightLimit);
        float y = Mathf.Clamp(temp.y, downLimit, topLimit);
        temp = new Vector3(x, y, 0);
        dragTarget.localPosition = temp;
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        //return;
        offset = dragTarget.localPosition - Input.mousePosition;
    }
}
