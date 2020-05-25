using UnityEngine;

public class EnemyFireball : Fireball
{
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(DamageSpellSettings.damage);
            Destroy(gameObject);
        }
    }
}
