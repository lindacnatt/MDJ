﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startscreen : MonoBehaviour
{
     public AudioSource backgroundmusic;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame(){
       Time.timeScale = 1; 
       backgroundmusic.Play();
    }
}
