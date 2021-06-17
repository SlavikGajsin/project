using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float minY;

    int damage = 30;
    int megaDamage = 50;
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
        
        Move();
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
        if ((target.tag == "Missile" || target.tag == "Laser" || target.tag == "ShieldBubble") && !collided) {
            Destroy(gameObject);
            collided = true;
            
            
        }
        if (target.tag == "Player" & !collided) {
            if (this.gameObject.tag == "EnemyLaser") {
                healthScript.TakeDamage(damage);
            }
            if (this.gameObject.tag == "EnemyMegaLaser") {
                healthScript.TakeDamage(megaDamage);
            }
            Destroy(gameObject);
            collided = true;  
        }
    }
}
