using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class wCenter : MonoBehaviour
{
    public sPlayer s_Player;
    private GameManager gm;
    public cInGame c_InGame;
    public cardChance card_Chance;
    public cardCommChest card_CommChest;

    public Text tTitle;
    public Text tMessage;
    public Image iArt;
    public Image iCard;

    public GameObject button1;
    public GameObject button2;

    public Text tActionButton; //The text that sits on the BUY button
    public Text tActionButton2; //This is the left button (usually PASS
    ePos focusedSpot;
    eChanceCards chanceFocused;
    eCommChestCards commChestFocused;

    soSpot spot;

    int randCardNum;

    public AudioClip click;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void PlayClickSound()
    {
        audioSource.PlayOneShot(click, 0.7f);
    }

    

    public void InitUI (string _title, soSpot _soSpot, string _actionButton, cardChance _cardChance, cardCommChest _cardCommChest)
    {
        gm = GameManager.gm; //Set up an alias to GameManager (gm)
        sPlayer curPlayer = gm.players[gm.curPlayer]; //Set up an alias to current player (player)


        tTitle.text = "For Sale";
        tActionButton.text = "Pay";
        tActionButton2.text = "Pass";
        iArt.sprite = _soSpot.FrontArt;
        focusedSpot = _soSpot.spotDesignation;
        chanceFocused = _cardChance.chanceCards;
        spot = _soSpot;

        iCard.enabled = false;
        
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        switch (_soSpot.spotType)
        {
            case eTypeSpot.utility:

            case eTypeSpot.railroad:
            case eTypeSpot.property:
                sPlayer owner = spot.IsPropertyOwnedByAnyPlayer();
                Debug.Log("The owner is: " + owner);

                if (owner == curPlayer)
                {
                    tTitle.text = "Already Owned";
                    tMessage.text = "You own this property";
                    tActionButton.text = "Confirm";
                    button2.SetActive(false);
                    if (owner.IsPropertyMortgaged(spot.spotDesignation))
                    {
                        iArt.sprite = _soSpot.backArt;
                    }
                }
                else if (!owner)
                {
                    tMessage.text = "Purchase " + _soSpot.nameSpot + " for $" + _soSpot.propertyCost;
                    
                }
                else if (owner != curPlayer)
                {
                    tTitle.text = owner.playerName + " owns this property";
                    button2.SetActive(false);

                    if (!owner.IsPropertyMortgaged(spot.spotDesignation))
                    {
                        if (curPlayer.cashOnHand >= spot.rentofProperty[0])
                        {
                            tMessage.text = "Pay Rent $" + _soSpot.rentofProperty[0];
                            tActionButton.text = "Pay Rent";
                            /*curPlayer.UpdateCash(-spot.rentofProperty[0]);
                            owner.UpdateCash(spot.rentofProperty[0]);
                            //Add Sound
                            Destroy(this.gameObject);*/
                        }
                        else
                        {
                            iArt.sprite = _soSpot.backArt;
                            tMessage.text = "This Property is Mortgaged";
                            tActionButton.text = "Confirm";

                        }
                    }
                    
                }
                
                
                break;
            case eTypeSpot.doNothing:
                tTitle.text = _soSpot.nameSpot;
                tMessage.text = "You have landed on " + _soSpot.nameSpot;
                button2.gameObject.SetActive(false);
                tActionButton.text = "CONFIRM";
                

                break;

            case eTypeSpot.tax:
                tTitle.text = "Pay Tax";
                tMessage.text = "You owe $" + _soSpot.taxCost;
                button2.gameObject.SetActive(false);
                tActionButton.text = "CONFIRM";
                
                break;
            case eTypeSpot.goToJail:
                tTitle.text = "Go To Jail!";
                tMessage.text = "Go directly to Jail.";
                button2.gameObject.SetActive(false);
                tActionButton.text = "CONFIRM";
                
                break;
            case eTypeSpot.chance:
                tTitle.text = _soSpot.nameSpot;
                tMessage.text = "Draw a Chance Card!";
                button2.gameObject.SetActive(false);
                tActionButton.text = "Draw";
                iArt.enabled = false;
                iCard.enabled = true;
                //TODO randomly select card
                randCardNum = Random.Range(0, gm.cardChances.Length);
                iCard.sprite = gm.cardChances[randCardNum].cardArt;
                //iArt.sprite = _cardChance.cardArt;
                button2.SetActive(false);



                break;
            case eTypeSpot.commChest:
                tTitle.text = _soSpot.nameSpot;
                tMessage.text = "Draw a Community Chest Card!";
                button2.gameObject.SetActive(false);
                tActionButton.text = "Draw";
                iArt.enabled = false;
                iCard.enabled = true;
                //TODO randomly select card
                randCardNum = Random.Range(0, gm.cardCommChests.Length);
                iCard.sprite = gm.cardCommChests[randCardNum].cardArt;
                //iArt.sprite = _cardCommChest.cardArt;
                button2.SetActive(false);

                break;


            default:
                Debug.Log("<color = yellow>Not yet implimented</color>");
                break;

        }

        
    }

    public void OnActionButtonPressed()
    {
        GameManager gm = GameManager.gm; //Set up an alias to GameManager (gm)
        sPlayer curPlayer = gm.players[gm.curPlayer]; //Set up an alias to current player (player)
        soSpot curSpot = gm.monoSpots[(int)focusedSpot]; //Set up an alias to current spot that is associated with this Widget (curSpot)
        sPlayer owner = spot.IsPropertyOwnedByAnyPlayer();
        
        int playersCashOnHand = curPlayer.cashOnHand;
        PlayClickSound();
        switch (spot.spotType)
        {
            case eTypeSpot.utility:
            case eTypeSpot.railroad:
            case eTypeSpot.property:
                if (owner == curPlayer) //Current Player owns property, so do nothing
                {
                    
                    Destroy(this.gameObject, 1);
                }
                else if (!owner)
                {
                    if (playersCashOnHand >= spot.propertyCost)
                    {
                        curPlayer.UpdateCash(playersCashOnHand - spot.propertyCost);
                        curPlayer.AddProperty(focusedSpot, false, 0);
                        
                        Destroy(this.gameObject, 1);
                        SpawnMarker();
                    }
                    else
                    {
                        Debug.Log("Not enough Cash");
                    }
                }
                else if (owner != curPlayer)
                {
                    if (!owner.IsPropertyMortgaged(spot.spotDesignation))
                    {
                        if (playersCashOnHand > curSpot.rentofProperty[0])
                        {
                            curPlayer.UpdateCash(playersCashOnHand - spot.rentofProperty[0]);
                            owner.UpdateCash(owner.cashOnHand + spot.rentofProperty[0]);
                            
                            Destroy(this.gameObject, 1);
                        }
                        else
                        {
                            Debug.Log("Not enough Cash");
                        }
                    }
                    
                }
                
                break;
            case eTypeSpot.doNothing:
                
                Destroy(this.gameObject);
                break;
            case eTypeSpot.chance:
                //Make global variable
                gm.cardChances[randCardNum].CardActions();
                Debug.Log("Chance Card Called");
                break;

            case eTypeSpot.commChest:
                //Set scriptable objects
                gm.cardCommChests[randCardNum].CardActions();
                Debug.Log("Community Chest Card Called");
                break;
            case eTypeSpot.tax:
                if (playersCashOnHand >= spot.taxCost)
                {
                    curPlayer.UpdateCash(playersCashOnHand - spot.taxCost);
                }
                else
                {
                    Debug.Log("Not enough cash");
                }
                break;
                
        }

        
        Destroy(this.gameObject);

        //int curplayer = gm.curPlayer;
        //bool isCurrentPlayerInJail = s_Player.isInJail;
        //int costOfHouseOfAtlanticAvenue = gm.monoSpots[(int)ePos.atlantic].costPerHouse;
        //  1.   int player0cashOnHand = gm.players[0].cashOnHand;
        //  2.   int player1turnsInJail = gm.players[1].turnsJail;
        //  3.   eTypeSpot spotTypeofReadingRailroad = gm.monoSpots[(int)ePos.readingRR].spotType;
        //  5.   iArt.sprite = gameObject.GetComponent<soSpot>().backArt;


        //player.AddProperty(focusedSpot, false, 0);
        //s_Player.listPropertiesOwned.Add(s_Player.);
    }

    public void SetMortgageOnProperty()
    {
        GameManager gm = GameManager.gm; //Set up an alias to GameManager (gm)
        sPlayer curPlayer = gm.players[gm.curPlayer]; //Set up an alias to current player (player)
        soSpot curSpot = gm.monoSpots[(int)focusedSpot]; //Set up an alias to current spot that is associated with this Widget (curSpot)
        sPlayer owner = spot.IsPropertyOwnedByAnyPlayer();

        if (owner == curPlayer)
        {
            s_Player.MortgageProperty(curSpot.spotDesignation, true, 0);
            curSpot.isMortgaged = true;
            curPlayer.UpdateCash(+curSpot.propertyCost / 2);
        }
    }

    public void UnmortgageOnProperty()
    {
        GameManager gm = GameManager.gm; //Set up an alias to GameManager (gm)
        sPlayer curPlayer = gm.players[gm.curPlayer]; //Set up an alias to current player (player)
        soSpot curSpot = gm.monoSpots[(int)focusedSpot]; //Set up an alias to current spot that is associated with this Widget (curSpot)
        sPlayer owner = spot.IsPropertyOwnedByAnyPlayer();
        int playersCashOnHand = curPlayer.cashOnHand;

        if (owner == curPlayer && playersCashOnHand > (int)((curSpot.propertyCost / 2)+(curSpot.propertyCost * .05f)))
        {
            s_Player.AddProperty(curSpot.spotDesignation, false, 0);
            curSpot.isMortgaged = false;
            curPlayer.UpdateCash(-(int)((curSpot.propertyCost / 2) + (curSpot.propertyCost * .05f)));
            //curPlayer.UpdateCash(-curSpot.propertyCost / 2 + ((curSpot.propertyCost / 2) * interest));
        }
    }

    public void OnActionButton2Pressed()
    {
        PlayClickSound();
        Destroy(this.gameObject);
    }

    void SpawnMarker()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];

        float x = 0f;
        float z = 0f;
        float y = 0f;

        if (spot.spotDesignation < ePos.stCharles)
        {
            z = -1.1f;
            x = -.15f;
        }
        else if (spot.spotDesignation < ePos.kentucky)
        {
            z = .15f;
            x = -1.1f;
        }
        else if (spot.spotDesignation < ePos.pacific)
        {
            z = 1.1f;
            x = .15f;
        }
        else 
        {
            z = .65f;
            x = 1.1f;
        }

        Transform loc = gm.s_Board.location[(int)focusedSpot];
        Vector3 offset = new Vector3(x, y, z);
        
        GameObject marker = Instantiate(gm.pMarker, loc);
        marker.transform.position = loc.position + offset;

        Material mat = marker.GetComponentInChildren<Renderer>().material;
        Debug.Log("Mat" + mat);
        Debug.Log("Mat" + curPlayer);
        mat.SetColor("_Color", curPlayer.playerColor);
        mat.SetColor("_SpecColor", curPlayer.playerColor);
        mat.SetColor("_EmissionColor", curPlayer.playerColor);
    }

}
