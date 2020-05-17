using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button open;
    public Button close;
   
    public void PauseGame(){
        Time.timeScale = 0;
        open.enabled = false;
        GetComponent<Canvas>().enabled = true;
    }
    public void ResumeGame(){
       Time.timeScale = 1; 
       open.enabled = true;
       GetComponent<Canvas>().enabled = false;
    }

    
}
