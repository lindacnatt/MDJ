using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private Dictionary<string, Spell> SpellMap = new Dictionary<string, Spell>();


    // Start is called before the first frame update
    void Start()
    {
        foreach(Spell spell in Resources.FindObjectsOfTypeAll<Spell>())
        {
            SpellMap.Add(spell.Gesture.Name, spell);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
