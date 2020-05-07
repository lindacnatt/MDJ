using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // brown - 1 + 1
    // green - 1 + 2
    // blue - 2 + 1
    // purple - 2 + 2
    // golden - 3 + 1
    public enum ChestType {brown, green, blue, purple, golden, }
    public ChestType tipo;
    public GameObject[] items;
    public int numItems;


    public void OpenChest()
    {
        foreach(GameObject g in items)
        {
            System.Random r = new System.Random();
            Item i = FindObjectOfType<Item>();
            if (i.type == Item.ItemType.Ink)
            {
                switch (tipo)
                {
                    case ChestType.brown:
                        break;
                    case ChestType.green:
                        break;
                    case ChestType.blue:
                        break;
                    case ChestType.purple:
                        break;
                    case ChestType.golden:
                        break;
                }
            }
        }
    }

    public void Generate(ChestType type, System.Random r)
    {
        //just to instantiate with different numbers n shit
        if (type == ChestType.golden) CreateGolden(r);
        else if (type == ChestType.purple) CreatePurple(r);
        else if (type == ChestType.blue) CreateBlue(r);
        else if (type == ChestType.green) CreateGreen(r);
        else CreateBrown(r);
    }

    private void CreateBrown(System.Random r)
    {
        numItems = r.Next(1, 10);
        if (numItems > 5) numItems = 2;
        else numItems = 1;
        items = new GameObject[numItems];
        tipo = ChestType.brown;
        items[0] = ItemArrays.commonItems[0];
        for(int i = 1; i < numItems; i++)
        {
            int next = r.Next(0, 20);
            if (next > 19) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 15) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];
        }
        
    }

    private void CreateGreen(System.Random r)
    {
        numItems = r.Next(1, 20);
        if (numItems > 15) numItems = 3;
        else if (numItems > 10) numItems = 2;
        else numItems = 1;
        items = new GameObject[numItems];
        tipo = ChestType.green;
        items[0] = ItemArrays.commonItems[0];
        for (int i = 1; i < numItems; i++)
        {
            int next = r.Next(0, 20);
            if (next > 18) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 13) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];

        }
    }

    private void CreateBlue(System.Random r)
    {
        numItems = r.Next(1, 10);
        if (numItems > 5) numItems = 3;
        else numItems = 2;
        items = new GameObject[numItems];
        tipo = ChestType.blue;
        items[0] = ItemArrays.commonItems[0];
        for (int i = 1; i < numItems; i++)
        {
            int next = r.Next(0, 20);
            if (next > 17) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 11) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];
        }
    }

    private void CreatePurple(System.Random r)
    {
        numItems = r.Next(1, 20);
        if (numItems > 15) numItems = 4;
        else if (numItems > 10) numItems = 3;
        else numItems = 2;
        items = new GameObject[numItems];
        tipo = ChestType.purple;
        items[0] = ItemArrays.commonItems[0];
        for (int i = 1; i < numItems; i++)
        {

            int next = r.Next(0, 20);
            if (next > 16) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 9) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];
        }
    }

    private void CreateGolden(System.Random r)
    {
        numItems = r.Next(1, 10);
        if (numItems > 5) numItems = 4;
        else numItems = 3;
        items = new GameObject[numItems];
        tipo = ChestType.golden;
        items[0] = ItemArrays.commonItems[0];
        for (int i = 1; i < numItems; i++)
        {
            int next = r.Next(0, 20);
            if (next > 15) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 7) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];
        }
    }



}
