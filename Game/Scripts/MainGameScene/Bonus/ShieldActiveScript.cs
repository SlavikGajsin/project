using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActiveScript : MonoBehaviour
{
    GameObject player;
    Vector3 playerPosition;
    public float duration;
    float currentTime;
    bool isActive;

    void Start()
    {
        isActive = true;
        currentTime = duration;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
    }

    void FixedUpdate()
    {
        FollowPlayerPosition();
        playerPosition = player.transform.position;
        if (isActive) {
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime <= 0) {
                CancelCollisionWithShieldBonus();
                Destroy(gameObject);
                isActive = false;
            }
        }
    }

    void CancelCollisionWithShieldBonus() {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerControllerSpaceShip2>() != null) {
            PlayerControllerSpaceShip2 playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerControllerSpaceShip2>();
            playerScript.collidedWithShieldBonus = false;
        } 
        if (GameObject.FindWithTag("Player").GetComponent<PlayerController>() != null) {
            PlayerController playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerScript.collidedWithShieldBonus = false;
        }
        
        
    }

    

    void FollowPlayerPosition() {
        transform.position = playerPosition;
    }

    public void ActivateShield() {
        isActive = true;
        currentTime = duration;
        Debug.Log(currentTime);
    }


}
