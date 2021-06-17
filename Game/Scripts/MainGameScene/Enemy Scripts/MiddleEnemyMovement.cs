using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleEnemyMovement : MonoBehaviour
{
    int health = 100;
    float speed = 0.5f;
    float laserCooldown = 3f;
    float missileCooldown = 5f;
    float currentTimeLaser, currentTimeMissile;
    bool canShootLaser, canShootMissile;
    public GameObject laserPrefab, missilePrefab, explosionPrefab;
    public GameObject[] shootingPointsLaser, shootingPointsMissile;
    CoinAndScoreGain coinAndScoreGainScript;
    Vector3 temp;

    void Start()
    {
        coinAndScoreGainScript = GameObject.FindWithTag("GameController").GetComponent<CoinAndScoreGain>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ShootLaser();
        ShootMissile();
    }

    void ShootLaser() {
        if (canShootLaser) {
            for (int i = 0; i < shootingPointsLaser.Length; i++) {
                Instantiate(laserPrefab, shootingPointsLaser[i].transform.position, Quaternion.identity);
            }
            currentTimeLaser = laserCooldown;
            canShootLaser = false;
        }
        else {
            currentTimeLaser -= Time.deltaTime;
            if (currentTimeLaser <= 0) {
                canShootLaser = true;
            }
        }
    }

    void ShootMissile() {
        if (canShootMissile) {
            for (int i = 0; i < shootingPointsMissile.Length; i++) {
                Instantiate(missilePrefab, shootingPointsMissile[i].transform.position, Quaternion.identity);
            }
            currentTimeMissile = missileCooldown;
            canShootMissile = false;
        }
        else {
            currentTimeMissile -= Time.deltaTime;
            if (currentTimeMissile <= 0) {
                canShootMissile = true;
            }
        }
    }

    void Move() {
        temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 0.5f);
            Destroy(gameObject);
            coinAndScoreGainScript.GainScore(50);
        }
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player") {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 0.5f);
            Destroy(gameObject);
        }
    }
}
