using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovmentScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float shootingCooldown;
    bool canShoot;

    int damage = 30;
    public GameObject missilePrefab;
    Vector3 movementTemp;
    public GameObject[] shootingPoints;
    public GameObject explosionPrefab;
    float timeToNextShoot;
    
    Health healthScript;
    public int scoreToGain;
    bool collided;
    CoinAndScoreGain casg;
    void Start()
    {
        casg = GameObject.FindGameObjectWithTag("GameController").GetComponent<CoinAndScoreGain>();
        collided = false;
        healthScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
    }

    
    void Update()
    {
        Move();
        ShootMissile();
        checkUnderMap();
    }

    void Move() {
        movementTemp = transform.position;
        movementTemp.y -= speed * Time.deltaTime;
        transform.position = movementTemp;
    }

    void ShootMissile() {
        if (canShoot)
        {
            for (int i = 0; i < shootingPoints.Length; i++) {
                Instantiate(missilePrefab, shootingPoints[i].transform.position, Quaternion.identity);
            }
            canShoot = false;
            timeToNextShoot = shootingCooldown;
        }
        else {
            timeToNextShoot -= Time.deltaTime;
            if (timeToNextShoot <= 0) {
                canShoot = true;
            }
        }
    }

    void checkUnderMap() {
        if (transform.position.y < -6) {
            Destroy(gameObject);
        }
    }

    void CreateExplosion() {
        Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), 0.4f);
    }

    void OnTriggerEnter2D (Collider2D target) {
        if ((target.tag == "Explosion" || target.tag == "Missile" || target.tag == "Player" || target.tag == "Laser" || target.tag == "ShieldBubble") && !collided) {
            CreateExplosion();
            collided = true;
            Destroy(gameObject);
            casg.GainScore(scoreToGain);
            if (target.tag == "Player") {
                healthScript.TakeDamage(damage);
            }
        }
    }
}
