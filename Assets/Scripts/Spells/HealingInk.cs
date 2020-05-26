using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingInk : HealingSpellBase
{
    private PlayerController2D player;

    [SerializeField] private HealingSpellSettings healingSpellSettings = null;

    // Start is called before the first frame update
    void Start()
    {
        timeToHeal = 5.0f;
        player = FindObjectOfType<PlayerController2D>();

        //Do the heal here
        StartCoroutine(HealOverTime());
    }


    IEnumerator HealOverTime()
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / timeToHeal;

        while (ratio < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / timeToHeal;
            
            player.CurrentInk += (healingSpellSettings.amountToHeal / timeToHeal) * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
