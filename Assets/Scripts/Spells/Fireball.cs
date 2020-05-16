using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, ISpell
{
    public DamagingSpell DamageSpellSettings;
    
    private Vector3 Destination;
    private Vector3 Direction;
    public float speed = 1f;

    //Update is called once per frame
    void Update()
    {
        transform.position += Direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController2D>().TakeDamage(DamageSpellSettings.damage);
            Destroy(gameObject);

        }
    }

    public bool OnSpellPrimed()
    {
        return false;
    }

    public void SetDestination(Vector2 target)
    {
        Vector3 temp = new Vector3(target.x, target.y, 0);
        Destination = temp;

        Direction = (Destination - transform.position).normalized;
        Direction.z = 0;
    }

}
