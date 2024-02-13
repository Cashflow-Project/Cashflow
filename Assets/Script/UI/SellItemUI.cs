using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItemUI : MonoBehaviour
{
    public SellObjFromYourself sellObjFromYourselfParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemCount;
    

    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetCountInItem(string _itemCount)
    {
        itemCount.text = _itemCount;
    }

    public void SellPressed()
    {
        UIController.instance.investSellCanvas.SetActive(true);
}
}
