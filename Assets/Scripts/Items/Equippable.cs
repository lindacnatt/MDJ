using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : Item
{
    public float defenseValue;
    public float healthValue;
    public float inkValue;
    public float speedValue;

    protected new void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController2D>().equipItem(gameObject)) pickedUp();
            else if (collision.gameObject.GetComponent<PlayerController2D>().addItem(gameObject)) pickedUp();
        }
    }
}
