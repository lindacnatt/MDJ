using UnityEngine;
using System.Collections;

public abstract class AOESpellBase : SpellBase
{
    protected Vector3 Center;

    public override void SetDestination(Vector2 target)
    {
        Vector3 temp = new Vector3(target.x, target.y, 0);
        Center = temp;

        transform.position = Center;
    }
}
