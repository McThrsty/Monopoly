using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class cInGame : MonoBehaviour
{
    public sPlayer s_Player;
    private GameManager gm;
    //private wPlayerInfo w_PlayerInfo;
    public wMessage w_Message;
    public soSpot so_Spot;
    public wPlayerInfo w_PlayerInfo;
    public AudioManager audio_Manager;

    public Button bRollDice;
    public Button bEndTurn;
    public Button bManageProperty;

    public Image iPiece;
    public Text tPlayerName;
    public Text tPlayerCash;
    public string playerCash;

    public Transform panelHUD;

    public GameObject w_PlayerHUD;
    public GameObject wCenter;
    public GameObject wPropertyInventory;
    public GameObject wPropManageConfirm;

    /*public Image iDiceImage1;
    public Image iDiceImage2;
    public eDiceRolls diceRolls;
    public Sprite Die1Face;*/

    public AudioClip click;
    AudioSource audioSource;

    /*private void Start()
    {
        die1 = GetComponent<Image>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }*/

    public void InitUI()
    {
        gm = GameManager.gm;
        DisplayRollDiceButton(true);
        SetPlayerHUDs();


    }

    void SetPlayerHUDs()
    {
        for (int i = 0; i < GameManager.maxNumPlayers; i++)
        {
            gm.players[i].w_PlayerHUD = Instantiate(w_PlayerHUD, panelHUD).GetComponent<wPlayerHUD>();
            gm.players[i].w_PlayerHUD.InitUI(gm.players[i]);
            gm.players[i].w_PlayerHUD.SetHilightPlayer(i == gm.curPlayer);
        }
    }

    public void SetHilightedPlayerHUD()
    {
        for (int i = 0; i < gm.players.Length; i++)
        {
            gm.players[i].w_PlayerHUD.SetHilightPlayer(i == gm.curPlayer);
        }
    }

    public void DisplayCenterWidget( ePos _spot, eChanceCards _cardChance, eCommChestCards _cardCommChest)
    {
        wCenter scr = Instantiate(wCenter, this.gameObject.transform).GetComponent<wCenter>();
        scr.InitUI("FOR SALE!", GameManager.gm.monoSpots[(int)_spot], "BUY", GameManager.gm.cardChances[(int)_cardChance], GameManager.gm.cardCommChests[(int)_cardCommChest]);
    }

    public void DisplayRollDiceButton (bool _active)
    {
        bRollDice.gameObject.SetActive(_active);
        bEndTurn.gameObject.SetActive(!_active);
    }

    public void PropertiesManager()
    {
        wPropertyInventory scr = Instantiate(wPropertyInventory, this.gameObject.transform).GetComponent<wPropertyInventory>();
        scr.InitUI();
        PlayClickSound();
    }
    
    public void OnBackClicked()
    {
        GameManager.gm.LoadScene(0);
        PlayClickSound();
    }

    public void OnDieRollClicked()
    {
        gm.players[gm.curPlayer].MovePlayer();
        bRollDice.gameObject.SetActive(false);
        PlayClickSound();
        //iDie1Face.sprite = gm.diefaces[s_Player.die1];
        //iDie2Face.sprite = gm.diefaces[s_Player.die2];
        //StartCoroutine("RollDiceImages");
    }

    /*private IEnumerable RollDiceImages()
    {
        int randDice1Side = 0;
        int finalSideDie1 = 0;
        int randDice2Side = 0;
        int finalSideDie2 = 0;

        for (int i = 0; i <= 30; i++)
        {
            randDice1Side = Random.Range(0, 5);
            die1.sprite = diceSides[randDice1Side];

            randDice2Side = Random.Range(0, 5);
            die2.sprite = diceSides[randDice2Side];
            yield return new WaitForSeconds(0.05f);
        }
        finalSideDie1 = randDice1Side + 1;
        finalSideDie2 = randDice2Side + 1;
    }*/

    public void OnEndTurnClicked()
    {
        gm.AdvanceToNextPlayer();
        PlayClickSound();
    }

    public void PlayerInfoInGame()
    {
        playerCash = s_Player.cashOnHand.ToString();
        iPiece.sprite = gm.sprPieces[(int)s_Player.piece];
        tPlayerName.text = gm.players[4].playerName;
        tPlayerCash.text = "$"+playerCash;
    }

    /*public void ChangeDieSprites(int _amount1, int _amount2)
    {
        gm = GameManager.gm;
        switch (diceRolls)
        {

            case eDiceRolls.one:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
            case eDiceRolls.two:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
            case eDiceRolls.three:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
            case eDiceRolls.four:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
            case eDiceRolls.five:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
            case eDiceRolls.six:
                iDiceImage1.sprite = gm.diefaces[_amount1];
                iDiceImage2.sprite = gm.diefaces[_amount2];
                break;
        }
    }*/

    void PlayClickSound()
    {
        audioSource.PlayOneShot(click, 0.7f);
    }



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
