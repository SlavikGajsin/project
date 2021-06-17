using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSourceController : MonoBehaviour
{
    public int currentVolumeEq;
    AudioSource audioSource;
    public AudioClip clickAudio;

    public GameObject musicButton;
    Image musicButtonImage;
    public Sprite soundOnIcon, soundOffIcon;

    void Start()
    {
        musicButtonImage = musicButton.GetComponent<Image>();
        audioSource = gameObject.GetComponent<AudioSource>();
        currentVolumeEq = PlayerPrefs.GetInt("VolumeEq", 1);
        CheckVolumeChanges();
    }

    void Update() {
        CheckVolumeChanges();
    }

    void CheckVolumeChanges() {
        if (currentVolumeEq == 1) {
            AudioListener.pause = false;
            musicButtonImage.sprite = soundOnIcon;
        }
        else if (currentVolumeEq == 0) {
            AudioListener.pause = true;
            musicButtonImage.sprite = soundOffIcon;
        }
    }

    public void ChangeVolume() {
        
        if (currentVolumeEq == 0) {
            audioSource.PlayOneShot(clickAudio);
            PlayerPrefs.SetInt("VolumeEq", 1);
            currentVolumeEq = 1;
            AudioListener.pause = false;
            audioSource.PlayOneShot(clickAudio);
            musicButtonImage.sprite = soundOnIcon;
        }
        else if (currentVolumeEq == 1) {
            PlayerPrefs.SetInt("VolumeEq", 0);
            currentVolumeEq = 0;
            AudioListener.pause = true;
            audioSource.PlayOneShot(clickAudio);
            musicButtonImage.sprite = soundOffIcon;
        }
    }
}
