using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    float timeRemaining;
    bool timerRunning;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 300f;
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0 && timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTimeRemaining(timeRemaining);
        }
        else if(timeRemaining <= 0 && timerRunning)
        {
            timerRunning = false;
            Debug.Log("Player Lost");
            timerText.text = "00:00";
        }
        
    }
    
    public void DisplayTimeRemaining(float timeDisplay)
    {
        float timeMin = Mathf.FloorToInt(timeDisplay / 60);
        float timeSec = Mathf.FloorToInt(timeDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", timeMin, timeSec);
    }
}
