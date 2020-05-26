using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedo : SpellBase
{
    [SerializeField] private Boost speed = null;

    public override bool OnSpellPrimed()
    {
        return true;
    }

    public override void SetDestination(Vector2 target)
    {
    }

    private void Start()
    {
        GameEvents.current.BoostSpeed(speed.value);
        StartCoroutine(disable());
    }

    IEnumerator disable()
    {
        yield return new WaitForSeconds(6.54321f);

        GameEvents.current.BoostSpeed(1/speed.value);
    }

}
