using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Linq;

public class CoinAndScoreGain : MonoBehaviour
{
    
    public static int currentScore;
    
    public Text currentScoreText, highScoreText, coinText;

    public PlayerData playerData = new PlayerData();
    int highScore, amountToGain;

    float gameSpeed;

    void Start()
    {
        var highScoresArray = SaveLoadSystem.GetAllHighscores();
        highScoresArray = highScoresArray.OrderByDescending(u=>u.score).ToArray();
        Debug.Log(highScoresArray[0].score);
        highScore = highScoresArray[0].score;
        LoadPlayer();
        gameSpeed = PlayerPrefs.GetInt("GameSpeed");
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHighScore();
        UpdateTextScore();
        UpdateCoinAmount();
    }

    public void GainScore(int scoreToGain) {
        amountToGain = scoreToGain;
        if (gameSpeed == 0.85f) {
            amountToGain += 1;
        }
        if (gameSpeed == 1) {
            amountToGain = (int)(scoreToGain * 1.5);
        }
        
        currentScore += amountToGain;
    }

    public void GainCoins(int amount) {
        
        playerData.coinAmount += amount;
    }

    void CheckHighScore() {
        if (currentScore >= highScore) {
            highScore = currentScore;
        }
    }

    void UpdateTextScore() {
        currentScoreText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "Highscore: " + highScore.ToString();
    }

    void UpdateCoinAmount() {
        coinText.text = " " + playerData.coinAmount;
    }

    

    public void SavePlayer() {
        SaveLoadSystem.SavePlayer(playerData);
    }

    public void LoadPlayer() {
        playerData = SaveLoadSystem.LoadPlayer();
        
    }
}
