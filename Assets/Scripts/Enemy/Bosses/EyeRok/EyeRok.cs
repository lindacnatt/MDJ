using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EyeRok : MonoBehaviour
{

    public NavMeshAgent navAgent = null;

    public GameObject LeftHand = null;
    public GameObject RightHand = null;

    public BoxCollider2D LeftHandCollider = null;
    public BoxCollider2D RightHandCollider = null;


    private EyeRokState currentState;

    public GameObject Player { get; internal set; }



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        LeftHandCollider = LeftHand.GetComponent<BoxCollider2D>();
        RightHandCollider = RightHand.GetComponent<BoxCollider2D>();

        currentState = new EyeRokPatrol();
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        EyeRokState state = currentState.Update(this);
        if(state != null)
        {
            currentState = state;

            currentState.OnEnter(this);
        }
    }

    

}
