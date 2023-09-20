using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "SpendCard",order = 1)]
public class CardSpendScriptableObj : ScriptableObject
{
    public string cardName;
    
    public int payCost;

    public bool canLoan;

    public Sprite cardSprite;
}
