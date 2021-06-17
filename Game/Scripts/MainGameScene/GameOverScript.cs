using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip clickAudio;
    
    int score;
    int currHp;
    int reward;
    public GameObject gameOverScreen;
    int rewardEq = 5;
    CoinAndScoreGain CoinAndScoreGainScript;
    Health healthScript;
    bool gameIsOver;
    public Text gameOverScoreText, gameOverRewardText;
    
    

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameIsOver = false;
        healthScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
        CoinAndScoreGainScript = GameObject.FindWithTag("GameController").GetComponent<CoinAndScoreGain>();
        gameOverScreen.SetActive(false);
    }

    void SetTextState() {
        gameOverScoreText.text = "Вы набрали: " + score + " очков";
        gameOverRewardText.text = "Ваша награда: " + reward + " coins";
    }

    
    void Update()
    {
        currHp = Health.currentHp;
        
        if (currHp <= 0 && !gameIsOver) {
            ShowGameOverScreen();
            gameIsOver = true;
        }
    }

    public void ShowGameOverScreen() {
        Time.timeScale = 0;
        score = CoinAndScoreGain.currentScore;
        reward = score / 60 * rewardEq;
        CoinAndScoreGainScript.playerData.coinAmount += reward;

        var newHighscore = new HighscoreInfoModel(score);
        SaveLoadSystem.SaveHighscore(newHighscore);
        
        SetTextState();
        gameOverScreen.SetActive(true);
        
    }

    public void QuitToMainMenu () {
        audioSource.PlayOneShot(clickAudio);
        CoinAndScoreGainScript.SavePlayer();
        SceneManager.LoadScene("MainMenu");
        
    }

    public void Restart() {
        audioSource.PlayOneShot(clickAudio);
        CoinAndScoreGainScript.SavePlayer();
        SceneManager.LoadScene("MainGameScene");
    }
}
