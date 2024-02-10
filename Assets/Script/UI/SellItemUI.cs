using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : MonoBehaviour
{
    public SellObjFromYourself sellObjFromYourselfParent;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemPrice;


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
