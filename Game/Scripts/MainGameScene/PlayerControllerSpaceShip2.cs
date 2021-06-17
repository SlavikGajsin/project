using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSpaceShip2 : MonoBehaviour
{
    public float speed;
    public float minX, maxX;
    public bool canShootMissile, canShootLaser;
    
    public GameObject missileObject, laserObject;
    public GameObject[] shootingPointLaser;
    public GameObject[] shootingPointMissile;

    Vector3[] shootingPositionMissile = new Vector3[2];
    Vector3[] shootingPositionLaser = new Vector3[2];
    public float cooldownTimeMissile;
    public float cooldownTimeLaser;
    private float timeToNextMissile, timeToNextLaser;

    GameObject shieldBubble;
    public bool collidedWithShieldBonus, collidedWithAttackBonus;
    float currentTimeShield, currentTimeAttackBonus;
    float shieldDuration = 5f;
    float attackBonusDuration = 7f;

    PlayerData playerData = new PlayerData();

    KeyCode laserKeyCode, missileKeyCode;

    void Start() {
        laserKeyCode = (KeyCode)System.Enum.Parse(typeof (KeyCode), PlayerPrefs.GetString("LaserKey", "K"));
        missileKeyCode = (KeyCode)System.Enum.Parse(typeof (KeyCode), PlayerPrefs.GetString("MissileKey", "Space"));
        canShootMissile = true;
        Time.timeScale = 1;
        collidedWithShieldBonus = false;
        collidedWithAttackBonus = false;
        SetUpgradedParams();
        shieldBubble = GameObject.FindWithTag("ShieldBubble");
        shieldBubble.SetActive(false);
        
    }

    void SetUpgradedParams() {
        playerData = SaveLoadSystem.LoadPlayer();
        speed += (float)(0.15 * playerData.secondShipLevel);
    }

    void Update(){
        MovePlayer();
        ShootMissile();
        ShootLaser();
    }

    void FixedUpdate() {
        CheckShieldDeactivation();
        CheckAttackBonusDeactivation();
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
    
    void ShootMissile()
    {
        if (canShootMissile)
        {
            if (Input.GetKeyDown(missileKeyCode)) 
            {
                GetShootingPositionMissile();
                for (int i = 0; i < shootingPointMissile.Length; i++) {
                    Instantiate(missileObject, shootingPositionMissile[i], Quaternion.identity);
                    if (collidedWithAttackBonus) {
                        Instantiate(missileObject, new Vector3(shootingPositionMissile[i].x + 0.15f, shootingPositionMissile[i].y, shootingPositionMissile[i].z),Quaternion.identity);
                        Instantiate(missileObject, new Vector3(shootingPositionMissile[i].x - 0.15f, shootingPositionMissile[i].y, shootingPositionMissile[i].z),Quaternion.identity);
                    }
                }
                
                canShootMissile = false;
                timeToNextMissile = cooldownTimeMissile;
            }
        }
        else {
            timeToNextMissile -= Time.deltaTime;
            if (timeToNextMissile <= 0) {
                canShootMissile = true;
            }
        }
        
    }

    void GetShootingPositionMissile() {
        for (int i = 0; i < shootingPointMissile.Length; i++) {
            shootingPositionMissile[i] = shootingPointMissile[i].transform.position;
        }
    }

    void ShootLaser() {
        if (canShootLaser)
        {
            if (Input.GetKeyDown(laserKeyCode)) 
            {
                GetShootingPositionLaser();
                
                Instantiate(laserObject, shootingPositionLaser[0], Quaternion.identity);
                
                Instantiate(laserObject, shootingPositionLaser[1], Quaternion.identity);
                if (collidedWithShieldBonus) {
                    Instantiate(laserObject, new Vector3(shootingPositionLaser[0].x + 0.15f, shootingPositionLaser[0].y, 0f), Quaternion.identity);
                    Instantiate(laserObject, new Vector3(shootingPositionLaser[0].x - 0.15f, shootingPositionLaser[0].y, 0f), Quaternion.identity);
                    Instantiate(laserObject, new Vector3(shootingPositionLaser[1].x - 0.15f, shootingPositionLaser[1].y, 0f), Quaternion.identity);
                    Instantiate(laserObject, new Vector3(shootingPositionLaser[1].x + 0.15f, shootingPositionLaser[1].y, 0f), Quaternion.identity);
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

    void GetShootingPositionLaser() {
        for (int i = 0; i < shootingPointLaser.Length; i++) {
            shootingPositionLaser[i] = shootingPointLaser[i].transform.position;
        }
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

    void CreateShieldBubble() {
        //Instantiate(shieldBubble, transform.position, Quaternion.identity);
        shieldBubble.SetActive(true);
        Debug.Log("Created shield");
    }

    void CheckAttackBonusDeactivation() {
        if (collidedWithAttackBonus) {
            currentTimeAttackBonus -= Time.deltaTime;
            if (currentTimeAttackBonus <= 0) {
                collidedWithAttackBonus = false;
            }
        }
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
