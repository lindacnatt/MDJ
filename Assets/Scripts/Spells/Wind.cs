﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : ProjectileSpellBase
{
    public DamagingSpellSettings DamageSpellSettings;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            var IDamageableGameObject = collision.gameObject.GetComponent<IDamageable>();
            if (IDamageableGameObject != null)
            {
                IDamageableGameObject.TakeDamage(DamageSpellSettings.damage * DamageMultiplier);
 
            }
        }
    }

    public override void SetDestination(Vector2 target)
    {
        base.SetDestination(target);
        StartCoroutine(EyeRokState.WaitForSeconds(3.0f, () => Destroy(gameObject)));
    }

    public override bool OnSpellPrimed()
    {
        return false;
    }
}
