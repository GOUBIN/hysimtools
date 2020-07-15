using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class HET_Anchor
{

    static List<RectTransform> rectList = new List<RectTransform>();
    [MenuItem("Hysim/UI工具/一键锚点（不创建预制体）")]
    public static void makeAnchors()
    {
        GameObject obj = Selection.activeObject as GameObject;
        if (obj != null)
        {
            getChild(obj.GetComponent<RectTransform>());
            if (rectList.Count > 0)
            {
                for (int i = 0; i < rectList.Count; i++)
                {
                    xx(rectList[i]);
                }
            }
            else
            {
                Debug.Log("没有子物体");
            }
        }
        rectList.Clear();
    }


    public static void getChild(RectTransform rect)
    {
  
        if (rect.childCount>0)
        {
            for (int i = 0; i < rect.childCount; i++)
            {
                RectTransform rectTransform = rect.GetChild(i).GetComponent<RectTransform>();
                rectList.Add(rectTransform);
                getChild(rectTransform);

            }
        }
    }

    public static void xx(RectTransform transform)
    {
        //获取父物体组件
        RectTransform rect = transform.parent.GetComponent<RectTransform>();

        float w = rect.rect.width;
        float h = rect.rect.height;

        Vector2 pos = new Vector2(rect.rect.x,rect.rect.y);
        rect.pivot = Vector2.zero;

        float distanceX = transform.localPosition.x - transform.rect.width / 2;
        float distanceY = transform.localPosition.y - transform.rect.height / 2;

        float MinX = distanceX / w;
        float MinY = distanceY / h;

        float manX = MinX + transform.rect.width / w;
        float manY = MinY + transform.rect.height / h;

        transform.anchorMin = new Vector2(MinX, MinY);
        transform.anchorMax = new Vector2(manX, manY);

        transform.offsetMax = transform.offsetMin = Vector2.zero;

        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.rect.Set(pos.x, pos.y, w, h);

    }

}
