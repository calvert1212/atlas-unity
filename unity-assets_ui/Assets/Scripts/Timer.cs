using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Handle the timer in game</summary>
public class Timer : MonoBehaviour
{
    // Set the timer text for the canvas
    public Text TimerText;
    // Catch the delta time for the timer
    private float timer = 0f;
    // Divide the time by 60 for the minutes
    private float minutes;
    // Modulo the time by 60 for the seconds
    private float seconds;
    public Text FinalTime;

    /// <summary>Final time !</summary>
    public void Win()
    {
        FinalTime.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;

        TimerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}
