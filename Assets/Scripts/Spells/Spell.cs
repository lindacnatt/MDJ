using PDollarGestureRecognizer1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public int InkCost;
    public string GestureName;
    public GameObject SpellPrefab;
}
