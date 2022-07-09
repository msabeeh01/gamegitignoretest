using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class finalTimer : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerScript.currentTime = TimerScript.currentTime;
        currentTimeText.text = TimerScript.time.ToString(@"mm\:ss\:fff");
    }
}
