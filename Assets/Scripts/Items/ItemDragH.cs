using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragH : LeanSelectableBehaviour
{

    public int index;
    public Item.ItemType type;

    private RectTransform panel;

    private ItemDragH other;


    protected override void OnSelect(LeanFinger finger)
    {

    }

    protected override void OnDeselect()
    {
        Vector2 pos = (transform as RectTransform).anchoredPosition;
        if (Vector2.Distance(pos,Vector2.zero) < 50)
        {
            (transform as RectTransform).anchoredPosition = Vector2.zero;
            FindObjectOfType<Inventory>().use(index, type);
        }
        else
        {
            if(findRect())
            {
                if (!FindObjectOfType<Inventory>().switchItems(index,type, other.index, other.type))
                {
                    (transform as RectTransform).anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                (transform as RectTransform).anchoredPosition = Vector2.zero;
            }
        }
    }

    private bool findRect()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("InvSlot");
        foreach(GameObject g in slots)
        {
            Vector2 pos = (transform.parent as RectTransform).anchoredPosition;
            if(Vector2.Distance(pos,(g.transform as RectTransform).anchoredPosition) < 50)
            {
                foreach(Transform c in g.transform)
                {
                    other = c.gameObject.GetComponent<ItemDragH>();
                    return true;
                }
            }
            
        }
        return false;
    }

    protected override void Start()
    {
        base.Start();
        panel = GameObject.FindGameObjectWithTag("garbage").transform.GetComponent<RectTransform>();
    }

    
}
