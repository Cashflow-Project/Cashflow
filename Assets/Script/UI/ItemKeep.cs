using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemKeep : MonoBehaviour
{
    public ListKeep itemKeepParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemPrice;
    // Start is called before the first frame update
    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetPriceInItem(string _itemPrice)
    {
        itemPrice.text = _itemPrice;
    }
}
