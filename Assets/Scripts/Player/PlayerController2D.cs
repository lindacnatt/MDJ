using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;


    //QUICK WORKAROUND TODO
    public bool HasSpell;
    private Spell currentPrimedSpell;

    [SerializeField] private FloatEvent onHPChanged = null;
    [SerializeField] private FloatEvent onInkChanged = null;
    [SerializeField] private SpellEvent onSpellPrimed = null;

    //Values
    private float currentHP;
    private float currentInk;


    private bool Knockback = false;
    private Vector3 direction;

    //Raise an event if we change the Ink
    public float CurrentInk { get => currentInk; 
        set {
            currentInk = value;
            onInkChanged.Raise(currentInk);
        } 
    }

    //Raise an event if the HP changes
    //TODO: Raise an event if the player dies instead
    //TODO: Don't forget to clamp the hp between 0 and maxHP as well!
    public float CurrentHP { get => currentHP; 
        set
        {
            currentHP = value;
            onHPChanged.Raise(currentHP);
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;



        //Don't forgot to set the current hp/ink values at start to get the UI working (fire off events)
        CurrentHP = CurrentInk = 100;
        
        onSpellPrimed.Raise(null);
    }

    void OnEnable()
    {
        Lean.Touch.LeanTouch.OnFingerDown += HandleFingerTap;
        agent.enabled = true;
    }

    void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerDown -= HandleFingerTap;
    }

    void HandleFingerTap(Lean.Touch.LeanFinger finger)
    {

        //Use spell instead of moving
        if(HasSpell)
        {

            //TODO: We have an interface for now
            //If we need anything else let me know - Rafael
            var target = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
            Instantiate(currentPrimedSpell.SpellPrefab, transform.position, Quaternion.identity).GetComponent<ISpell>().SetDestination(target);         
            
            HasSpell = false;
            onSpellPrimed.Raise(null);
        }
        //Ignore GUI
        else if (!finger.IsOverGui)
        {

            var target = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
            target.z = 0;
            agent.destination = target;
            DebugDrawPath(agent.path.corners);

            //RaycastHit hit;

            //if (Physics.Raycast(Camera.main.ScreenPointToRay(finger.ScreenPosition), out hit, 100))
            //{
            //    agent.destination = hit.point;
            //}
        }
    }


    public static void DebugDrawPath(Vector3[] corners)
    {
        if (corners.Length < 2) { return; }
        int i = 0;
        for (; i < corners.Length - 1; i++)
        {
            Debug.DrawLine(corners[i], corners[i + 1], Color.blue);
        }
        Debug.DrawLine(corners[0], corners[1], Color.red);
    }

    public void AssignSpell(Spell spell)
    {
        if (spell == null)
        {
            Debug.LogWarning("No spell was received.");
            return;
        }
            
        if(spell.InkCost > CurrentInk)
        {
            Debug.Log("Player didn't have enough Ink for this spell.");
            return;
        }

        HasSpell = true;
        currentPrimedSpell = spell;
        onSpellPrimed.Raise(spell);

        CurrentInk -= spell.InkCost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            direction = transform.position - collision.gameObject.transform.position;
            direction = direction.normalized;

            StartCoroutine(AddKnockback(direction));

            //Take damage
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
    }

    #region Knockback

    //Check this out: https://www.youtube.com/watch?v=gFq0lO2E2Sc
    private void FixedUpdate()
    {
        DebugDrawPath(agent.path.corners);

        if (Knockback)
        {
            //TODO: try to refactor this in it's own class. We use navAgents on enemies too and we might want to have enemies have
            //knockback
            //Reset path needs to be here otherwise knockback won't happen if the navagent hasn't a set destination
            //TODO: Side effect is if we get knocked our previous path will be discarded. Think about this.
            //Check if the agent has a set path, etc... I'm sure navagent has a way to check
            //https://forum.unity.com/threads/setting-navmeshagent-velocity-manually-not-really-working-all-the-time.718055/
            agent.ResetPath();
            agent.velocity = direction * 8;
        }
            
    }

    //TODO: refactor this (on it's own class/function with the rest)
    //Cache the values at start and then reapply everything neatly
    IEnumerator AddKnockback(Vector3 direction)
    {
        Knockback = true;
        agent.speed = 10;
        agent.angularSpeed = 0;
        agent.acceleration = 20;

        yield return new WaitForSeconds(.2f);

        Knockback = false;
        agent.speed = 3.5f;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
    }
    #endregion
}