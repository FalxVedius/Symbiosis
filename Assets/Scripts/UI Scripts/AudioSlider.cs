using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    //private variables
    AudioManager audioManager;
    Slider slider;

    public void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>(); //Assigns slider variable to gameobject's slider component
        if(slider == null)
        {
            Debug.LogError("Slider not assigned");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Assigns audioManager variable to AudioManager
        audioManager = AudioManager.instance;


        if (audioManager == null) //Checks if the Audio Manager was properly assigned
        {
            Debug.Log("Audio Manager not assigned");
        }
        else
        {
            //If Audio Manager is properly assigned, check what type of volume the slider is adjusting and assign its value accordingly
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

    public void SetMasterVolume(float masterVol)
    {
        //Sets Master Volume Level
        audioManager.SetMasterVolume(masterVol);
    }

    public void SetMusicVolume(float musicVol)
    {
        //Sets Music Volume Level
        audioManager.SetMusicVolume(musicVol);
    }

    public void SetSFXVolume(float sfxVol)
    {
        //Sets SFX Volume Level
        audioManager.SetSFXVolume(sfxVol);
    }
}
