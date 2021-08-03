using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spot", menuName = "Create Spot")]

public class soSpot : ScriptableObject
{
    public string nameSpot;
    public ePos spotDesignation;
    public eTypeSpot spotType;
    public ePropertyGroupColor propGroupColor;
    public int taxCost;
    public int costPerHouse;
    public int[] rentofProperty;
    public int propertyCost;

    public bool isOwned;
    public bool isMortgaged;
    public int houseAmt;

    public Sprite FrontArt;
    public Sprite backArt;

    public sPlayer IsPropertyOwnedByAnyPlayer()
    {
        GameManager gm = GameManager.gm;
        for (int i = 0; i < gm.players.Length; i++)
        {
            if (gm.players[i].IsPropertyOwned(spotDesignation))
            {
                return gm.players[i]; // Send back the player that owns the property
            }
        }
        return null; // No one ownes the property
    }

}
