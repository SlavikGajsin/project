using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroidPrefab;
    public int maxAmount;
    
    float gameTimer;
    bool increased1, increased2, increased3;

    void Start()
    {
        gameTimer = 0f;
        increased1 = false;
        increased2 = false;
        increased3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        SpawnAsteroids();
        IncreaseAsteroidsAmount();
    }

    void IncreaseAsteroidsAmount() {
        gameTimer += Time.deltaTime;
        if (gameTimer >= 30f && !increased1) {
            increased1 = true;
            maxAmount += 2;
        }
        if (gameTimer >= 60f && !increased2) {
            increased2 = true;
            maxAmount += 3;
        }
        if (gameTimer >= 90f && !increased3) {
            increased3 = true;
            maxAmount += 4;
        }
    }

    void SpawnAsteroids() {
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length < maxAmount) {
            float randX = Random.Range(-8, 8);
            float randY = Random.Range(8, 9);
            Vector3 coordinates = new Vector3(randX, randY, 0);
            Instantiate(asteroidPrefab, coordinates, Quaternion.identity);
        }
    }

}
