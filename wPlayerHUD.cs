using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wPlayerHUD : MonoBehaviour
{
    public Image iHilite;
    public Text tPlayerName;
    public Text tCashOnHand;
    public Image iPiece;



    public Image[] iColorChange;

    public void InitUI(sPlayer _sPlayer)
    {
        tPlayerName.text = _sPlayer.playerName;
        tCashOnHand.text = "$" + _sPlayer.cashOnHand.ToString();

        iPiece.sprite = GameManager.gm.sprPieces[(int)_sPlayer.piece];

        foreach (Image i in iColorChange)
        {
            i.color = _sPlayer.playerColor;
        }
    }

    public void UpdateCashOnHand(int _amount)
    {
        tCashOnHand.text = "$" + _amount;
    }

    public void SetHilightPlayer(bool _active)
    {
        iHilite.gameObject.SetActive(_active);
    }
}
