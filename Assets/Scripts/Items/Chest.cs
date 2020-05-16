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
    private System.Random r;


    public void Generate(ChestType type)
    {
        r = Generator.r;
        //just to instantiate with different numbers n shit
        if (type == ChestType.golden) CreateGolden();
        else if (type == ChestType.purple) CreatePurple();
        else if (type == ChestType.blue) CreateBlue();
        else if (type == ChestType.green) CreateGreen();
        else CreateBrown();

    }

    private void CreateBrown()
    {
        numItems = r.Next(1, 20);
        if (numItems > 15) numItems = 2;
        else numItems = 1;
        items = new GameObject[numItems];
        tipo = ChestType.brown;
        items[0] = ItemArrays.commonItems[0];
        for(int i = 1; i < numItems; i++)
        {
            int next = r.Next(0, 40);
            if (next > 39) items[i] = ItemArrays.legendItems[r.Next(0, ItemArrays.legendItems.Length)];
            else if (next > 31) items[i] = ItemArrays.rareItems[r.Next(0, ItemArrays.rareItems.Length)];
            else items[i] = ItemArrays.commonItems[r.Next(0, ItemArrays.commonItems.Length)];
        }
        
    }

    private void CreateGreen()
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

    private void CreateBlue()
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

    private void CreatePurple()
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

    private void CreateGolden()
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



    public void OpenChest()
    {
        foreach (GameObject g in items)
        {
            Item i = g.GetComponent<Item>();
            if (i.type == Item.ItemType.Ink || i.type == Item.ItemType.Health) {
                float v = 1;
                switch (tipo) {
                    case ChestType.brown:
                        v = 0.8f;
                        break;
                    case ChestType.green:
                        v = 0.9f;
                        break;
                    case ChestType.blue:
                        v = 1f;
                        break;
                    case ChestType.purple:
                        v = 1.1f;
                        break;
                    case ChestType.golden:
                        v = 1.2f;
                        break;
                }
                i.value = (int)v * r.Next(9, 12);
            }
            float a = (float)r.NextDouble() + 0.2f;
            float b = (float)r.NextDouble() + 0.2f;
            Vector3 offset = new Vector3(a,b,-0.5f);
            Debug.Log(a+" " +b);
            Instantiate(g, transform.position + offset,Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
