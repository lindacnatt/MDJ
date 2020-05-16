using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : Item
{

    public Sprite spellSigil;

    private void Start()
    {
        panel = GameObject.FindGameObjectWithTag("SigilScreen");
    }

    protected override void changeImage(Image i)
    {
        i.sprite = spellSigil;
    }

}
