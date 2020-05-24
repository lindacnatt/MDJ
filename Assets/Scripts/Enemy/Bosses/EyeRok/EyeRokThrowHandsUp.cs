using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRokThrowHandsUp : EyeRokState
{

    //TODO: Put this in a scriptable object for EyeRok?
    [SerializeField] private float TimeDelayBeforeAttack = 5.0f;
    private bool HandsReadyToAttack = false;
    public override void OnEnter(EyeRok eyeRok)
    {
        /*
         * Hands should be near the EyeRok from the previous state. Just throw them up in the air.
         * How? Disable the colliders of both hands and increment the Y position LOOOOL
         */

        eyeRok.StartCoroutine(WaitForSeconds(TimeDelayBeforeAttack));
        
        //Disable the colliders 
        eyeRok.LeftHandCollider.enabled = false;
        eyeRok.RightHandCollider.enabled = false;
        
        //And move the hands
        eyeRok.StartCoroutine(ThrowHands(eyeRok.LeftHand));
        eyeRok.StartCoroutine(ThrowHands(eyeRok.RightHand));
    }

    public override EyeRokState Update(EyeRok eyeRok)
    {
        if(HandsReadyToAttack)
        {
            return new EyeRokSmashFloor();
        }
        else
        {
            return null;
        }        
    }

    IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        HandsReadyToAttack = true;
    }

    IEnumerator ThrowHands(GameObject hand)
    {

        float elapsedTime = 0;
        float ratio = elapsedTime / TimeDelayBeforeAttack;

        Vector2 destination = (Vector2)hand.transform.localPosition + new Vector2(0, 7);
        while (ratio < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / TimeDelayBeforeAttack;
            hand.transform.localPosition = MovePosition(hand.transform.localPosition, destination, 7.0f);
            yield return null;
        }
    }
}
