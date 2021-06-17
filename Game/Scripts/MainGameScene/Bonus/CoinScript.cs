using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 movementTemp;
    public float speed;
    CoinAndScoreGain casg;
    AudioSource audioSource;
    public AudioClip coinAudio;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        casg = GameObject.Find("GameController").GetComponent<CoinAndScoreGain>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        checkFallingUnderScreen();
    }

    void PlayGainSound() {
        audioSource.PlayOneShot(coinAudio);
    }

    void Move() {
            movementTemp = transform.position;
            movementTemp.y -= speed * Time.deltaTime;
            transform.position = movementTemp;
    }

    void OnTriggerEnter2D (Collider2D target) {
        if (target.tag == "Player") {
            Destroy(gameObject);
            PlayGainSound();
            casg.GainCoins(1);
        }
    }

    void checkFallingUnderScreen() {
        if (transform.position.y < -6f) {
            Destroy(gameObject);
        }
    }

}
