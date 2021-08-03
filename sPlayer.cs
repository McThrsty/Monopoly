using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class PropertyOwnership
{
    public string name;
    public ePos position;
    public bool isMortgaged;
    public int houseAmt;

    public GameObject marker;

    public PropertyOwnership (ePos _position, bool _isMortgaged, int _houseAmt)
    {
        position = _position;
        name = GameManager.gm.monoSpots[(int)_position].nameSpot;
        isMortgaged = _isMortgaged;
        houseAmt = _houseAmt;
    }
}
public class sPlayer : MonoBehaviour
{
    public int index;
    private GameManager gm;
    private sPlayer s_Player;
    public sBoard s_Board;
    public cInGame c_InGame;
    public AudioManager audioManager;


    [Space]
    [Header("AI Controls")]
    public ePlayerType playerType;
    public eDifficulty difficulty; // only used if the player is an AI

    [Space]
    [Header("Player Vars")]
    public int cashOnHand;
    public string playerName;
    public Color playerColor;
    public ePiece piece;
    public ePos playerPos;
    public eChanceCards cardChance;
    public eCommChestCards cardCommChest;
    public eDiceRolls diceRolls;
    public bool[] hasGetOutOfJail;
    public bool isInJail;
    public int turnsJail;
    public int amtOfDoubles;
    public bool isBankrupt;
    public int playerIndex;
    public Image[] iButtonPlayerColor;
    public int die1;
    public int die2;
    public int dieRoll;

    public GameObject modelPiece;
    public wPlayerHUD w_PlayerHUD;

    public GameObject playerPiece;

    /*public Transform connect;
    public Transform StCharles;
    public Transform NewYork;
    public Transform Kentucky;
    public Transform Marvin;
    public Transform Pacific;
    public Transform Boardwalk;
    public Transform Medit;*/

    /*float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;*/
    /*private Sprite[] diceSides;
    public Image iDiceImage1;
    public Image iDiceImage2;*/


    [Space]
    [Header("Property Ownership")]
    public List<PropertyOwnership> listPropertiesOwned;
    

    public void InitUI(int _playerIndex, cInGame _cInGame)
    {
        gm = GameManager.gm;
        s_Player = gm.players[_playerIndex];
        playerIndex = _playerIndex;
        
        s_Player.index = playerIndex;
        s_Player.gameObject.name = "Player " + playerIndex.ToString();

        foreach (Image i in iButtonPlayerColor)
        {
            i.color = gm.playerColors[_playerIndex];
        }
        

    }

    /*public void DieFace()
    {
        switch (diceRolls)
        {
            case eDiceRolls.one:

                break;
            case eDiceRolls.two:
                break;
            case eDiceRolls.three:
                break;
            case eDiceRolls.four:
                break;
            case eDiceRolls.five:
                break;
            case eDiceRolls.six:
                break;
        }
            
    }*/

    public void UpdateCash(int _amount)
    {
        cashOnHand = _amount;
        w_PlayerHUD.UpdateCashOnHand(_amount);
    }

    public void MovePlayer()
    {
        GameManager gm = GameManager.gm;
        sPlayer curPlayer = gm.players[gm.curPlayer];
        ePos currentPos = playerPos;
        int die1 = Random.Range(1, 7);
        int die2 = Random.Range(1, 7);
        //int die1 = 3;
        //int die2 = 3;
        int dieRoll = die1 + die2;
        //int dieRoll = 7;
        bool doublesRolled = false;
        

        Debug.Log("Die1: " + die1 + " Die2: " + die2 + " DieRoll: " + dieRoll);

        if (die1 == die2)
        {
            doublesRolled = true;
            amtOfDoubles++;
            if (amtOfDoubles > 2)
            {
                GoToJail();
                return;
                
            }
        }


        int newPos = (int)playerPos + dieRoll;
        if (newPos >= (int)ePos.terminator)
        {
            playerPos = newPos - ePos.terminator;
            UpdateCash(curPlayer.cashOnHand + 200);
        }
        else
        {
            playerPos = (ePos)newPos;
        }

        /*switch (diceRolls)
        {
            case eDiceRolls.one:
                
                break;
            case eDiceRolls.two:

                break;
            case eDiceRolls.three:

                break;
            case eDiceRolls.four:

                break;
            case eDiceRolls.five:

                break;
            case eDiceRolls.six:

                break;
        }*/

        GameManager.gm.c_InGame.DisplayRollDiceButton(doublesRolled);

        //c_InGame.ChangeDieSprites(die1 + 1, die2 + 1);

        StartCoroutine(MovePlayerPiece(currentPos, playerPos, newPos >= (int)ePos.terminator, doublesRolled));
        
    }

