using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketSellItemUI : MonoBehaviour
{
    public SellObjFromMarketCard MarketSellObjFromYourselfParent;
    [SerializeField] public TMP_Text itemName;
    [SerializeField] public TMP_Text itemDownPayment;
    [SerializeField] public TMP_Text itemIncome;
    [SerializeField] public TMP_Text itemPrice;
    [SerializeField] public Button itemMarketSellBtn;

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
    public void SetPriceInItem(int _itemPrice,int count)
    {
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true 
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard == 0 
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost > 0 && count == 0)
        {
            itemPrice.text = (_itemPrice + MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost).ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard == 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost > 0 && count > 0)
        {
            itemPrice.text = (_itemPrice + (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost * count)).ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard > 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost == 0 && count == 0)
        {
            itemPrice.text = (_itemPrice + (_itemPrice * MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard / 100)).ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == true
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard > 0
            && MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost == 0 && count > 0)
        {
            //wait
            itemPrice.text = (_itemPrice + (_itemPrice * MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].percentCard / 100)).ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == false && count == 0)
        {
            itemPrice.text = MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost.ToString();
        }
        else if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].increseFromSameCost == false && count > 0)
        {
            itemPrice.text = (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost * count).ToString();
        }
    }

    public void SellPressed()
    {
        MarketDeckController.instance.SellCost();

    }
}
