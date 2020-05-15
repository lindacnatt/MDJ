using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : EnemyController2D
{
    protected int range = 5;
    protected int rangeMove = 4;
    protected int projSpeed = 3;

    private void Start()
    {
        
    }

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
                Invoke("Shoot", 1);
            }
        }
        
    }

    public void Shoot()
    {

    }

    IEnumerator wait(float lag)
    {
        yield return new WaitForSeconds(lag);
    }
}

