using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragH : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IDropHandler
{
    private Vector3 pos;

    public int index;
    public Item.ItemType type;

    private RectTransform panel;
    public void OnDrop(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition))
        {
            Debug.Log("WIJENMOWEMFaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaWE");
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = pos;
    }


    void Start()
    {
        pos = this.transform.position;
        Debug.Log(pos);
        panel = GameObject.FindGameObjectWithTag("garbage").transform.GetComponent<RectTransform>();
    }

    
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (type == Item.ItemType.Other)
        {
            FindObjectOfType<Inventory>().use(index, type);
        }
    }
}
