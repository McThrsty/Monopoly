using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public static SetVolume setVolume;

    public AudioMixerGroup musicMixer;
    AudioSource audioSource;

    public bool SFXbool = true;
    public int SFXcounter = 0;

    public Image checkmark;

    public AudioClip click;
    //private GameManager gm;

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMasterVolume(float sliderValue)
    {
        musicMixer.audioMixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    void PlaySound()
    {
        if (SFXcounter == 1)
        {
            audioSource.PlayOneShot(click);
        }
        else if (SFXcounter == 0)
        {
            Debug.Log("SFX are turned off");
            Debug.Log(SFXbool);

        }
    }

    void Start()
    {
        //gm = GameManager.gm;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClicked()
    {
        PlaySound();
    }

    public void OnSFXClicked()
    {
        Debug.Log("Sound Effects was clicked");
        if (SFXbool != true)
        {
            
            SFXbool = true;
            SFXcounter++;
            
        }
        else if (SFXbool == true)
        {
            SFXbool = false;
            SFXcounter--;
            //setVolume.gameObject.SetActive(false);
            //checkmark.sprite = setVolume.GetComponent<checkmark>().enabled = false;
        }
        Debug.Log(SFXbool);
        
    }

    void Update()
    {
        
    }
}
