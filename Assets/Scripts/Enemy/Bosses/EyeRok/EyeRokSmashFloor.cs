using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRokSmashFloor : EyeRokState
{
    private readonly float delayBeforeAttack = 1.5f;
    private readonly float handSmashVelocity = 15.0f;
    private readonly float timeHandsInGround = 2.0f;

    private Vector2 leftHandDestination, rightHandDestination;

    /*
     * Okay, so now we just have to decrement the y positions of the hands until we reach the floor and enable the colliders when they hit the floor 
     * As for the hands, have a hand on the current position of the player and make the other hand be a random position within a circle
     * Spawn a prefab or something of a circle in the positions where the hands are going to fall into
     */

    private bool leftHandFinished, rightHandFinished, handsCooldown = false;
    public override void OnEnter(EyeRok eyeRok)
    {
        //Calculate hand positions based on the player position
        //Left hand on top of the player
        leftHandDestination = eyeRok.Player.transform.position;

        //Right hand on a circle around the player
        rightHandDestination = GetLocationAroundPoint(eyeRok.Player.transform.position, 1.5f, 3.0f);

        //Spawn prefabs
        var leftHandWarning = Object.Instantiate(eyeRok.HandWarningPrefab, leftHandDestination, Quaternion.identity);
        var rightHandWarning = Object.Instantiate(eyeRok.HandWarningPrefab, rightHandDestination, Quaternion.identity);

        //Also get rid of the prefab at the end
        eyeRok.StartCoroutine(SmashFloor(eyeRok.LeftHand, leftHandDestination, eyeRok.LeftHandCollider, () =>
        {
            leftHandFinished = true;
            Object.Destroy(leftHandWarning);
        }));

        eyeRok.StartCoroutine(SmashFloor(eyeRok.RightHand, rightHandDestination, eyeRok.RightHandCollider, () =>
        {
            rightHandFinished = true;
            Object.Destroy(rightHandWarning);
        }));
        
    }

    
    public override EyeRokState Update(EyeRok eyeRok)
    {
        if(leftHandFinished && rightHandFinished)
        {
            eyeRok.StartCoroutine(WaitForSeconds(timeHandsInGround, () => handsCooldown = true));
            if (handsCooldown)
                return new EyeRokPatrol();
            else
                return null;
        }
        else
        {
            return null;
        }
    }

    IEnumerator SmashFloor(GameObject hand, Vector2 handDestination, BoxCollider2D handCollider, System.Action action)
    {
        yield return new WaitForSeconds(delayBeforeAttack);

        while(!ArrivedAtDestination(hand.transform.position, handDestination))
        {
            hand.transform.position = MovePosition(hand.transform.position, handDestination, handSmashVelocity);
            yield return null;
        }

        //We arrived at the position
        //Turn on the collider
        handCollider.enabled = true;

        action.Invoke();
    }
}
