using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // 1 -> UP
    // 2 -> DOWN
    // 3 -> LEFT
    // 4 -> RIGHT

    public Vector2 position;
    public int direction;
    public bool connected;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        connected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
