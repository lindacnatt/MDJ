using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public void closeCanvas()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
}
