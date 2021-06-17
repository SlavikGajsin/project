using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject easyEnemy;
    public GameObject bossEnemy;
    public GameObject middleEnemy;

    public float easyEnemyCooldown, bossSpawnCooldown, middleEnemyCooldown;
    float curTimeEasyEnemy, curTimeBoss, curTimeMiddleEnemy;
    bool canSpawnEasyEnemy, canSpawnBoss, canSpawnMiddleEnemy;
    float randomX, randomY;

    float gameTimer;
    bool increased1, increased2, increased3;

    void Start()
    {
        gameTimer = 0f;
        increased1 = false;
        increased2 = false;
        increased3 = false;  
        curTimeBoss = bossSpawnCooldown;
    }

    void IncreaseHardness() {
        gameTimer += Time.deltaTime;
        if (gameTimer >= 60f && !increased1) {
            increased1 = true;
            easyEnemyCooldown -= 0.5f;
            middleEnemyCooldown -= 1.5f;
        }
        if (gameTimer >= 120f && !increased2) {
            increased2 = true;
            easyEnemyCooldown -= 0.5f;
            bossSpawnCooldown -= 10f;
            middleEnemyCooldown -= 2f;
        }
        if (gameTimer >= 180f && !increased3) {
            increased3 = true;
            easyEnemyCooldown -= 0.5f;
            bossSpawnCooldown -= 10f;
            middleEnemyCooldown -= 2f;
        }
    }

    void Update()
    {
        IncreaseHardness();
        SpawnEasyEnemy();
        SpawnSeveralEasyEnemy();
        SpawnBoss();
        SpawnMiddleEnemy();
        
        
    }

    void SpawnEasyEnemy() {
        if (canSpawnEasyEnemy) {
            randomX = Random.Range(-8f, 8f);
            randomY = Random.Range(5.5f, 7f);
            Vector3 spawnPos = new Vector3(randomX, randomY, 0f);
            Instantiate(easyEnemy, spawnPos, Quaternion.identity);
            curTimeEasyEnemy = easyEnemyCooldown;
            canSpawnEasyEnemy = false;
        }
        else {
            curTimeEasyEnemy -= Time.deltaTime;
            if (curTimeEasyEnemy <= 0) {
                canSpawnEasyEnemy = true;
            }
        }
    }

    void SpawnMiddleEnemy() {
        if (canSpawnMiddleEnemy) {
            var randomXt = Random.Range(-7.5f, 7.5f);
            var randomYt = Random.Range(5.5f, 7f);
            Vector3 spawnPos = new Vector3(randomXt, randomYt);
            Instantiate(middleEnemy, spawnPos, Quaternion.identity);
            curTimeMiddleEnemy = middleEnemyCooldown;
            canSpawnMiddleEnemy = false;
        }
        else {
            curTimeMiddleEnemy -= Time.deltaTime;
            if (curTimeMiddleEnemy <= 0) {
                canSpawnMiddleEnemy = true;
            }
        }
    }

    void SpawnSeveralEasyEnemy() {
        if (canSpawnEasyEnemy) {
            randomX = Random.Range(-7.5f, 7.5f);
            randomY = Random.Range(5.5f, 7f);
            Vector3 spawnPos1 = new Vector3(randomX + 1f, randomY, 0f);
            Vector3 spawnPos2 = new Vector3(randomX - 1f, randomY, 0f);
            Vector3 spawnPos3 = new Vector3(randomX, randomY + 1f, 0f);
            Instantiate(easyEnemy, spawnPos1, Quaternion.identity);
            Instantiate(easyEnemy, spawnPos2, Quaternion.identity);
            Instantiate(easyEnemy, spawnPos3, Quaternion.identity);
            curTimeEasyEnemy = easyEnemyCooldown * 5;
            canSpawnEasyEnemy = false;
        }
        else {
            curTimeEasyEnemy -= Time.deltaTime;
            if (curTimeEasyEnemy <= 0) {
                canSpawnEasyEnemy = true;
            }
        }
    }

    void SpawnBoss() {
        if (canSpawnBoss) {
            float randomXboss = Random.Range(-7.5f, 7.5f);
            float randomYboss = Random.Range(5.5f, 7f);
            Vector3 spawnPosBoss = new Vector3(randomXboss, randomYboss, 0f);
            Instantiate(bossEnemy, spawnPosBoss, Quaternion.identity);
            curTimeBoss = bossSpawnCooldown;
            canSpawnBoss = false;
        }
        else {
            curTimeBoss -= Time.deltaTime;
            if (curTimeBoss <= 0) {
                Debug.Log("sadasd");
                canSpawnBoss = true;
            }
        }
    }

    
}
