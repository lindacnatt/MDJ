using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2D : MonoBehaviour
{
    public EnemyData EnemyData;

    private float CurrentHP;

    NavMeshAgent NavAgent;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.updateRotation = false;
        NavAgent.updateUpAxis = false;

        Player = GameObject.FindGameObjectWithTag("Player");
        CurrentHP = EnemyData.HP;

        NavAgent.speed = EnemyData.WalkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        NavAgent.destination = Player.transform.position;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
