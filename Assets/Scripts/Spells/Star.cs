using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ProjectileSpellBase
{
   public DamagingSpellSettings DamageSpellSettings;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(DamageSpellSettings.damage * DamageMultiplier);
            Destroy(gameObject);

        }
    }

    public override bool OnSpellPrimed()
    {
        return false;
    }
}
