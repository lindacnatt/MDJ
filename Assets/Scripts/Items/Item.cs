using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Rarity { common, rare, legend, }
    public enum ItemType { Spell, Boot, Pant, Chest, Glove, Ink, Health, InkTank, Backpack, Other, }

    public Rarity rare;
    public ItemType type;

    public int value;

}
