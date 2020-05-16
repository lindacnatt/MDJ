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
}
