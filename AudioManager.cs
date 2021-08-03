using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    GameManager gm = GameManager.gm;
    public static AudioManager am;
    public AudioClip click;
    public AudioClip noGo;
    public AudioClip landOnSpot;
    public eAudio playSounds;
    AudioSource audioSource;

    public void PlaySound()
    {
        gm = GameManager.gm;
        switch (playSounds)
        {

            case eAudio.click:
                audioSource.PlayOneShot(click);
                break;
            case eAudio.noGo:
                audioSource.PlayOneShot(noGo);
                break;
            case eAudio.landOnSpot:
                audioSource.PlayOneShot(landOnSpot);
                break;

        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
