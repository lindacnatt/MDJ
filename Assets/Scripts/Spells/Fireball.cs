using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public DamagingSpell DamageSpellSettings;

    
    private Vector3 Destination;
    private Vector3 Direction;
    public float speed = 1f;

    /*TODO:
        Check if over GUI, otherwise cancel
        Tap once for the destination and remove itself from the lean touch event
        so it doesn't change midway
    */

    // Start is called before the first frame update
    void Start()
    {
        //Lean.Touch.LeanTouch.OnFingerDown += SetDestination;
        Destroy(gameObject, 2.0f);

        Direction = (Destination - transform.position).normalized;
    }

    void OnDisable()
    {
        //Lean.Touch.LeanTouch.OnFingerDown -= SetDestination;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Direction * speed;

        transform.LookAt(transform.position + Direction * speed);

    }

    public void SetDestination(Lean.Touch.LeanFinger finger)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(finger.ScreenPosition), out hit, 100))
        {
            Destination = hit.point;
            Destination.y = 1;
        
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(DamageSpellSettings.damage);
            Destroy(gameObject);

        }
        
    }

}
