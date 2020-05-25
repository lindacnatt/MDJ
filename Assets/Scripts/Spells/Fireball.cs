using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileSpellBase
{
    public DamagingSpellSettings DamageSpellSettings;

    protected void OnTriggerEnter2D(Collider2D collision)
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
