using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wMessage : MonoBehaviour
{
    public Text tTitle;
    public Text tMessage;

    public soSpot spot;
    GameManager gm;
    public sPlayer curPlayer;

    string action;
    GameObject sendObject;

    public void InitUI (string _tTitle, string _tMessage, string _action, GameObject _sendObject)
    {
        gm = GameManager.gm;

        tTitle.text = _tTitle;
        tMessage.text = _tMessage;
        action = _action;
        sendObject = _sendObject;
    }

    public void MortgageProp()
    {
        int mortgageValue = (int)((float)spot.propertyCost * .5f);
        curPlayer.UpdateCash(mortgageValue);
        curPlayer.SetMortgageProperty(spot.spotDesignation, true);
        sendObject.SendMessage("DisplayCurSpot");
        Destroy(this.gameObject);
    }

    public void UnMortgageProp()
    {
        int mortgageCost = (int)((float)spot.propertyCost * .55f);
        curPlayer.UpdateCash(-curPlayer.cashOnHand - mortgageCost);
        curPlayer.SetMortgageProperty(spot.spotDesignation, false);
        sendObject.SendMessage("DisplayCurSpot");
        Destroy(this.gameObject);
    }

    public void PurchaseProp()
    {
        Destroy(this.gameObject);
    }

    public void DoNothing()
    {
        Destroy(this.gameObject);
    }

    public void OnConfirmHit()
    {
        if(sendObject)
        {
            sendObject.SendMessage(action);
        }
        Destroy(this.gameObject);
    }

    public void OnCancelHit()
    {
        Destroy(this.gameObject);
    }

}
