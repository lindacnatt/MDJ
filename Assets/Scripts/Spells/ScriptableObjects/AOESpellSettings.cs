using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOESpell", menuName = "Spells/AOE Damaging Spell Settings")]
public class AOESpellSettings : DamagingSpellSettings
{
    //In game units
    public float radius;
    
    //In seconds
    public float delay;
}
