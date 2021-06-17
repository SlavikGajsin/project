using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coinPrefab;
    float randomX, randomY;
    bool canSpawnCoins, canSpawnBonus;
    public float spawningCooldownCoins,spawningCooldownBonus;
    float currentTimeCoins,currentTimeBonus;
    public GameObject healthBonusPrefab, scoreBonusPrefab, shieldBonusPrefab, attackBonusPrefab;

    void Start()
    {
        canSpawnCoins = true;
        canSpawnBonus = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCoins();
        SpawnBonus();
    }

    void SpawnCoins() {
        if (canSpawnCoins) {
            int randomFigure = Random.Range(0, 3);
            if (randomFigure == 0) {
                SpawnCoinFigure1();
            }
            else if (randomFigure == 1) {
                SpawnCoinFigure2();
            }
            else if (randomFigure == 2) {
                SpawnCoinFigure3();
            }
            currentTimeCoins = spawningCooldownCoins;
            canSpawnCoins = false;
        }
        else {
            currentTimeCoins -= Time.deltaTime;
            if (currentTimeCoins <= 0) {
                canSpawnCoins = true;
            }
        }
    }

    void SpawnCoinFigure1() {
        randomX = Random.Range(-7.5f, 7.5f);
        randomY = Random.Range(5.5f, 7f);
        float yPlus = 0f;
        for (int i = 0; i < 4; i += 1) {
            Instantiate(coinPrefab, new Vector3(randomX, randomY + yPlus, 0f), Quaternion.identity);
            Instantiate(coinPrefab, new Vector3(randomX + 0.4f, randomY + yPlus, 0f), Quaternion.identity);
            Instantiate(coinPrefab, new Vector3(randomX + 0.8f, randomY + yPlus, 0f), Quaternion.identity);
            Instantiate(coinPrefab, new Vector3(randomX + 1.2f, randomY + yPlus, 0f), Quaternion.identity);
            yPlus += 0.4f;
        }
    }

    void SpawnCoinFigure2() {
        randomX = Random.Range(-7.5f, 7.5f);
        randomY = Random.Range(5.5f, 7f);
        float yPlus = 0f;
        for (int i = 0; i < 8; i += 1) {
            Instantiate(coinPrefab, new Vector3(randomX, randomY + yPlus, 0f), Quaternion.identity);
            yPlus += 0.4f;               
        }
    }

    void SpawnCoinFigure3() {
        randomX = Random.Range(-7.5f, 7.5f);
        randomY = Random.Range(5.5f, 7f);
        float yPlus = 0f;
        float xPlus = 0.9f;
        for (int i = 0; i < 4; i += 1) {
            Instantiate(coinPrefab, new Vector3(randomX + xPlus, randomY + yPlus, 0f), Quaternion.identity);
            yPlus += 0.4f;
            xPlus *= 0.8f;               
        }
        for (int i = 0; i < 5; i += 1) {
            Instantiate(coinPrefab, new Vector3(randomX + xPlus, randomY + yPlus, 0f), Quaternion.identity);
            yPlus += 0.4f;
            xPlus /= 0.8f;               
        }
    }

    void SpawnBonus() {
        if (canSpawnBonus) {
            randomX = Random.Range(-7.5f, 7.5f);
            randomY = Random.Range(5.5f, 7f);
            int randomBonus = Random.Range(0, 4);
            if (randomBonus == 0) {
                Instantiate(healthBonusPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            }
            else if (randomBonus == 1) {
                Instantiate(scoreBonusPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            }
            else if (randomBonus == 2) {
                Instantiate(shieldBonusPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            }
            else if (randomBonus == 3) {
                Instantiate(attackBonusPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            }
            
            currentTimeBonus = spawningCooldownBonus;
            canSpawnBonus = false;
        }
        else {
            currentTimeBonus -= Time.deltaTime;
            if (currentTimeBonus <= 0) {
                canSpawnBonus = true;
            }
        }
            
    }
    
}
