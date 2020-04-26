using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private Text UIHPText;
    [SerializeField] private Text UIInkText;
    [SerializeField] private Text UISpellText;

    // Update is called once per frame
    //void Update()
    //{
    //    UIHPText.text = "Health: " + Player.CurrentHP;
    //    UIInkText.text = "Ink: " + Player.CurrentInk;
    //    UISpellText.text = Player.HasSpell ? "Fireball" : "";

    //}

    public void UpdateHp(float newHP)
    {
        UIHPText.text = "Health: " + newHP;
    }

    public void UpdateInk(float newInk)
    {
        UIInkText.text = "Ink: " + newInk;
    }

    //TODO
    void UpdateSpellPrimed()
    {

    }
}
