using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : EnemyController2D
{
    protected int range = 5;
    protected int rangeMove = 5;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Vector3.Distance(transform.position,Player.transform.position) > rangeMove)
        {
            NavAgent.destination = Player.transform.position;
        }
    }
}
