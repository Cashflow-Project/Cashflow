using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSVIncome : MonoBehaviour
{
    public SVIncome itemIncomeParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemIncome;
    // Start is called before the first frame update
    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetIncomeInItem(string _itemIncome)
    {
        itemIncome.text = _itemIncome;
    }

   
}
