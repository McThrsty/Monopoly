using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class wPlayerInfo : MonoBehaviour
{
    private GameManager gm;
    private sPlayer s_Player;
    private cSetup c_Setup;
    public int playerIndex;

    public Image iPiece;
    public Text tPiece;

    public Image iPlayerType;
    public Text tPlayerType;

    public GameObject panelPlayerSetup;
    public GameObject panelAddPlayer;

    public Image[] iButtonPlayerColor; // This is where we change the color of all the buttons

    public AudioClip click;
    AudioSource audioSource;

    public void InitUI(int _playerIndex, cSetup _cSetup)
    {
        gm = GameManager.gm;
        s_Player = gm.players[_playerIndex];
        playerIndex = _playerIndex;
        c_Setup = _cSetup;
        s_Player.index = playerIndex;
        s_Player.gameObject.name = "Player " + playerIndex.ToString();
        s_Player.playerColor = gm.playerColors[_playerIndex];

        foreach (Image i in iButtonPlayerColor)
        {
            i.color = gm.playerColors[_playerIndex];
        }

        // TODO: Show how to add player color to sPlayer

        SetDefaultState();
    }

    void SetDefaultState()
    {
        panelPlayerSetup.SetActive(false);
        panelAddPlayer.SetActive(true);


        s_Player.piece = (ePiece)playerIndex; // Initial setting of the player piece
        CheckPlayerPiece();
        SetPlayerPieceArt();

        s_Player.playerType = ePlayerType.none;
        SetPlayerTypeArt();

        CheckForStartButton();
    }

    

    public void OnAddPlayerClicked()
    {
        panelPlayerSetup.SetActive(true);
        panelAddPlayer.SetActive(false);

        s_Player.piece = (ePiece)playerIndex;
        s_Player.playerType = ePlayerType.human;
        SetPlayerTypeArt();

        CheckPlayerPiece();
        SetPlayerPieceArt();
        PlaySound();



        gm.numPlayers++;
        gm.numHumanPlayers++;
        CheckForStartButton();
    }

    void CheckForStartButton()
    {
        c_Setup.DisplayStartGame(gm.numHumanPlayers > 0 && gm.numPlayers > 1);
    }

    public void OnPieceClicked()
    {
        // Get the piece to increment
        s_Player.piece++;
        // Check for last piece
        if (s_Player.piece == ePiece.terminator)
        {
            s_Player.piece = 0;
        }
        // Check to make sure that the piece isn't used by any other player
        CheckPlayerPiece();

        // Display the new piece
        SetPlayerPieceArt();
        PlaySound();
    }

    void CheckPlayerPiece()
    {
        bool checkAgain = false;
        // Step through all of the players
        for (int i = 0; i < GameManager.maxNumPlayers; i++)
        {
            // Check to make sure the piece isn't used
            // Check all of the players except the index that we are
            if (s_Player.piece == gm.players[i].piece && i != playerIndex && gm.players[i].playerType != ePlayerType.none)
            {
                // Increment the piece
                s_Player.piece++;
                // Check for last piece
                if (s_Player.piece == ePiece.terminator)
                {
                    s_Player.piece = 0;
                }
                checkAgain = true;
                break;
            }
        }
        if (checkAgain)
        {
            CheckPlayerPiece();
        }
    }

    public void OnPlayerTypeClicked()
    {
        s_Player.playerType++;
        if (s_Player.playerType > ePlayerType.AI)
        {
            s_Player.playerType = ePlayerType.none;
            gm.numPlayers--;
            SetDefaultState();
        }
        else
        {
            if (s_Player.playerType == ePlayerType.AI)
            {
                gm.numHumanPlayers--;
            }
        }
        SetPlayerTypeArt();
        PlaySound();
        CheckForStartButton();
    }

    void SetPlayerPieceArt()
    {
        iPiece.sprite = gm.sprPieces[(int)s_Player.piece];
        tPiece.text = gm.strPieces[(int)s_Player.piece];
    }

    void SetPlayerTypeArt()
    {
        iPlayerType.sprite = gm.sprPlayerType[(int)s_Player.playerType];
        tPlayerType.text = gm.strPlayerType[(int)s_Player.playerType];
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(click, 0.7f);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}