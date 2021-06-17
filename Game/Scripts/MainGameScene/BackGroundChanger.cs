using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    public GameObject bg1, bg2;
    public Sprite[] spriteArray;
    SpriteRenderer bg1_sr, bg2_sr;
    float alpha1, alpha2;
    float duration = 30f;
    bool setSecond;
    int iterationNumber;

    void Start()
    {
        iterationNumber = 0;
        bg1_sr = bg1.GetComponent<SpriteRenderer>();
        bg1_sr.sprite = spriteArray[iterationNumber];
        bg2_sr = bg2.GetComponent<SpriteRenderer>();
        bg2_sr.sprite = spriteArray[iterationNumber + 1];
        alpha1 = 1f;
        setSecond = true;
        bg2_sr.color = new Color(bg2_sr.color.r, bg2_sr.color.g, bg2_sr.color.b, 0f);
    }

    
    void FixedUpdate()
    {
        ChangeBackground();
    }

    void ChangeBackground() {
        if (setSecond) {
            alpha1 -= Time.deltaTime / duration;
            alpha2 += Time.deltaTime / duration;
            //проверка первого
            if (alpha1 > 0f) {
                bg1_sr.color = new Color(bg1_sr.color.r, bg1_sr.color.g, bg1_sr.color.b, alpha1);
            }
            else {
                bg1_sr.color = new Color(bg1_sr.color.r, bg1_sr.color.g, bg1_sr.color.b, 0f);
            }
            //проверка второго
            if (alpha2 < 1f) {
                bg2_sr.color = new Color(bg2_sr.color.r, bg2_sr.color.g, bg2_sr.color.b, alpha2);
            }
            else {
                bg2_sr.color = new Color(bg2_sr.color.r, bg2_sr.color.g, bg2_sr.color.b, 1f);
            }
            //проверка конца смены 
            if (bg1_sr.color.a == 0f && bg2_sr.color.a == 1f) {
                setSecond = !setSecond;
                iterationNumber++;
                if (iterationNumber == spriteArray.Length) {
                    iterationNumber = 0;
                }
                bg1_sr.sprite = spriteArray[iterationNumber];
            }
            
        }
        if (!setSecond) {
            alpha1 += Time.deltaTime / duration;
            alpha2 -= Time.deltaTime / duration;
            //проверка первого
            if (alpha1 < 1f) {
                bg1_sr.color = new Color(bg1_sr.color.r, bg1_sr.color.g, bg1_sr.color.b, alpha1);
            }
            else {
                bg1_sr.color = new Color(bg1_sr.color.r, bg1_sr.color.g, bg1_sr.color.b, 1f);
            }
            //проверка второго
            if (alpha2 > 0f) {
                bg2_sr.color = new Color(bg2_sr.color.r, bg2_sr.color.g, bg2_sr.color.b, alpha2);
            }
            else {
                bg2_sr.color = new Color(bg2_sr.color.r, bg2_sr.color.g, bg2_sr.color.b, 0f);
            }
            //проверка конца смены 
            if (bg1_sr.color.a == 1f && bg2_sr.color.a == 0f) {
                setSecond = !setSecond;
                iterationNumber++;
                if (iterationNumber == spriteArray.Length) {
                    iterationNumber = 0;
                }
                bg2_sr.sprite = spriteArray[iterationNumber];
            }
        }
    }
}
