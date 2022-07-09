using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    public static float currentTime;
    public static int startMinutes;
    public static TimeSpan time;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        time = TimeSpan.FromSeconds(currentTime);
        
    }
    
}

