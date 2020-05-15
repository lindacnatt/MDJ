using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : EnemyController2D
{
    

    public override void Move()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < sightRange)
        {
            base.Move();
        }
    }
}
