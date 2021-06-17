using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int currentVolumeEq;
    PlayerData playerData = new PlayerData();
    public Button musicButton;
    Image musicButtonImage;
    public Sprite soundOnIcon, soundOffIcon;
    public GameObject firstShipContainer, secondShipContainer, chooseShipScreen, mainMenuContainer;
    public Text noFirstShipText, noSecondShipText;

    AudioSource audioSource;
    public AudioClip clickAudio;

    public Slider slider;
    float gameSpeed;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
        chooseShipScreen.SetActive(false);
        musicButtonImage = musicButton.GetComponent<Image>();
        currentVolumeEq = PlayerPrefs.GetInt("VolumeEq", 1);
        LoadPlayerInfo();
        CheckVolumeChanges();
    }

    void CheckVolumeChanges() {
        if (currentVolumeEq == 1) {
            AudioListener.pause = false;
            musicButtonImage.sprite = soundOnIcon;
        }
        else if (currentVolumeEq == 0) {
            AudioListener.pause = true;
            musicButtonImage.sprite = soundOffIcon;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        audioSource.PlayOneShot(clickAudio);
        mainMenuContainer.SetActive(false);
        chooseShipScreen.SetActive(true);
        PrepareChooseShipScreen();
    }

    public void TableRecord() {
        audioSource.PlayOneShot(clickAudio);
        SceneManager.LoadScene("Highscores");
    }

    public void ChangeVolume() {
        
        if (currentVolumeEq == 0) {
            audioSource.PlayOneShot(clickAudio);
            PlayerPrefs.SetInt("VolumeEq", 1);
            currentVolumeEq = 1;
            AudioListener.pause = false;
            audioSource.PlayOneShot(clickAudio);
            musicButtonImage.sprite = soundOnIcon;
        }
        else if (currentVolumeEq == 1) {
            PlayerPrefs.SetInt("VolumeEq", 0);
            currentVolumeEq = 0;
            AudioListener.pause = true;
            audioSource.PlayOneShot(clickAudio);
            musicButtonImage.sprite = soundOffIcon;
        }
    }

    public void OpenShop() {
        audioSource.PlayOneShot(clickAudio);
        SavePlayerInfo();
        SceneManager.LoadScene("Shop");
    }

    public void QuitGame() {
        audioSource.PlayOneShot(clickAudio);
        Application.Quit();
    }

    //Методы сохранения и загрузки данных игрока

    public void LoadPlayerInfo() {
        playerData = SaveLoadSystem.LoadPlayer();
        if (playerData == null) {
            Debug.Log("asdasd");
            playerData = new PlayerData();
            //playerData.highScore = 0;
            playerData.coinAmount = 20;
            playerData.firstShipLevel = 0;
            playerData.secondShipLevel = 0;
            SavePlayerInfo();
        }
    }

    public void SavePlayerInfo() {
        SaveLoadSystem.SavePlayer(playerData);
    }

    //Методы экрана выбора корабля

    void PrepareChooseShipScreen() {
        if (playerData.firstShipLevel == 0) {
            firstShipContainer.SetActive(false);
            noFirstShipText.text = "У вас нет первого корабля! Перейдите в магазин, чтобы приобрести его!";
        }
        if (playerData.secondShipLevel == 0) {
            secondShipContainer.SetActive(false);
            noSecondShipText.text = "У вас еще нет второго корабля!";
        }
    }

    public void ChooseFirstShip() {
        audioSource.PlayOneShot(clickAudio);
        PlayerPrefs.SetInt("ShipChosen", 1);
        SavePlayerInfo();
        SaveSliderValue();
        SceneManager.LoadScene("MainGameScene");
    }

    public void ChooseSecondShip() {
        audioSource.PlayOneShot(clickAudio);
        PlayerPrefs.SetInt("ShipChosen", 2);
        SavePlayerInfo();
        SaveSliderValue();
        SceneManager.LoadScene("MainGameScene");
    }

    public void BackFromChoosingToMenu() {
        audioSource.PlayOneShot(clickAudio);
        chooseShipScreen.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

    public void SaveSliderValue() {
        if (slider.value == 2) {
            gameSpeed = 0.7f;
        }
        if (slider.value == 1) {
            gameSpeed = 0.85f;
        }
        if (slider.value == 0) {
            gameSpeed = 1f;
        }
        PlayerPrefs.SetFloat("GameSpeed", gameSpeed);
    }

    public void OpenControllerScene() {
        SceneManager.LoadScene("GameInfo");
    }
}
