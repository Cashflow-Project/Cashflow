using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSVDebt : MonoBehaviour
{
    public SVDebt itemDebtParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemDebt;
    // Start is called before the first frame update
    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetDebtInItem(string _itemDebt)
    {
        itemDebt.text = _itemDebt;
    }

}
