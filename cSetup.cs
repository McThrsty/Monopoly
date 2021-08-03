using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSetup : MonoBehaviour
{
    public GameObject w_PlayerInfo; // Reference to the prefab called wPlayerInfo
    public Transform panelWidget;

    public Button bStartGame;
    
    public void InitUI()
    {
        for (int i = 0; i < GameManager.maxNumPlayers; i++)
        {
            wPlayerInfo scr = Instantiate(w_PlayerInfo, panelWidget).GetComponent<wPlayerInfo>();
            scr.InitUI(i, this);
        }
    }

    public void DisplayStartGame (bool _active)
    {
        bStartGame.gameObject.SetActive(_active);
    }

    public void OnStartGameClicked()
    {
        Debug.Log("On Start Game Clicked");
        GameManager.gm.LoadScene(1);
    }

    public void OnBackButtonClicked()
    {
        Destroy(gameObject);
    }
}
