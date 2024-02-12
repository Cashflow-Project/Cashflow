using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "GlobalMarketCard", order = 1)]
public class CardGlobalMarketScriptableObj : ScriptableObject
{
    public string cardName;

    public int Cost;
    public int percentCard;

    public bool ON2U;
    public bool MYT4U;
    public bool GRO4US;
    public bool OK4U;

    public bool GoldCoins;
    public bool house3s2;
    public bool house2s1;
    public bool Condominium;
    public bool CommercialBuilding;
    public bool Apartment;

    public bool notSell;
    public bool canLoan;
    public bool increseFromSameCost;
    public Sprite cardSprite;
}
