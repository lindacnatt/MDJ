using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemy", menuName = "Enemies/Base Enemy")]
public class EnemyData : ScriptableObject
{
    public float HP;
    public float WalkingSpeed;
}
