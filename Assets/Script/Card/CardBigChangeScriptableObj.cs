using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "BigChangeCard", order = 1)]
public class CardBigChangeScriptableObj : ScriptableObject
{
    public string cardName;

    public int BusinessValue;
    public int DownPayment;
    public int BankLoan;
    public int CashflowIncome;

    public bool house3s2;
    public bool CommercialBuilding;
    public bool Apartment;
    public bool Business;

    public int count;

    public bool canLoan;

    public Sprite cardSprite;


}
