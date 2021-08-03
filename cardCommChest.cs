using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Community Chest Card", menuName = "Create Community Chest Card")]
public class cardCommChest : ScriptableObject
{
    public eCardType cardType;
    public eCommChestCards commChestCards;
    public ePos focusedSpot;
    public string cardText;
    public int amount;
    public Sprite cardArt;
    public Transform spot;

    GameManager gm = GameManager.gm;

    public void CardActions()
    {
        switch (cardType)
        {
            case eCardType.collectReward:
                //CollectRewardCard();
                Debug.Log("Collect Reward");
                break;
            case eCardType.payBank:
                //PayBankCard();
                Debug.Log("Pay Bank");
                break;
            case eCardType.payEachPlayer:
                //PayEachPlayerCard();
                Debug.Log("Pay each Player");
                break;
            case eCardType.goToSpot:
                //GoToSpotCard();
                Debug.Log("Go to spot");
                break;
            case eCardType.giveCard:
                //GivePlayerCard();
                Debug.Log("Give Card");
                break;
            case eCardType.payHouseHotel:
                //PayHouseHotelCard();
                Debug.Log("Pay for Houses and Hotels");
                break;
        }
    }

    public void PayBankCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        gm.players[gm.curPlayer].UpdateCash(curPlayer.cashOnHand - amount);
    }

    public void PayEachPlayerCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        gm.players[gm.curPlayer].UpdateCash((curPlayer.cashOnHand - amount) * gm.numPlayers);
    }

    public void CollectRewardCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        gm.players[gm.curPlayer].UpdateCash(curPlayer.cashOnHand + amount);
    }

    public void GoToSpotCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        soSpot curSpot = gm.monoSpots[(int)focusedSpot];

    }

    public void GivePlayerCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
    }

    public void PayHouseHotelCard()
    {
        sPlayer curPlayer = gm.players[gm.curPlayer];
        gm.players[gm.curPlayer].UpdateCash(amount);
    }
}
