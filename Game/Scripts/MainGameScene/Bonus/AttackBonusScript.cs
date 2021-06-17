using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBonusScript : MonoBehaviour
{
    public float speed;

    
    void Move() {
        Vector3 temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;
    }

    void Update() {
        Move();
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
