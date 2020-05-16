using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArrays : MonoBehaviour
{
    public static GameObject[] commonItems;
    public static GameObject[] rareItems;
    public static GameObject[] legendItems;

    public GameObject[] commonItems1;
    public GameObject[] rareItems1;
    public GameObject[] legendItems1;

    void Start()
    {
        commonItems = commonItems1;
        rareItems = rareItems1;
        legendItems = legendItems1;
    }
}
