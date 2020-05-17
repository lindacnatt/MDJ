using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private Text UIHPText = null;
    [SerializeField] private Text UIInkText = null;
    [SerializeField] private Text UISpellText = null;
    [SerializeField] public Slider HealthSlider = null;
    [SerializeField] public Image HealthFill = null;
    [SerializeField] public Slider InkSlider = null;
   
    
    public void UpdateHp(float newHP)
    {
        UIHPText.text = "HP: " + newHP;
        HealthSlider.value = newHP; 
        if (newHP < 30){
            HealthFill.color = Color.red;
        }
        if (newHP > 30 && newHP < 60){
            HealthFill.color = Color.yellow;
        }
    }

    public void UpdateInk(float newInk)
    {
        UIInkText.text = "Ink: " + newInk;
        InkSlider.value = newInk;
    }

    public void UpdateSpellPrimed(Spell spell)
    {
        UISpellText.text = spell == null ? "" : spell.name;
    }
}
