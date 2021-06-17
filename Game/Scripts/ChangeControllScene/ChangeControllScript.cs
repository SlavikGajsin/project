using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeControllScript : MonoBehaviour
{
    bool pressedMissileButton, pressedLaserButton, foundKey;
    public Text missileText, laserText;

    public GameObject pressAnyButtonScreen;

    AudioSource audioSource;
    public AudioClip clickAudio;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        pressAnyButtonScreen.SetActive(false);
        pressedMissileButton = false;
        pressedLaserButton = false;
        foundKey = false;
        missileText.text = PlayerPrefs.GetString("MissileKey", "Space");
        laserText.text = PlayerPrefs.GetString("LaserKey", "K");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (pressedMissileButton) {
            Event e = Event.current;
            
            if (e.isKey && !foundKey) {
                pressedMissileButton = false;
                foundKey = true; 
                PlayerPrefs.SetString("MissileKey", e.keyCode.ToString());
                missileText.text = e.keyCode.ToString();
                Debug.Log(e.keyCode.ToString());
                pressAnyButtonScreen.SetActive(false);
            }
            
        }

        if (pressedLaserButton) {
            Event e = Event.current;
            if (e.isKey && !foundKey) {
                pressedMissileButton = false;
                foundKey = true;
                PlayerPrefs.SetString("LaserKey", e.keyCode.ToString());
                laserText.text = e.keyCode.ToString();
                Debug.Log(e.keyCode.ToString()); 
                pressAnyButtonScreen.SetActive(false);
            }
        }
        
    }

    public void OnPressMissileButton() {
        audioSource.PlayOneShot(clickAudio);
        pressedMissileButton = true;
        foundKey = false;
        pressAnyButtonScreen.SetActive(true);
    }

    public void OnPressLaserButton() {
        audioSource.PlayOneShot(clickAudio);
        pressedLaserButton = true;
        foundKey = false;
        pressAnyButtonScreen.SetActive(true);
    }

    public void QuitToMenu() {
        audioSource.PlayOneShot(clickAudio);
        SceneManager.LoadScene("MainMenu");
    }



}
