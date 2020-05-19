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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case ItemType.Health:
                    if (collision.gameObject.GetComponent<PlayerController2D>().addHP(value)) Destroy(gameObject);
                    else
                    {
                        if (collision.gameObject.GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
                    }
                    break;
                case ItemType.Ink:
                    if (collision.gameObject.GetComponent<PlayerController2D>().addInk(value)) Destroy(gameObject);
                    else
                    {
                        if (collision.gameObject.GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
                    }
                    break;
                default:
                    if (collision.gameObject.GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
                    break;
            }
        }
        
    }

    protected void pickedUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
