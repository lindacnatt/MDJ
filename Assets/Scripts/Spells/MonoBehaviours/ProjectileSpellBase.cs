using UnityEngine;
using UnityEditor;

public abstract class ProjectileSpellBase : SpellBase
{
    void Update()
    {
        transform.position += Direction * Speed;
    }

    public override void SetDestination(Vector2 target)
    {
        Vector3 temp = new Vector3(target.x, target.y, 0);
        Destination = temp;

        Direction = (Destination - transform.position);
        Direction.z = 0;
        Direction = Direction.normalized;
    }
}