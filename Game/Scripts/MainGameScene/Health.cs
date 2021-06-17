using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    int hp;
    int currentShip;
    public bool choseShip1, choseShip2;
    public static int currentHp;
    float percentage;

    public GameObject healthTextObject;
    Text healthText;

    public GameObject player;
    Vector3 playerPos;
    public GameObject healthBarGameObject;
    Slider healthBar;
    public GameObject firstShipPrefab, secondShipPrefab;
    PlayerData playerData = new PlayerData();

    void Awake() {
        playerData = SaveLoadSystem.LoadPlayer();
        hp = 100;
        currentHp = hp;
        currentShip = PlayerPrefs.GetInt("ShipChosen", 1);
        GetMaxHp(currentShip);
        CreateShip();

        
    }

    void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        
        
        healthBar = healthBarGameObject.GetComponent<Slider>();
        playerPos = player.transform.position;
        healthText = healthTextObject.GetComponent<Text>();
    }

    void CreateShip() {
        Vector3 posToSpawn = new Vector3(0f, -4f, 0f);
        if (currentShip == 1) {
            Instantiate(firstShipPrefab, posToSpawn, Quaternion.identity);
        }
        if (currentShip == 2) {
            Instantiate(secondShipPrefab, posToSpawn, Quaternion.identity);
        }
    }

    public void GetMaxHp(int coef) {
       if (currentShip == 1) {
           hp = 40 + playerData.firstShipLevel * 5;
           currentHp = hp;
       }
       if (currentShip == 2) {
           hp = 100 + playerData.secondShipLevel * 5;
           currentHp = hp;
       }
    }

    public void GainHealth(int healthToGain) {
        if (healthToGain + currentHp > hp) {
            currentHp = hp;
        }
        else {
            currentHp += healthToGain;
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        UpdateHealthBar();
        
    }

    void UpdateHealthBar() {
        percentage = (float)currentHp / (float)hp;
        healthBar.value = percentage;
        healthText.text = currentHp.ToString() + " HP";
    }

    public void TakeDamage(int damage) {
        currentHp -= damage;
        if (currentHp <= 0) {
            currentHp = 0;
            player.SetActive(false);
            
        }
    }
}
