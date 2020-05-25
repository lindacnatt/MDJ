using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{

    private PlayerController2D p;
    protected new void Start()
    {
        base.Start();
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
    }

    public override void clicked()
    {
        if(type == ItemType.Health)
        {
            if (!p.addHP(value))
            {
                base.clicked();
                Destroy(gameObject);
            }
        }
        if (type == ItemType.Ink)
        {
            if (!p.addInk(value))
            {
                base.clicked();
                Destroy(gameObject);
            }
        }
    }


}
