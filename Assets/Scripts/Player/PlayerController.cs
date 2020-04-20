using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;

    public Joystick joystick;

    //QUICK WORKAROUND TODO
    public bool HasSpell;
    public GameObject FireballPrefab;


    //Values
    public float CurrentHP;
    public float CurrentInk;

    public bool JoystickMode = true;
    private float velocity = 5f;

 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CurrentHP = CurrentInk = 100;
        if (JoystickMode)
        {

        }
    }

    private void Update()
    {
        this.transform.Translate(Time.deltaTime * velocity*joystick.Horizontal,0, Time.deltaTime * velocity * joystick.Vertical);
    }

    void OnEnable()
    {
        if(!JoystickMode)
        Lean.Touch.LeanTouch.OnFingerDown += HandleFingerTap;
    }

    void OnDisable()
    {
        if (!JoystickMode)
            Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerTap;
    }

    void HandleFingerTap(Lean.Touch.LeanFinger finger)
    {

        //Use spell instead of moving
        if(HasSpell)
        {
            Instantiate(FireballPrefab, transform.position, Quaternion.identity).GetComponent<Fireball>().SetDestination(finger);
            
            HasSpell = false;
        }
        //Ignore GUI
        else if (!finger.IsOverGui)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(finger.ScreenPosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    public void AssignSpell()
    {

        //TODO: Incomplete of course.
        //For now we just "equip" the only spell we have, the fireball
        //Later on, hopefully we can just pass the apropriate spell we get from the recognizer.
        //But we need a good architecture for that lol

        //TODO: read this from the spell SO and check if we have enough ink to cast the spell
        CurrentInk -= 10;


        //If we have enough boom
        if (CurrentInk > 0)
            HasSpell = true;
        else
            CurrentInk = 0;
    }
}