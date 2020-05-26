using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealingSpellBase : SpellBase
{
    protected float timeToHeal;

    public override bool OnSpellPrimed()
    {
        return true;
    }

    public override void SetDestination(Vector2 target)
    {
        //Empty
    }
}
