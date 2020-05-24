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
    protected bool drop;


    protected void Start()
    {
        panel = GameObject.FindGameObjectWithTag("InfoScreen");
        IEnumerator c = startCollider(0.3f);
        StartCoroutine(c);
        drop = true;
    }

    public virtual void clicked()
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
        if(collision.gameObject.tag == "Player" && drop)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        drop = true;
    }

    protected void pickedUp()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void dropped(Vector3 pos)
    {
        drop = false;
        Vector3 offset = new Vector3((float)randomNum(Generator.r), (float)randomNum(Generator.r), -0.5f);
        transform.position = pos + offset;
        GetComponent<SpriteRenderer>().enabled = true;
        IEnumerator c = startCollider(1.5f);
        StartCoroutine(c);
    }

    private double randomNum(System.Random r)
    {
        if (r.NextDouble() > 0.5) return (float)r.NextDouble() * 0.9 + 0.25;
        else return (float)r.NextDouble() * -0.9 - 0.25;
    }

    IEnumerator startCollider(float num)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(num);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    
}
