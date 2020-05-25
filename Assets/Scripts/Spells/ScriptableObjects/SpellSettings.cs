using PDollarGestureRecognizer1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellSettings : ScriptableObject
{
    public int InkCost;
    public string GestureName;
    public GameObject SpellPrefab;

    //In case we need it (for statistics or something else)
    [SerializeField] private VoidEvent OnSpellPrimedEvent;
    [SerializeField] private VoidEvent OnSpellCastEvent;
}
