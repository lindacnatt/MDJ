using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragH : LeanSelectableBehaviour
{

    public int index;
    public Item.ItemType type;

    public RectTransform panel;

    private ItemDragH other;
    private Transform par;



    protected override void OnSelect(LeanFinger finger)
    {
        transform.parent = transform.parent.parent;
        (transform as RectTransform).SetAsLastSibling();
    }

    protected override void OnDeselect()
    {
        transform.parent = par;
        Vector2 pos = (transform as RectTransform).position;
        Vector2 pos2 = (transform.parent as RectTransform).position;
        Debug.Log(pos);
        if (Vector2.Distance(pos, pos2) < 50)
        {
            (transform as RectTransform).position = (transform.parent as RectTransform).position;
            FindObjectOfType<Inventory>().use(index, type);
        }
        else if (outInv())
        {
            FindObjectOfType<Inventory>().dropItem(index, type);
            (transform as RectTransform).position = (transform.parent as RectTransform).position;
        }
        else
        {
            if (findRect())
            {

                FindObjectOfType<Inventory>().switchItems(index, type, other.index, other.type);
                (transform as RectTransform).position = (transform.parent as RectTransform).position;
            } 
            else
            {
                (transform as RectTransform).position = (transform.parent as RectTransform).position;
            }
        }
    }

    private bool outInv()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(panel.transform as RectTransform, (transform as RectTransform).position))
            return false;
        else
            return true;
    }

    private bool findRect()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("InvSlot");
        foreach(GameObject g in slots)
        {
            Vector2 pos = (transform as RectTransform).position;
            if(Vector2.Distance(pos,(g.transform as RectTransform).position) < 50 && g != gameObject.transform.parent.gameObject)
            {
                other = g.GetComponentInChildren<ItemDragH>();
                return true;
            }
            
        }
        return false;
    }

    protected override void Start()
    {
        par = transform.parent;
        base.Start();
        //panel = GameObject.FindGameObjectWithTag("garbage").transform.GetComponent<RectTransform>();
    }

    
}
