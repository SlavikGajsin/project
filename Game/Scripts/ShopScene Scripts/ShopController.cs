using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    
    public int[] levelCostShip1 = new int[11];
    int[] levelCostShip2 = new int[11];

    public Button buttonShip1, buttonShip2;
    public Text textCostShip1, textCostShip2, textCurrentLevelShip1, textCurrentLevelShip2;
    public Text textCoinAmount;

    AudioSource audioSource;
    public AudioClip clickAudio;
    

    void CreateLevelCostArrays() { 
        // Массив вида <Стоимость текущего уровня>[<Текущий уровень>]
        // Нулевые элементы хранят стоимость покупки
        
        levelCostShip1[0] = 20;
        levelCostShip2[0] = 500;

        for (int i = 1; i < 10; i++) {
            levelCostShip1[i] = levelCostShip1[i - 1] + 20;
            levelCostShip2[i] = levelCostShip2[i - 1] + 150;
        }
    }
    
    void Awake() {
        LoadPlayerInfo();
        CreateLevelCostArrays();
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        
        SetStartTextState();
    }
    
    void Update()
    {
        
    }

    void UpdateCoinAmount() {
        textCoinAmount.text = playerData.coinAmount.ToString() + " coins";
    }

    public void QuitToMenu() {
        audioSource.PlayOneShot(clickAudio);
        SavePlayerInfo();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPlayerInfo() {
        playerData = SaveLoadSystem.LoadPlayer();
    }

    public void SavePlayerInfo() {
        SaveLoadSystem.SavePlayer(playerData);
    }

    void SetStartTextState() {
        if (playerData.firstShipLevel == 10) {
                textCostShip1.text = "MAX";
            }
        else {
            textCostShip1.text = levelCostShip1[playerData.firstShipLevel].ToString();
        }

        if (playerData.secondShipLevel == 10) {
            textCostShip2.text = "MAX";
        }
        else {
            textCostShip2.text = levelCostShip2[playerData.secondShipLevel].ToString();
        }

        textCurrentLevelShip1.text = playerData.firstShipLevel.ToString() + "/10";
        textCurrentLevelShip2.text = playerData.secondShipLevel.ToString() + "/10";

        textCoinAmount.text = playerData.coinAmount.ToString() + " coins";
    }

    public void UpdateBuyButtonShip1() {
        if (playerData.coinAmount >= levelCostShip1[playerData.firstShipLevel] && playerData.firstShipLevel != 10)
        {
            playerData.coinAmount -= levelCostShip1[playerData.firstShipLevel];
            playerData.firstShipLevel += 1;
            textCurrentLevelShip1.text = playerData.firstShipLevel.ToString() + "/10";

            if (playerData.firstShipLevel == 10) {
                textCostShip1.text = "MAX";
            }
            else {
                textCostShip1.text = levelCostShip1[playerData.firstShipLevel].ToString();
            }
        }
        
        UpdateCoinAmount();
    }

    public void UpdateBuyButtonShip2() {
        if (playerData.coinAmount >= levelCostShip2[playerData.secondShipLevel] && playerData.secondShipLevel != 10)
        {
            Debug.Log(playerData.secondShipLevel);
            playerData.coinAmount -= levelCostShip2[playerData.secondShipLevel];
            playerData.secondShipLevel += 1;
            textCurrentLevelShip2.text = playerData.secondShipLevel.ToString() + "/10";

            if (playerData.secondShipLevel == 10) {
                textCostShip2.text = "MAX";
            }
            else {
                textCostShip2.text = levelCostShip2[playerData.secondShipLevel].ToString();
            }
        }

        UpdateCoinAmount();
    }

}
