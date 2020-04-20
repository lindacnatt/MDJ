using UnityEngine;
using UnityEngine.AI;

public class MoveToClick : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        Lean.Touch.LeanTouch.OnFingerDown += HandleFingerTap;
    }

    void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerTap;
    }

    void HandleFingerTap(Lean.Touch.LeanFinger finger)
    {
        //Ignore GUI
        if (!finger.IsOverGui)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(finger.ScreenPosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }
}