using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button open;
    public Button close;

    public GameObject inventory;

    public List<Image> slots;
    private List<GameObject> items;
    private int numItems = 0;
    private int maxNumItems;

    public Image chestSlot;
    private GameObject chestplate;
    private bool chestplateOc = false;

    public Image pantSlot;
    private GameObject pant;
    private bool pantOc = false;

    public Image gloveSlot;
    private GameObject glove;
    private bool gloveOc = false;

    public Image bootSlot;
    private GameObject boot;
    private bool bootOc = false;

    public Image inkSlot;
    private GameObject inkTank;
    private bool inkOc = false;

    public Image backSlot;
    private GameObject backpack;
    private bool backOc = false;


    void Start()
    {
        items = new List<GameObject>();
        maxNumItems = 4;
        int i = 1;
        foreach(Image g in slots)
        {
            if (i > maxNumItems) g.transform.parent.gameObject.SetActive(false);
            i++;
        }
    }

    public bool setChest(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Chest)
        {
            chestplate = c;
            chestplateOc = true;
            return true;
        }
        return false;
    }

    public bool setPant(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Pant)
        {
            pant = c;
            pantOc = true;
            return true;
        }
        return false;
    }
    public bool setGlove(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Glove)
        {
            glove = c;
            gloveOc = true;
            return true;
        }
        return false;
    }
    public bool setBoot(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Boot)
        {
            boot = c;
            bootOc = true;
            return true;
        }
        return false;
    }

    public bool setInkT(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.InkTank)
        {
            inkTank = c;
            inkOc = true;
            return false;
        }
        return false;
    }

    public bool setBackpack(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Backpack)
        {
            backpack = c;
            backOc = true;
            return false;
        }
        return false;
    }

    public bool addItem(GameObject i)
    {
        if(numItems < maxNumItems)
        {
            items.Add(i);
            slots[numItems].sprite = i.GetComponent<SpriteRenderer>().sprite;
            slots[numItems].color = new Color32(255, 255, 225, 255);
            numItems++;
            return true;
        }
        return false;
    }

    public bool equipItem(GameObject i)
    {
        switch (i.GetComponent<Item>().type)
        {
            case Item.ItemType.Backpack:
                return setBackpack(i);
            case Item.ItemType.Boot:
                return setBoot(i);
            case Item.ItemType.Chest:
                return setChest(i);
            case Item.ItemType.Pant:
                return setPant(i);
            case Item.ItemType.InkTank:
                return setInkT(i);
            case Item.ItemType.Glove:
                return setGlove(i);
        }
        return false;
    }

    public void removeItem(int i)
    {
        items.RemoveAt(i);
        slots[i].sprite = null;
        numItems--;
    }

    public void showInv()
    {
        open.enabled = false;
        inventory.SetActive(true);
    }

    public void closeInv()
    {
        inventory.SetActive(false);
        open.enabled = true;
    }
}
