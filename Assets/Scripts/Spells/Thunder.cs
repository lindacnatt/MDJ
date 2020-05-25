using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AOESpellBase
{
    [SerializeField] private AOESpellSettings AOESpellSettings = null;
    [SerializeField] private CircleCollider2D Hurtbox = null;
    [SerializeField] private SpriteRenderer BaseSprite = null;   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateHurtbox());
        StartCoroutine(IncreaseAlpha());

        //Stay for extra time in the ground and then destroy itself
        StartCoroutine(Death());

    }

    IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(AOESpellSettings.delay + 1.0f);
        Destroy(gameObject);
    }

    IEnumerator IncreaseAlpha()
    {
        float elapstedTime = 0;
        Color temp = BaseSprite.color;

        while(elapstedTime < AOESpellSettings.delay)
        {
            //TODO: magic constants looool
            //0 at start, 60 at end. 

            float t = (elapstedTime / AOESpellSettings.delay);

            //Exponential lerp
            //temp.a = Mathf.SmoothStep(0, 60, t * t);
            temp.a = LerpEaseInExpo(0, 60, t);

            BaseSprite.color = temp;

            elapstedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Make sure we finish at 60f alpha
        temp.a = 60f;
        BaseSprite.color = temp;
    }

    IEnumerator ActivateHurtbox()
    {
        yield return new WaitForSecondsRealtime(AOESpellSettings.delay);
        Hurtbox.enabled = true;        
    }

    public override bool OnSpellPrimed()
    {
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController2D>().TakeDamage(AOESpellSettings.damage * DamageMultiplier);
            Destroy(gameObject);

        }
    }

    static float LerpEaseInExpo(float from, float to, float t)
    {
        return ExpoEaseIn(t, from, to - from, 1.0f);
    }


    public static float ExpoEaseIn(float current, float initalValue, float totalChange, float duration)
    {
        return (current == 0) ? initalValue : totalChange * (float)Mathf.Pow(2, 10 * (current / duration - 1)) + initalValue - totalChange * 0.001f;
    }

}
