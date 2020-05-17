using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragH : LeanSelectableBehaviour
{
    private Vector3 pos;

    public int index;
    public Item.ItemType type;

    private RectTransform panel;

    protected override void OnSelect(LeanFinger finger)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        FindObjectOfType<Inventory>().use(index, type);
        this.GetComponent<LeanSelectable>().IsSelected = false;
    }

    protected override void OnDeselect()
    {
        pos = this.transform.parent.transform.position;
    }

    protected override void Start()
    {
        base.Start();
        pos = this.transform.position;
        Debug.Log(pos);
        panel = GameObject.FindGameObjectWithTag("garbage").transform.GetComponent<RectTransform>();
    }

    
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
