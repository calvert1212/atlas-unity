using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    public TMP_Text counterText;
    public void SetCounter(int count)
    {
        counterText.text = count.ToString();
    }

    public void SetCounter(string count)
    {
        counterText.text = count;
    }

    public void SetCounter(float count)
    {
        counterText.text = count.ToString();
    }

}
