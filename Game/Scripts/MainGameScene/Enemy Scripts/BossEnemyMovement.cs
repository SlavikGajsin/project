using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement : MonoBehaviour
{
    int health = 300;
    float laserCooldown = 3f;
    float megaLaserCooldown = 15f;
    float currentTime, currentTimeMega;
    public float speed;
    public GameObject[] shootingPoints;
    public GameObject megaLaserShootingPoint;
    public float leftX, rightX;
    Vector3 temp;
    public bool moveRight, moveVertical, reachedStartPoint, canShoot, canShootMegaLaser;
    public GameObject explosionPrefab, enemyLaserPrefab, enemyMegaLaserPrefab;
    Vector3 leftPos, rightPos;
    CoinAndScoreGain coinAndScoreGainScript;
    
    void Start() {
        coinAndScoreGainScript = GameObject.FindWithTag("GameController").GetComponent<CoinAndScoreGain>();
        leftPos = new Vector3(-7.5f, 3.7f, 0f);
        rightPos = new Vector3(7.5f, 3.7f, 0f);
        moveVertical = true;
        reachedStartPoint = false;
        canShootMegaLaser = false;
        canShoot = false;
    }

    void Update() {
        MoveVertical();
        MoveHorizontal();
        ShootMegaLaser();
        ShootLaser();
    }

    void MoveVertical() {
        if (transform.position.y > 3.7f && moveVertical) {
            temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;
        }
        else if (!reachedStartPoint) {
            reachedStartPoint = true;
            moveVertical = false;
            moveRight = true;
            
        }
    }

    void MoveHorizontal() {
        if (reachedStartPoint) {
            if (moveRight) {
                transform.position = Vector2.MoveTowards(transform.position, rightPos, speed / 2 * Time.deltaTime);
                if (transform.position == rightPos) {
                    moveRight = false;
                }
            }
            else {
                transform.position = Vector2.MoveTowards(transform.position, leftPos, speed / 2 * Time.deltaTime);
                if (transform.position == leftPos) {
                    moveRight = true;
                }
            }
        }
    }

    void ShootLaser() {
        if (canShoot) {
            for (int i = 0; i < shootingPoints.Length; i++) {
                Instantiate(enemyLaserPrefab, shootingPoints[i].transform.position, Quaternion.identity);
            }
            currentTime = laserCooldown;
            canShoot = !canShoot;
        }
        else {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) {
                canShoot = true;
            }
        }
    }

    void ShootMegaLaser() {
        if (canShootMegaLaser) {
            Instantiate(enemyMegaLaserPrefab, megaLaserShootingPoint.transform.position, Quaternion.identity);
            currentTimeMega = megaLaserCooldown;
            canShootMegaLaser = false;
        }
        else {
            currentTimeMega -= Time.deltaTime;
            if (currentTimeMega <= 0) {
                canShootMegaLaser = true;
            }
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 0.5f);
            Destroy(gameObject);
            coinAndScoreGainScript.GainScore(50);
        }
    }

    
}
