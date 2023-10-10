using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "GlobalMarketCard", order = 1)]
public class CardGlobalMarketScriptableObj : ScriptableObject
{
    public string cardName;

    public int Cost;

    public bool canLoan;

    public Sprite cardSprite;
}
