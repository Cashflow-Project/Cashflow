using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : MonoBehaviour
{
    public SellObjFromYourself sellObjFromYourselfParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemPrice;
    public bool isON2U;
    public bool isMYT4U;
    public bool isGRO4US;
    public bool isOK4U;

    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetPriceInItem(string _itemPrice)
    {
        itemPrice.text = _itemPrice;
    }

    public void SellPressed()
    {
        
    }
}