    /*public void DiceRollChangeSprites(Image die1, Image die2)
    {
        die1.sprite = gm.[die1];
    }*/

    public void LandsOnSpot(bool _doublesRolled)
    {
        AudioManager am = AudioManager.am;
        Debug.Log("The space landed on was:" + GameManager.gm.monoSpots[(int)playerPos].nameSpot);
        //MovePlayerPieceToPos(playerPos);
        GameManager.gm.c_InGame.DisplayCenterWidget(playerPos, cardChance, cardCommChest);
        GameManager.gm.c_InGame.DisplayRollDiceButton(_doublesRolled);
    }

    public void GoToJail()
    {
        Debug.Log("Go To Jail");
        playerPos = ePos.justVisiting;
        isInJail = true;
    }

    public void AddProperty(ePos _position, bool _isMortgaged, int _houseAmt)
    {
        listPropertiesOwned.Add(new PropertyOwnership(_position, _isMortgaged, _houseAmt));

    }

    public void MortgageProperty(ePos _position, bool _isMortgaged, int _houseAmt)
    {
        index = (int)playerPos;

        listPropertiesOwned.RemoveAt(index);
    }

    public void SetMortgageProperty(ePos _property, bool _state)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                listPropertiesOwned[i].isMortgaged = _state;
            }
        }
        
    }

    public bool IsPropertyMortgaged(ePos _property)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                return listPropertiesOwned[i].isMortgaged;
            }
        }
        return false;
    }

    public bool IsPropertyOwned (ePos _property)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                return true;
            }
        }
        return false;
    }

    public int GetIndexOfProperty(ePos _property)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                return i;
            }
        }
        return 99;
    }

    public void RemoveProperty(ePos _property)
    {
        int idx = GetIndexOfProperty(_property);
        listPropertiesOwned.RemoveAt(idx);
        listPropertiesOwned.Sort();
    }

    public void AddMarkerToList(ePos _property, GameObject _marker)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                listPropertiesOwned[i].marker = _marker;
            }
        }
    }

    public void DestroyMarker(ePos _property)
    {
        for (int i = 0; i < listPropertiesOwned.Count; i++)
        {
            if (listPropertiesOwned[i].position == _property)
            {
                Destroy(listPropertiesOwned[i].marker);
            }
        }
    }

    public void SpawnPlayerPiece()
    {
        GameManager gm = GameManager.gm;
        playerPiece = Instantiate(gm.pPiece[(int)piece]);
        MovePlayerPieceToPos(playerPos);
    }

    public void MovePlayerPieceToPos(ePos _pos)
    {
        GameManager gm = GameManager.gm;
        playerPiece.transform.position = gm.s_Board.location[(int)_pos].position;
        playerPiece.transform.rotation = gm.s_Board.location[(int)_pos].rotation;
    }

    IEnumerator MovePlayerPiece(ePos _curPos, ePos _finalPos, bool _passGo, bool _doublesRolled)
    {
        GameManager gm = GameManager.gm;


        int spaces =  (int)_finalPos - (int)_curPos;

        if (_passGo)
        {
            spaces = (int)_finalPos - (int)_curPos + 40; //Offsets and reverses sign to get us to the right number of steps
        }

        int counter = 0;
        int nextPos = (int)_curPos++;

        // Correct for nextPos greater than 39:
        if (nextPos == 40)
        {
            nextPos = 0;
        }

        while (spaces >= counter)
        {
            Vector3 currentPos = playerPiece.transform.position;
            Vector3 newPos = gm.s_Board.location[nextPos].position;

            Quaternion currentRot = playerPiece.transform.rotation;
            Quaternion newRot = gm.s_Board.location[nextPos].rotation;

            float elapsedTime = 0f;
            float waitTime = 0.75f;

            

            while (elapsedTime < waitTime)
            {
                
                playerPiece.transform.position = Vector3.Lerp(currentPos, newPos, (elapsedTime / waitTime));
                playerPiece.transform.rotation = Quaternion.Lerp(currentRot, newRot, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
            nextPos++;

            

            // Correct for nextPos greater than 39:
            if (nextPos == 40)
            {
                nextPos = 0;
            }
            counter++;
        }
        LandsOnSpot(_doublesRolled);
    }
}
