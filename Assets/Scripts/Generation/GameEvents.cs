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
    public delegate void boost(float speed);

    public event equip OnEquip;
    public event boost OnSpeedBoost;

    public void Equip()
    {
        if (OnEquip != null)
        {
            OnEquip();
        }
    }

    public void BoostSpeed(float speed)
    {
        if (OnSpeedBoost != null)
        {
            OnSpeedBoost(speed);
        }
    }

}
