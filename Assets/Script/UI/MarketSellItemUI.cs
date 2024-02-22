using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class MarketSellItemUI : MonoBehaviourPunCallbacks
{
    public SellObjFromMarketCard MarketSellObjFromYourselfParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemDownPayment;
    [SerializeField] public TMP_Text itemIncome;
    [SerializeField] public TMP_Text itemPrice;
    [SerializeField] public Button itemMarketSellBtn;
    [SerializeField] public bool isSelected;
    
    public void SetItemSelect(bool _itemSelect)
    {
       isSelected = _itemSelect ;
       
    }

    public void SetItemName(string _itemName)
    {
        itemName.text = _itemName;
    }

    public void SetItemDownPayment(string _itemDownPayment)
    {
        itemDownPayment.text = _itemDownPayment;
    }
    public void SetItemIncome(string _itemIncome)
    {
        itemIncome.text = _itemIncome;
    }
    public void SetPriceInItem(int countPrice,int count,int mortgage)
    {
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true 
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard == 0 
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost > 0)
        {
            itemPrice.text = ((countPrice + MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost) - mortgage).ToString();
            
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard > 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost == 0 )
        {
            itemPrice.text = (countPrice + (countPrice * MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard / 100) - mortgage).ToString();
        }
  
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == false
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard == 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost > 0 && count == 0)
        {
            itemPrice.text = (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost - mortgage).ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == false
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard == 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost > 0 && count > 0)
        {
            itemPrice.text = ((MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost * count) - mortgage).ToString();
        }
        
        if (Int32.Parse(itemPrice.text) < 0)
        {
            itemMarketSellBtn.enabled = false;
        }
        if (Int32.Parse(itemPrice.text) > 0)
        {
            itemMarketSellBtn.enabled = true;
        }
    }

    public void SellPressed()
    {
        isSelected = true;
        
    }

    



}
