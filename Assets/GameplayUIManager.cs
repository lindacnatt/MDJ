using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    //TODO workaround
    public PlayerController Player;

    public Text UIHPText;
    public Text UIInkText;
    public Text UISpellText;
    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UIHPText.text = "Health: " + Player.CurrentHP;
        UIInkText.text = "Ink: " + Player.CurrentInk;
        UISpellText.text = Player.HasSpell ? "Fireball" : "";
        
    }
}
