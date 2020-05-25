using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : ProjectileSpellBase
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
                Destroy(gameObject);
            }
        }
    }

    public override bool OnSpellPrimed()
    {
        return false;
    }
}
