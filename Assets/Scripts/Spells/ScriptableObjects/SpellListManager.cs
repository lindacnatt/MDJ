using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Manager", menuName = "Spells/Spell Manager")]
public class SpellListManager : ScriptableObject
{

    [SerializeField] private List<SpellSettings> SpellMap = new List<SpellSettings>();

    public SpellSettings GetSpell(string gestureName)
    {
        return SpellMap.Find(spell => spell.GestureName.Equals(gestureName));
    }
}
