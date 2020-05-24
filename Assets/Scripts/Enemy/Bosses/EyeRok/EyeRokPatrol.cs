using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRokPatrol : EyeRokState
{
    //TODO: Put this in a scriptable object for EyeRok?
    [SerializeField] private float MaxWalkRange = 10.0f;
    
    private float sightRange = 10.0f;

    public static Vector2 LeftHandInitialLocalPosition  = new Vector2(-1, -1);
    public static Vector2 RightHandInitialLocalPosition = new Vector2(1, -1);

    private float handSpeed = 7.0f;
    private bool handNearEyeRok = false;


    public override void OnEnter(EyeRok eyeRok)
    {
        //TODO: Is there a way to get a position within a specific boundary (i.e: the boss room)?
        //For now We'll make it so the boss chooses a random location around it's previous location.
        //It's not good but it's honest work   
        Vector2 newDestination = GetLocationAroundPoint(eyeRok.transform.position, MaxWalkRange / 2.0f, MaxWalkRange);               
        SetDestination(eyeRok, newDestination);
        
    }

    public override EyeRokState Update(EyeRok eyeRok)
    {

        //Check if player is in range
        if (PositionInRange(eyeRok.transform.position, eyeRok.Player.transform.position, sightRange) 
            && handNearEyeRok && ReachedDestination(eyeRok))
        {
            //We reached the destination so prepare for attacking
            //Return a new state here
            //TODO: for now return null to check if patrol works
            return new EyeRokThrowHandsUp();
                       
        }
        else
        {
            //Bring hands close to EyeRok
            Vector2 leftHandWorldPos = eyeRok.LeftHand.transform.position;
            Vector2 rightHandWorldPos = eyeRok.RightHand.transform.position;

            Vector3 LeftHandInitialWorldPosition = eyeRok.transform.TransformPoint(LeftHandInitialLocalPosition);
            Vector3 RightHandInitialWorldPosition = eyeRok.transform.TransformPoint(RightHandInitialLocalPosition);

            eyeRok.LeftHand.transform.position = MovePosition(leftHandWorldPos, LeftHandInitialWorldPosition, handSpeed);
            eyeRok.RightHand.transform.position = MovePosition(rightHandWorldPos, RightHandInitialWorldPosition, handSpeed);

            //If the hands are in their initial position (near EyeRok)
            if (ArrivedAtDestination(eyeRok.LeftHand.transform.position, LeftHandInitialWorldPosition) 
                && ArrivedAtDestination(eyeRok.RightHand.transform.position, RightHandInitialWorldPosition))
            {
                handNearEyeRok = true;
            }
                
            return null;
        }
    }

    bool PositionInRange(Vector2 currentPos, Vector2 target, float sightRange)
    {
        return Vector2.Distance(currentPos, target) < sightRange;
    }

    void SetDestination(EyeRok eyeRok, Vector2 destination)
    {
        eyeRok.navAgent.destination = destination;
    }

    bool ReachedDestination(EyeRok eyeRok)
    {
        //TODO: Test
        //NOTE: This only works if auto-braking is turned ON in the navAgent
        //It's on by default so it should be ok
        return !eyeRok.navAgent.hasPath;
    }
}
