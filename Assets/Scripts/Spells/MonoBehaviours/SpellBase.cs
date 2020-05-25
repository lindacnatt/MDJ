using UnityEngine;
using System.Collections;

public abstract class SpellBase : MonoBehaviour, ISpell
{
    protected Vector3 Destination;
    protected Vector3 Direction;
    public float Speed = 1f;
    public float DamageMultiplier = 1f;

    public void SetDamageMultipler(float mult)
    {
        DamageMultiplier *= mult;
    }

    public abstract bool OnSpellPrimed();
    public abstract void SetDestination(Vector2 target);

}
