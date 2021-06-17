using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    

    public float speed;
    public float minX, maxX;
    public bool canShootMissle, canShootLaser;
    public GameObject missleObject;
    public GameObject shootingPoint;

    public Vector3 shootingPosition;
    public float cooldownTime;
    private float timeToNextMissle;

    public GameObject laserObject;
    float timeToNextLaser;
    public float cooldownTimeLaser;
    
    PlayerData playerData = new PlayerData();

    GameObject shieldBubble;
    public bool collidedWithShieldBonus, collidedWithAttackBonus;
    float currentTimeShield, currentTimeAttackBonus;
    float shieldDuration = 5f;
    float attackBonusDuration = 7f;
    KeyCode laserKeyCode, missileKeyCode;

    void Start() {
        currentTimeShield = 0f;
        laserKeyCode = (KeyCode)System.Enum.Parse(typeof (KeyCode), PlayerPrefs.GetString("LaserKey", "K"));
        missileKeyCode = (KeyCode)System.Enum.Parse(typeof (KeyCode), PlayerPrefs.GetString("MissileKey", "Space"));
        canShootMissle = true;
        shieldBubble = GameObject.FindWithTag("ShieldBubble");
        shieldBubble.SetActive(false);
        collidedWithShieldBonus = false;
        collidedWithAttackBonus = false;
        canShootLaser = true;
        SetUpgradedParams();
    }

    void Update(){
        MovePlayer();
        ShootMissle();
        ShootLaser();

    }

    void FixedUpdate() {
        CheckShieldDeactivation();
        CheckAttackBonusDeactivation();
    }

    void SetUpgradedParams() {
        playerData = SaveLoadSystem.LoadPlayer();
        speed += (float)(0.1 * playerData.firstShipLevel);
    }
    
    void MovePlayer() {
        if (Input.GetAxisRaw("Horizontal") > 0f) {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            if (temp.x > maxX) {
                temp.x = maxX;
            }
            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f) {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            if (temp.x < minX) {
                temp.x = minX;
            }
            transform.position = temp;
        }
    }
    
    void ShootMissle()
    {
        if (canShootMissle)
        {
            if (Input.GetKeyDown(missileKeyCode)) 
            {
                GetShootingPosition();
                Instantiate(missleObject, shootingPosition, Quaternion.identity);
                if (collidedWithAttackBonus) {
                    Instantiate(missleObject, new Vector3(shootingPosition.x + 0.15f, shootingPosition.y, 0f), Quaternion.identity);
                    Instantiate(missleObject, new Vector3(shootingPosition.x - 0.15f, shootingPosition.y, 0f), Quaternion.identity);
                }
                canShootMissle = false;
                timeToNextMissle = cooldownTime;
            }
        }
        else {
            timeToNextMissle -= Time.deltaTime;
            if (timeToNextMissle <= 0) {
                canShootMissle = true;
            }
        }
        
    }

    void ShootLaser() {
        if (canShootLaser)
        {
            if (Input.GetKeyDown(laserKeyCode)) 
            {
                GetShootingPosition();
                Instantiate(laserObject, new Vector3(shootingPosition.x, shootingPosition.y, 0f), Quaternion.identity);
                if (collidedWithAttackBonus) {
                    Instantiate(laserObject, new Vector3(shootingPosition.x + 0.15f, shootingPosition.y, 0f), Quaternion.identity);
                    Instantiate(laserObject, new Vector3(shootingPosition.x - 0.15f, shootingPosition.y, 0f), Quaternion.identity);
                }
                
                canShootLaser = false;
                timeToNextLaser = cooldownTimeLaser;
            }
        }
        else {
            timeToNextLaser -= Time.deltaTime;
            if (timeToNextLaser <= 0) {
                canShootLaser = true;
            }
        }
    }


    void GetShootingPosition()
    {
        shootingPosition = shootingPoint.transform.position;
    }

    void CheckShieldDeactivation() {
        if (collidedWithShieldBonus) {
            currentTimeShield -= Time.deltaTime;
            //Debug.Log(currentTime);
            if (currentTimeShield <= 0) {
                
                shieldBubble.SetActive(false);
                collidedWithShieldBonus = false;
            }
        }
    }

    void CheckAttackBonusDeactivation() {
        if (collidedWithAttackBonus) {
            currentTimeAttackBonus -= Time.deltaTime;
            if (currentTimeAttackBonus <= 0) {
                collidedWithAttackBonus = false;
            }
        }
    }

    void CreateShieldBubble() {
        //Instantiate(shieldBubble, transform.position, Quaternion.identity);
        shieldBubble.SetActive(true);
        Debug.Log("Created shield");
    }


    void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "ShieldBonus" && !collidedWithShieldBonus) {
            collidedWithShieldBonus = true;
            CreateShieldBubble();
            currentTimeShield = shieldDuration;
        }
        if (target.tag == "AttackBonus" && !collidedWithAttackBonus) {
            collidedWithAttackBonus = true;
            currentTimeAttackBonus = attackBonusDuration;
        }
    }
}
