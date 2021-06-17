using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonusScript : MonoBehaviour
{
    bool collided;
    public float speed;
    Vector3 temp;
    GameObject player;
    AudioSource audioSource;
    public AudioClip bonusSound;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        temp = transform.position;
        temp.y -= speed * Time.deltaTime;
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Player" && !collided) {
            collided = true;
            audioSource.PlayOneShot(bonusSound);
            Destroy(gameObject);
            
        }

    }

}
