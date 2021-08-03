using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cOptions : MonoBehaviour
{
    private cMainMenu c_MainMenu;

    public AudioClip click;
    private GameManager gm;
    AudioSource audioSource;

    private void Start()
    {
        gm = GameManager.gm;
        audioSource = GetComponent<AudioSource>();

    }

    public void OnBackButtonClicked()
    {
        audioSource.PlayOneShot(click);
        Destroy(gameObject);
    }
}
