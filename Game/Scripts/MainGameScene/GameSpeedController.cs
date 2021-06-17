using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    // Start is called before the first frame update
    float gameSpeed;

    void Awake() {
        gameSpeed = PlayerPrefs.GetFloat("GameSpeed", 0.75f);
    }

    void Start()
    {
        SetGameSpeed();
    }

    public void SetGameSpeed() {
        Time.timeScale = gameSpeed;
    }
}
