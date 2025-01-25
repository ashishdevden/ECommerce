using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RecyclableScroll : MonoBehaviour
{
    public RectTransform content;
    public ScrollRect scroll;
    public VerticalLayoutGroup verticalLayoutGroup;
    Vector2 pos;
    public List<Item> items;

    float upperLimit;
    float lowerLimit;

    bool itemRepositioned = false;
    Vector2 oldVelocity;
    float heightOfItem;
    float space;

    int i;
    public int total;

    public Action<Item, int> UpdateContent;

    void Start()
    {
        SetInitialContent();

        heightOfItem = items[0].GetComponent<RectTransform>().sizeDelta.y;
        upperLimit = 2 * heightOfItem + 500;
        lowerLimit = 400;
        space = verticalLayoutGroup.spacing;
        i = 0;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        pos = content.anchoredPosition;


        if (pos.y > upperLimit && (i + items.Count) < total)
        {
            oldVelocity = scroll.velocity;
            content.GetChild(0).transform.SetAsLastSibling();

            UpdateContent.Invoke(items[i % items.Count], i + items.Count);
            //items[i % items.Count].text.text = (i + items.Count).ToString();
            i++;

            content.anchoredPosition = new Vector2(pos.x, pos.y - heightOfItem - space);
            itemRepositioned = true;
        }
        if (pos.y < lowerLimit && i > 0)
        {
            oldVelocity = scroll.velocity;
            content.GetChild(items.Count - 1).transform.SetAsFirstSibling();
            i--;
            UpdateContent.Invoke(items[i % items.Count], i);
            // items[i % items.Count].text.text = (i).ToString();
            content.anchoredPosition = new Vector2(pos.x, pos.y + heightOfItem + space);
            itemRepositioned = true;
        }

        if (itemRepositioned)
        {
            scroll.velocity = oldVelocity;
            itemRepositioned = false;
        }
    }

    public void ResetScroll()
    {

        scroll.velocity = Vector2.zero;
        content.anchoredPosition = new Vector2(pos.x, 0);
        items = new List<Item>();
        for (int k = 0; k < content.childCount; k++)
        {
            Item item = content.GetChild(k).GetComponent<Item>();
            items.Add(item);
        }

        SetInitialContent();

        i = 0;
    }


    public void SetInitialContent()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i < total)
            {
                UpdateContent.Invoke(items[i], i);
                //items[i].text.text = i.ToString();
                items[i].gameObject.SetActive(true);
            }
            else
                items[i].gameObject.SetActive(false);
        }
    }

}
