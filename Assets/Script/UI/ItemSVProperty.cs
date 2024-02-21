using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSVProperty : MonoBehaviour
{
    public SVProperty itemPropertyParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemCost;
    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetCostInItem(string _itemCost)
    {
        itemCost.text = _itemCost;
    }


}
