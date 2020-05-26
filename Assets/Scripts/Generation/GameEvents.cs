using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public delegate void equip();

    public event equip OnEquip;

    public void Equip()
    {
        if (OnEquip != null)
        {
            OnEquip();
        }
    }

}
