using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    AudioManager audioManager;
    Slider slider;
    float musicVol;
    float sfxVol;

    public void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>();
        if(slider == null)
        {
            Debug.LogError("Slider not assigned");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.Log("Audio Manager not assigned");
        }
        else
        {
            if (this.gameObject.name == "BackgroundSlider")
            {
                slider.value = audioManager.GetMusicVolume();

            }
            else if (this.gameObject.name == "SFXSlider")
            {
                slider.value = audioManager.GetSFXVolume();
                Debug.Log("SFX Slider vold Assigned");
            }
            else
            {
                Debug.Log(gameObject.name + " doesn't have proper slider name on it");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
