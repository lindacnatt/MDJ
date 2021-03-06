﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Chest;

public class EnemyController2D : MonoBehaviour, IDamageable
{
    public EnemyData EnemyData;

    public enum EnemyRarity { common, rare, legend, }
    public enum EnemyType { melee, ranged, }

    public EnemyRarity rare;
    public EnemyType type;

    protected float CurrentHP;

    protected NavMeshAgent NavAgent;

    protected int sightRange;

    protected GameObject Player;

    protected BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.updateRotation = false;
        NavAgent.updateUpAxis = false;
        rare = EnemyRarity.common;
        type = EnemyType.melee;
        Player = GameObject.FindGameObjectWithTag("Player");
        CurrentHP = EnemyData.HP;
        sightRange = 30;
        NavAgent.speed = EnemyData.WalkingSpeed;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        NavAgent.destination = Player.transform.position;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            Loot();
            Destroy(gameObject);
        }
    }


    private void Loot()
    {
        System.Random r = new System.Random();
        int n = r.Next(0, 200);
        GameObject g;
        switch (rare)
        {
            case EnemyRarity.common:
                if (n > 199)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[4], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.golden);
                }
                else if (n > 194)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[3], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.purple);
                }
                else if (n > 176)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[2], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.blue);
                }
                else if (n > 150)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[1], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.green);
                }
                else
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[0], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.brown);
                }
                break;
            case EnemyRarity.rare:
                if (n > 180)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[4], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.golden);
                }
                else if (n > 140)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[3], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.purple);
                }
                else if (n > 95)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[2], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.blue);
                }
                else if (n > 45)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[1], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.green);
                }
                else
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[0], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.brown);
                }
                break;
            case EnemyRarity.legend:
                if (n > 99)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[4], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.golden);
                }
                else if (n > 55)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[3], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.purple);
                }
                else if (n > 21)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[2], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.blue);
                }
                else if (n > 9)
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[1], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.green);
                }
                else
                {
                    g = Instantiate(FindObjectOfType<Generator>().chests[0], new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
                    g.AddComponent<Chest>().Generate(ChestType.brown);
                }
                break;
        }

    }
}
