using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed, rotationSpeed;
    bool destroyed;
    int damage = 20;
    Vector3 temp;
    int randomRotation;
    public Sprite[] asteroidSprites;
    public GameObject explosionPrefab;
    SpriteRenderer spriteRenderer;
    public int scoreToGain;
    CoinAndScoreGain casg;

    Health healthScript;

    bool collided;

    void Start()
    {
        collided = false;
        healthScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
        casg = GameObject.Find("GameController").GetComponent<CoinAndScoreGain>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randomSpriteIndex = Random.Range(0, 3);
        spriteRenderer.sprite = asteroidSprites[randomSpriteIndex];
        randomRotation = Random.Range(-1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
        checkFallingUnderScreen();
        Rotate();
    }

    void Fall()
    {
        
        temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;
        
    }

    void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void checkFallingUnderScreen() {
        if (transform.position.y < -6f) {
            Destroy(gameObject);
        }
    }

    void CreateExplosion() {
        Destroy(Instantiate(explosionPrefab, transform.position, transform.rotation), 0.4f);
    }

    

    void OnTriggerEnter2D(Collider2D target) {
        if ((target.tag == "Missile" || target.tag == "Laser" || target.CompareTag("Player") || target.tag == "ShieldBubble") && !collided) {
            collided = true;
            CreateExplosion();
            Destroy(gameObject);
            
            casg.GainScore(scoreToGain);
            if (target.tag == "Player") {
                healthScript.TakeDamage(damage);
            }
        }
        
    } 
    
    /*
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Missile" || collision.gameObject.tag == "Laser" || collision.gameObject.tag == "Player") {
            
            CreateExplosion();
            Destroy(gameObject);
            
            casg.GainScore(scoreToGain);
            if (target.tag == "Player") {
                healthScript.TakeDamage(damage);
            }
        }
    } */
}
