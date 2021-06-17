using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    bool paused;
    AudioSource audioSource;
    public AudioClip clickAudio;
    
    CoinAndScoreGain coinAndScoreGainScript;
    GameSpeedController gameSpeedController;

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameSpeedController = gameObject.GetComponent<GameSpeedController>();
        coinAndScoreGainScript = GameObject.FindWithTag("GameController").GetComponent<CoinAndScoreGain>();
        paused = false;
        pauseScreen.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused) {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused) {
            ResumeGame();
        }
    }

    public void PauseGame() {
        audioSource.PlayOneShot(clickAudio);
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        paused = true;
        
    }
    
    public void ResumeGame() {
        audioSource.PlayOneShot(clickAudio);
        gameSpeedController.SetGameSpeed();
        pauseScreen.SetActive(false);
        paused = false;
        
    }


    public void QuitToMainMenu () {
        audioSource.PlayOneShot(clickAudio);
        coinAndScoreGainScript.SavePlayer();
        SceneManager.LoadScene("MainMenu");

    }

    public void Restart() {
        audioSource.PlayOneShot(clickAudio);
        coinAndScoreGainScript.SavePlayer();
        SceneManager.LoadScene("MainGameScene");
        
    }
}
