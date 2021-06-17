using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUIController : MonoBehaviour
{
    float timer;
    public Text timeText;
    string minutes;
    string seconds;

    void Start()
    {
        timer = 0f;
    }

    
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer() {
        timer += Time.deltaTime;
        minutes = GetZero((int)Mathf.Round(timer) / 60);
        seconds = GetZero((int)Mathf.Round(timer) - ((int)Mathf.Round(timer) / 60) * 60);
        timeText.text = minutes + ":" + seconds;
    }

    string GetZero(int value) {
        if (value < 10) {
            return "0" + value.ToString();
        }
        else {
            return value.ToString();
        }
    }
}
