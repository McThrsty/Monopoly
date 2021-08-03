using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class wPropertyInventory : MonoBehaviour
{
    private GameManager gm;
    //private sPlayer s_Player;
    public wCenter w_Center;
    private sPlayer curPlayer;
    public cInGame c_InGame;
    public soSpot spot;
    //private wMessage w_Message;

    public GameObject rightArrow;
    public GameObject leftArrow;

    public Image iArt;
    public Text tActionButton;
    public Text tPropName;



    ePos focusedSpot;
    

    int listIdx = 0;

    public AudioClip click;
    AudioSource audioSource;
    void PlayClickSound()
    {
        audioSource.PlayOneShot(click, 0.7f);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void InitUI()
    {
        gm = GameManager.gm;
        curPlayer = gm.players[gm.curPlayer];

        focusedSpot = curPlayer.listPropertiesOwned[0].position;
        spot = gm.monoSpots[(int)focusedSpot];

        tPropName.text = spot.nameSpot;
        //c_InGame = _c_InGame;

        //iArt.sprite = spot.FrontArt;
        for (int i = 0; i < curPlayer.listPropertiesOwned.Count; i++)
        {
            iArt.sprite = spot.FrontArt;
        }

        DisplayCurSpot();
    }

    void CheckPlayerProperty()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];

        bool checkAgain = false;
        
        for (int i = 0; i < curPlayer.listPropertiesOwned.Count; i++)
        {
            // Check to make sure the piece isn't used
            // Check all of the players except the index that we are
            /*if (curPlayer.listPropertiesOwned.Count)
            {
                // Increment the piece
                i++;
                // Check for last piece
                if (s_Player.piece == ePiece.terminator)
                {
                    s_Player.piece = 0;
                }
                checkAgain = true;
                break;
            }*/
        }
        if (checkAgain)
        {
            CheckPlayerProperty();
        }
    }

    public void MortgageProp()
    {
        int mortgagevalue = (int)((float)spot.propertyCost * .5f);
        curPlayer.UpdateCash(curPlayer.cashOnHand + mortgagevalue);
        curPlayer.SetMortgageProperty(spot.spotDesignation, true);
        Debug.Log("Mortgage Prop");
        
        DisplayCurSpot();
    }

    public void UnMortgageProp()
    {
        int mortgageCost = (int)((float)spot.propertyCost * .55f);
        curPlayer.UpdateCash(curPlayer.cashOnHand - mortgageCost);
        curPlayer.SetMortgageProperty(spot.spotDesignation, false);
        Debug.Log("UnMortgage Hit");
        
        DisplayCurSpot();
    }

    public void CancelButtonPress()
    {
        Destroy(this.gameObject);
    }

    public void DisplayCurSpot()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        focusedSpot = curPlayer.listPropertiesOwned[listIdx].position;
        spot = gm.monoSpots[(int)focusedSpot];

        /*if (curPlayer.listPropertiesOwned.Count > 1)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
        }*/
        
        if (curPlayer.GetIndexOfProperty(focusedSpot) == 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(curPlayer.listPropertiesOwned.Count > 1);
        }

        else if (curPlayer.GetIndexOfProperty(focusedSpot) == curPlayer.listPropertiesOwned.Count - 1)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(curPlayer.listPropertiesOwned.Count > 1);
        }
        else
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
        }

        if (!curPlayer.IsPropertyMortgaged(focusedSpot))
        {
            tActionButton.text = "Mortgage";
            iArt.sprite = spot.FrontArt;
        }
        if (curPlayer.IsPropertyMortgaged(focusedSpot))
        {
            tActionButton.text = "Unmortgage";
            iArt.sprite = spot.backArt;
        }
    }

    public void OnActionButtonHit()
    {
        /*if (!curPlayer.IsPropertyMortgaged(focusedSpot))
        {
            int mortgageValue = (int)((float)spot.propertyCost * .5f);
            string message = "Are you sure you want to mortgage " + spot.nameSpot + " for $" + mortgageValue + "?";
            wMessage scr = Instantiate(c_InGame.w_Message, c_InGame.gameObject.transform).GetComponent<wMessage>();
            scr.InitUI("Mortgage Property", message, "MortgageProp", this.gameObject);
        }
        else
        {
            int mortgageCost = (int)((float)spot.propertyCost * .55f);
            if (curPlayer.cashOnHand >= mortgageCost)
            {
                string message = "Are you sure you want to unmortgage " + spot.nameSpot + " for $" + mortgageCost + "?";
                wMessage scr = Instantiate(c_InGame.w_Message, c_InGame.gameObject.transform).GetComponent<wMessage>();
                scr.InitUI("Unmortgage Property", message, "UnMortgageProp", this.gameObject);
            }
            
        }*/
        Debug.Log(curPlayer.listPropertiesOwned[listIdx].isMortgaged);
        if (curPlayer.listPropertiesOwned[listIdx].isMortgaged == true)
        {
            int unmortgageCost = (int)(0.55f * spot.propertyCost);
            if (curPlayer.cashOnHand >= unmortgageCost)
            {
                /*string message = "Are you sure you want to unmortgage " + spot.nameSpot + " for $" + unmortgageCost + "?";
                wMessage scr = Instantiate(c_InGame.w_Message, c_InGame.gameObject.transform).GetComponent<wMessage>();
                scr.InitUI("Unmortgage Property", message, "UnMortgageProp", this.gameObject);*/
                UnMortgageProp();
            }
            else
            {
                Debug.Log("Not enough Cash");
            }
        }
        else if (curPlayer.listPropertiesOwned[listIdx].isMortgaged == false)
        {
            int mortgageValue = (int)(0.5f * spot.propertyCost);
            /*string message = "Are you sure you want to mortgage " + spot.nameSpot + " for $" + mortgageValue + "?";
            wMessage scr = Instantiate(c_InGame.w_Message, c_InGame.gameObject.transform).GetComponent<wMessage>();
            scr.InitUI("Mortgage Property", message, "MortgageProp", this.gameObject);*/
            MortgageProp();

        }
        PlayClickSound();
        DisplayCurSpot();
    }

    public void OnLeftArrowHit()
    {
        //Add Sound
        listIdx--;
        PlayClickSound();
        DisplayCurSpot();
    }

    public void OnRightArrowHit()
    {
        //Add Sound
        listIdx++;
        PlayClickSound();
        DisplayCurSpot();
    }
}
