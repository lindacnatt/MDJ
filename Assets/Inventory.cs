using System;
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
    private readonly int MAXITEMS = 4;

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
        equipped = new List<Equippable>();
        maxNumItems = MAXITEMS;
        for(int j = 0; j < maxNumItems; j++)
        {
            items.Add(null);
        }
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

    public void removeChest()
    {
        chestSlot.sprite = null;
        chestSlot.color = new Color32(255, 255, 255, 0);
        foreach (Equippable e in equipped)
        {
            if (e.type == Item.ItemType.Backpack)
            {
                equipped.Remove(e);
                break;
            }
        }
        chestplateOc = false;
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

    public void removePant()
    {

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

    public void removeGlove()
    {

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

    public void removeBoot()
    {

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

    public void removeInkT()
    {

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

    public void removeBack()
    {
        backSlot.sprite = null;
        backSlot.color = new Color32(255, 255, 225, 0);
        foreach(Equippable e in equipped)
        {
            if (e.type == Item.ItemType.Backpack)
            {
                equipped.Remove(e);
                break;
            }
        }
        backOc = false;
        maxNumItems = MAXITEMS;
        int i = 1;
        foreach (Image g in slots)
        {
            if (i > maxNumItems) g.transform.parent.gameObject.SetActive(false);
            i++;
        }
    }

    public bool addItem(GameObject i)
    {
        if(numItems < maxNumItems)
        {
            for(int j = 0; j < maxNumItems; j++)
            {
                if(items[j] == null)
                {
                    items[j] = i.GetComponent<Item>();
                    slots[j].sprite = i.GetComponent<SpriteRenderer>().sprite;
                    slots[j].color = new Color32(255, 255, 225, 255);
                    numItems++;
                    return true;
                }
            }

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

    public void removeItem(int i,Item.ItemType t)
    {
        if (t != Item.ItemType.Other)
        {
            switch (t)
            {
                case Item.ItemType.Backpack:
                    removeBack();
                    break;
                case Item.ItemType.Boot:
                    removeBoot();
                    break;
                case Item.ItemType.Chest:
                    removeChest();
                    break;
                case Item.ItemType.Pant:
                    removePant();
                    break;
                case Item.ItemType.InkTank:
                    removeInkT();
                    break;
                case Item.ItemType.Glove:
                    removeGlove();
                    break;
            }
        }
        else
        {
            items.RemoveAt(i);
            slots[i].sprite = null;
            slots[i].color = new Color32(255, 255, 255, 0);
            numItems--;
        }
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
                if (t == e.type)  e.clicked();
            }
        }
        else
        {
            if (items[index] != null)  items[index].clicked();
        }
        
    }


    private bool switchIAux(int index1, Item.ItemType type1, int index2, Item.ItemType type2)
    {
        if (index1 > -1)
        {
            //switch items
            if (items[index1] != null)
            {
                //both in inventory
                if (type2 == Item.ItemType.Other)
                {
                    swap(index1, index2);
                }
                //item 2 is equipped
                else
                {
                    //diferent types of items
                    if (items[index1].type != type2)
                    {
                        return false;
                    }
                    //same type -> can switch
                    else
                    {
                        swapE(index1, type2);
                    }
                }
                
            }
            //remove 2 from equipped
            else
            {
                
                foreach (Equippable e in equipped)
                {
                    if (e.type == type2)
                    {
                        items[index1] = e as Item;
                        slots[index1].sprite = e.GetComponent<SpriteRenderer>().sprite;
                        slots[index1].color = new Color32(255, 255, 255, 255);
                        numItems++;
                        break;
                    }
                }
                switch (type2)
                {
                    case Item.ItemType.Chest:
                        removeChest();
                        break;
                    case Item.ItemType.Pant:
                        removePant();
                        break;
                    case Item.ItemType.Glove:
                        removeGlove();
                        break;
                    case Item.ItemType.Boot:
                        removeBoot();
                        break;
                    case Item.ItemType.Backpack:
                        removeBack();
                        break;
                    case Item.ItemType.InkTank:
                        removeInkT();
                        break;
                }

            }
        }
        return false;
    }

    private void swap(int index1, int index2)
    {
        Item i1 = items[index1];
        Sprite s = slots[index1].sprite;
        Color c = slots[index1].color;

        items[index1] = items[index2];
        slots[index1].sprite = slots[index2].sprite;
        slots[index1].color = slots[index2].color;

        items[index2] = i1;
        slots[index2].sprite = s;
        slots[index2].color = c;

    }

    private void swapE(int index1, Item.ItemType t)
    {
        if (equipped.Count == 0)
        {
            if(items[index1] != null)
            {
                equipItem(items[index1].gameObject);
                slots[index1].sprite = null;
                slots[index1].color = new Color32(255, 255, 255, 0);
                items[index1] = null;
                numItems--;
            }
        }
        else
        {
            Item i1 = items[index1];
            if (i1 == null) numItems++;
            Sprite s = slots[index1].sprite;
            Color c = slots[index1].color;
            for (int i = 0; i < equipped.Count; i++)
            {
                if (equipped[i].type == t)
                {
                    items[index1] = equipped[i];
                    switch (t)
                    {
                        case Item.ItemType.Chest:
                            items[index1] = equipped[i];
                            slots[index1].sprite = chestSlot.sprite;
                            slots[index1].color = chestSlot.color;
                            chestSlot.sprite = s;
                            chestSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                        case Item.ItemType.Pant:
                            items[index1] = equipped[i];
                            slots[index1].sprite = pantSlot.sprite;
                            slots[index1].color = pantSlot.color;
                            pantSlot.sprite = s;
                            pantSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                        case Item.ItemType.Glove:
                            items[index1] = equipped[i];
                            slots[index1].sprite = gloveSlot.sprite;
                            slots[index1].color = gloveSlot.color;
                            gloveSlot.sprite = s;
                            gloveSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                        case Item.ItemType.Boot:
                            items[index1] = equipped[i];
                            slots[index1].sprite = bootSlot.sprite;
                            slots[index1].color = bootSlot.color;
                            bootSlot.sprite = s;
                            bootSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                        case Item.ItemType.Backpack:
                            items[index1] = equipped[i];
                            slots[index1].sprite = backSlot.sprite;
                            slots[index1].color = backSlot.color;
                            backSlot.sprite = s;
                            backSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                        case Item.ItemType.InkTank:
                            items[index1] = equipped[i];
                            slots[index1].sprite = inkSlot.sprite;
                            slots[index1].color = inkSlot.color;
                            inkSlot.sprite = s;
                            inkSlot.color = c;
                            equipped[i] = i1 as Equippable;
                            break;
                    }
                }
            }
        }

    }

    public bool switchItems(int index1, Item.ItemType type1, int index2, Item.ItemType type2)
    {
        Debug.Log(index1 + " " +index2);
        //both equipped
        if (index1 == index2) return false;
        //item 1 is in inventory
        if (index1 > -1) return switchIAux(index1, type1, index2, type2);
        //item 1 is equipped item 2 is in inventory ->switch
        else  return switchIAux(index2, type2, index1, type1);
        
    }

    private void Update()
    {

    }
}
