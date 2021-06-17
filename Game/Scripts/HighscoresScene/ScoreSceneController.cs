using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class ScoreSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] scoreTexts = new Text[10];
    public Text[] dateTexts = new Text[10];
    
    AudioSource audioSource;
    public AudioClip clickAudio;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
        SetScoreArray();
    }

    void SetScoreArray() {
        var highScoresArray = SaveLoadSystem.GetAllHighscores();
        highScoresArray = highScoresArray.OrderByDescending(u=>u.score).ToArray();
        Debug.Log(highScoresArray);
        int i = 0;
        
        foreach (var model in highScoresArray) {
            Debug.Log(model.score);
            if (model != null) {
                scoreTexts[i].text = (i+1).ToString() + ")" + "  " + model.score.ToString();
                dateTexts[i].text = model.date.ToString("d", DateTimeFormatInfo.InvariantInfo);
            }
            i++;
            if (i == 10) break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu() {
        audioSource.PlayOneShot(clickAudio);
        SceneManager.LoadScene("MainMenu");
    }
}
