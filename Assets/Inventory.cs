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

    private List<Equippable> equipped;

    public Image chestSlot;
    private bool chestplateOc = false;

    public Image pantSlot;
    private bool pantOc = false;

    public Image gloveSlot;
    private bool gloveOc = false;

    public Image bootSlot;
    private bool bootOc = false;

    public Image inkSlot;
    private bool inkOc = false;

    public Image backSlot;
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
            equipped.Add(c.GetComponent<Equippable>());
            chestplateOc = true;
            return true;
        }
        return false;
    }

    public bool setPant(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Pant && !pantOc)
        { 
            pantSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            pantSlot.color = new Color32(255, 255, 225, 255);
            equipped.Add(c.GetComponent<Equippable>());
            pantOc = true;
            return true;
        }
        return false;
    }
    public bool setGlove(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Glove && !gloveOc)
        {
            gloveSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            gloveSlot.color = new Color32(255, 255, 225, 255);
            equipped.Add(c.GetComponent<Equippable>());
            gloveOc = true;
            return true;
        }
        return false;
    }
    public bool setBoot(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.Boot && !bootOc)
        {
            bootSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            bootSlot.color = new Color32(255, 255, 225, 255);
            equipped.Add(c.GetComponent<Equippable>());
            bootOc = true;
            return true;
        }
        return false;
    }

    public bool setInkT(GameObject c)
    {
        if (c.GetComponent<Item>().type == Item.ItemType.InkTank && !inkOc)
        {
            inkSlot.sprite = c.GetComponent<SpriteRenderer>().sprite;
            inkSlot.color = new Color32(255, 255, 225, 255);
            equipped.Add(c.GetComponent<Equippable>());
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
            equipped.Add(c.GetComponent<Equippable>());
            backOc = true;
            int i = 0;
            maxNumItems += c.GetComponent<Equippable>().value;
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

    public float defensiveValue()
    {
        float res = 100;
        foreach(Equippable e in equipped)
        {
            res += e.defenseValue;
        }
        return res/100;
    }

    public float speedValue()
    {
        float res = 100;
        foreach (Equippable e in equipped)
        {
            res += e.speedValue;
        }
        return res / 100;
    }

    public float healthValue()
    {
        float res = 100;
        foreach (Equippable e in equipped)
        {
            res += e.healthValue;
        }
        return res / 100;
    }

    public float inkValue()
    {
        float res = 100;
        foreach (Equippable e in equipped)
        {
            res += e.inkValue;
        }
        return res / 100;
    }

    public void use(int index,Item.ItemType t)
    {
        if (index < 0)
        {
            foreach (Equippable e in equipped)
            {
                Debug.Log("CARALHO2");
                if (t == e.type) {
                    Debug.Log("CARALHO2");
                    e.clicked();
                }
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
