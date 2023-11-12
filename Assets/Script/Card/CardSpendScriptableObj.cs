using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName ="New Card", menuName = "SpendCard",order = 1)]
public class CardSpendScriptableObj : ScriptableObject
{
    public string cardName;
    
    public int payCost;

    public Sprite cardSprite;

    public bool hasChildsOrNot;
}
