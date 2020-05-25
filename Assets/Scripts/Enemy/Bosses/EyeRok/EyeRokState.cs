using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EyeRokState
{
    public abstract void OnEnter(EyeRok eyeRok);
    
    //Return type is optional
    public abstract EyeRokState Update(EyeRok eyeRok);

    public static Vector2 GetLocationAroundPoint(Vector3 position, float minRadius, float maxRadius)
    {
        float randomRadius = Random.Range(minRadius, maxRadius);
        
        Vector2 newLocation = (Vector2)position + (Random.insideUnitCircle.normalized * randomRadius);

        return newLocation;
    }

    public static Vector2 MovePosition(Vector2 currentPosition, Vector2 destination, float speed)
    {
        float step = speed * Time.deltaTime;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, destination, step);

        return newPosition;
    }

    public static bool ArrivedAtDestination(Vector2 position, Vector2 destination)
    {
        if (Vector2.Distance(position, destination) < 0.001f)
            return true;
        else
            return false;
    }

    public static IEnumerator WaitForSeconds(float time, System.Action action)
    {
        yield return new WaitForSecondsRealtime(time);

        action.Invoke();
    }
}
