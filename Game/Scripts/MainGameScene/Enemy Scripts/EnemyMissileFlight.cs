using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileFlight : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public float maxSpeed = 15f;
    public float minY;

    int damage = 20;
    public GameObject missleTrail;
    Vector3 temp;
    
    Health healthScript;

    bool collided;
    
    void Start()
    {
        
        collided = false;
        healthScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
    }

    void Update()
    {
        AccelerateMissle();
        Move();
    }

    void AccelerateMissle()
    {
        if (speed < maxSpeed)
        {
            speed += 0.03f;
        }
    }

    void Move()
    {
        temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        if (temp.y < minY){
            Destroy(gameObject);
        }
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if ((target.tag == "Player" || target.tag == "Missile" || target.tag == "Laser" || target.tag == "ShieldBubble") && !collided) {
            Destroy(gameObject);
            collided = true;
            if (target.tag == "Player") {
                healthScript.TakeDamage(damage);
            }
            
        }
    }

}
