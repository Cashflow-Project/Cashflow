using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "SmallChangeCard", order = 1)]
public class CardSmallChangeScriptableObj : ScriptableObject
{
    public string cardName;

    public int BusinessValue;

    public int DownPayment;

    public int BankLoan;

    public int CashflowIncome;

    public bool house3s2;
    public bool house2s1;
    public bool Condominium;
    public bool CommercialBuilding;
    public bool Apartment;

    public bool canLoan;

    public Sprite cardSprite;
}
