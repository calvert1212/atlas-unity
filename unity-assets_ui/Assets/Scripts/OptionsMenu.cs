using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handle all interactions with sliders and buttons
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("yInverted") == 1) ? true : false;
    }
    /// <summary>
    /// Return to the main menu
    /// Use PlayerPref to get the index of the previous scene
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("PreviousScene"));
    }

    /// <summary>
    /// Apply current settings using the data en playerpref
    /// </summary>
    public void Apply()
    {
        PlayerPrefs.SetInt("yInverted", GameObject.Find("InvertYToggle").GetComponent<Toggle>().isOn ? 1 : 0);
    }
}
