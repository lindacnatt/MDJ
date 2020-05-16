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
    private List<Item> items;
    private int numItems = 0;
    private int maxNumItems;

    public Image chestSlot;
    private Item chestplate;
    private bool chestplateOc = false;

    public Image pantSlot;
    private Item pant;
    private bool pantOc = false;

    public Image gloveSlot;
    private Item glove;
    private bool gloveOc = false;

    public Image bootSlot;
    private Item boot;
    private bool bootOc = false;

    public Image inkSlot;
    private Item inkTank;
    private bool inkOc = false;

    public Image backSlot;
    private Item backpack;
    private bool backOc = false;


    void Start()
    {
        items = new List<Item>();
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
        if (c.GetComponent<Item>().type == Item.ItemType.Chest && !chestplateOc)
        {
            chestSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            chestSlot.color = new Color32(255, 255, 225, 255);
            chestplate = c.GetComponent<Item>();
            chestplateOc = true;
            return true;
        }
        return false;
    }

    public bool setPant(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Pant && !pantOc)
        {
            pant = c.GetComponent<Item>();
            pantOc = true;
            return true;
        }
        return false;
    }
    public bool setGlove(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Glove && !gloveOc)
        {
            glove = c.GetComponent<Item>();
            gloveOc = true;
            return true;
        }
        return false;
    }
    public bool setBoot(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Boot && !bootOc)
        {
            boot = c.GetComponent<Item>();
            bootOc = true;
            return true;
        }
        return false;
    }

    public bool setInkT(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.InkTank && !inkOc)
        {
            inkTank = c.GetComponent<Item>();
            inkOc = true;
            return false;
        }
        return false;
    }

    public bool setBackpack(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Backpack && !backOc)
        {
            backSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            backSlot.color = new Color32(255, 255, 225, 255);
            backpack = c.GetComponent<Item>();
            backOc = true;
            int i = 0;
            maxNumItems += backpack.value;
            foreach(Image g in slots)
            {
                if (i < maxNumItems) g.transform.parent.gameObject.SetActive(true);
                i++;
            }
            return true;
        }
        return false;
    }

    public bool addItem(GameObject i)
    {
        if(numItems < maxNumItems)
        {
            items.Add(i.GetComponent<Item>());
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
        inventory.GetComponent<Canvas>().enabled = true;
    }

    public void closeInv()
    {
        inventory.GetComponent<Canvas>().enabled = false;
        open.enabled = true;
    }

    public int defensive()
    {
        int res = 1;
        if (chestplateOc) res *= chestplate.value;
        if (pantOc) res *= pant.value;
        if (bootOc) res *= boot.value;
        return res;
    }
    
    public void use(int index,Item.ItemType t)
    {
        if (index < 0)
        {
            switch (t)
            {
                case Item.ItemType.Chest:
                    Debug.Log("HFEWOIFNIWEF");
                    chestplate.clicked();
                    break;
                case Item.ItemType.Pant:
                    pant.clicked();
                    break;
                case Item.ItemType.Glove:
                    glove.clicked();
                    break;
                case Item.ItemType.Boot:
                    glove.clicked();
                    break;
                case Item.ItemType.Backpack:
                    Debug.Log("HFEWOIFNIWEF");
                    glove.clicked();
                    break;
                case Item.ItemType.InkTank:
                    inkTank.clicked();
                    break;
            }
        }
        else
        {
            if (index < numItems)
            {
                items[index].clicked();
            }
            
        }
        
    }
}
