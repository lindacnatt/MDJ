using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Rarity { common, rare, legend, }
    public enum ItemType { Spell, Boot, Pant, Chest, Glove, Ink, Health, InkTank, Backpack, Other, }

    public Rarity rare;
    public ItemType type;

    public string itemName;

    public string description;

    public int value;

    protected GameObject panel;


    protected void Start()
    {
        panel = GameObject.FindGameObjectWithTag("InfoScreen");
    }

    public void clicked()
    {
        foreach(Transform c in panel.transform)
        {
            if (c.name == "ItemImage")
            {
                changeImage(c.GetComponent<Image>());
            }
            else if (c.name == "Title")
            {
                c.GetComponent<TextMeshProUGUI>().text = itemName;
            }
            else if(c.name == "Description")
            {
                c.GetComponent<TextMeshProUGUI>().text = description;
            }
        }
        panel.GetComponent<Canvas>().enabled = true;
    }

    protected virtual void changeImage(Image i)
    {
        i.sprite = this.GetComponent<SpriteRenderer>().sprite;
    }
    public void close()
    {
        panel.GetComponent<Canvas>().enabled = false;
    }
}
