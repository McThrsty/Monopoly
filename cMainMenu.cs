using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class cMainMenu : MonoBehaviour
{
    private cOptions c_Options;

    AudioSource main_MyAudioSource;

    private GameManager gm;
    public AudioMixerGroup musicMixer;


    private void Start()
    {
        gm = GameManager.gm;
        main_MyAudioSource = GetComponent<AudioSource>();

    }

    public void OnStartGameClicked()
    {
        Debug.Log("On Start Game Clicked");
        musicMixer.audioMixer.SetFloat("MainMenuVol", 0f);
        GameObject obj = Instantiate(gm.pSetupScreen);
        cSetup scr = obj.GetComponent<cSetup>();
        scr.InitUI();
        main_MyAudioSource.Stop();

    }

    public void PlayMusic()
    {
        //if (c_Options.OnBackButtonClicked())
        //{
            //main_MyAudioSource.Play();
        //}
    }

    public void OnOptionsClicked()
    {
        Debug.Log("On Options Clicked");
        GameObject obj = Instantiate(gm.pOptionsScreen);
        cOptions scr = obj.GetComponent<cOptions>();
        main_MyAudioSource.Stop();
    }

    void Update()
    {
        
    }
}
