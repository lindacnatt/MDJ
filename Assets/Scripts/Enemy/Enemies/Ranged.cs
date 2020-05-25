using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : EnemyController2D
{
    protected int rangeMove = 4;
    protected int projSpeed = 3;
    protected float cooldownTime = 2.5f;
    protected bool cooldown = false;

    [SerializeField] private DamagingSpellSettings fireballSpell = null; 
    void Update()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < sightRange)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) > rangeMove)
            {
                base.Move();
            }
            else
            {
                Shoot();
            }
        }
        
    }

    public void Shoot()
    {
        if(!cooldown)
        {

            GameObject fireballSpawned = Instantiate(fireballSpell.SpellPrefab, transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(boxCollider, fireballSpawned.GetComponent<CircleCollider2D>());
            fireballSpawned.GetComponent<ISpell>().SetDestination(Player.transform.position);

            
            cooldown = true;
            StartCoroutine(wait(cooldownTime));
        }
        
    }

    IEnumerator wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        cooldown = false;

    }
}

