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
        switch (type)
        {
            case ItemType.Health:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().addHP(value)) Destroy(gameObject);
                else
                {
                    Debug.Log("wat");
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
                }
                break;
            case ItemType.Ink:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().addInk(value)) Destroy(gameObject);
                else
                {
                    Debug.Log("wat");
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
                }
                break;
            default:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().addItem(gameObject))  pickedUp();
                break;
        }
    }

    protected void pickedUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
