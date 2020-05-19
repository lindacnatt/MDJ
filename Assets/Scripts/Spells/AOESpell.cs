using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOESpell", menuName = "Spells/AOE Damaging Spell")]
public class AOESpell : DamagingSpell
{
    //In game units
    public float radius;
    
    //In seconds
    public float delay;
}
