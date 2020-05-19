using UnityEngine;

public class EnemyFireball : Fireball
{
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController2D>().TakeDamage(DamageSpellSettings.damage);
            Destroy(gameObject);
        }
    }
}
