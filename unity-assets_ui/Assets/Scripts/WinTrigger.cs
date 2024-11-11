using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>End the timer when the player touche the winflag</summary>
public class WinTrigger : MonoBehaviour
{
    // Get the player gameobject to set timer to false
    public GameObject playerTimer;
    // Set the timer text for the canvas
    public Text FinalTime;
    public GameObject winCanvas;
    public GameObject timerCanvas;


    // End the timer
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerTimer.GetComponent<Timer>().enabled = false;
            winCanvas.SetActive(true);
            timerCanvas.SetActive(false);
            Time.timeScale = 0;
            Cursor.visible = true;
            playerTimer.GetComponent<Timer>().Win();
        }
    }
}
