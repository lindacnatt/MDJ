using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{   
    public void PauseGame(){
        Time.timeScale = 0;
    }
    public void ResumeGame(){
       Time.timeScale = 1; 
    }
}
