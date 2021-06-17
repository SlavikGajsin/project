using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonusScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool collided;
    public int healthToGain;
    Health healthScript;
    public float speed;
    Vector3 temp;
    AudioSource audioSource;
    public AudioClip bonusSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        healthScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<Health>();
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
            healthScript.GainHealth(healthToGain);
        }

    }
}
