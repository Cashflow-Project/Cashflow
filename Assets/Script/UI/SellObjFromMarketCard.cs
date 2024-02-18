using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SellObjFromMarketCard : MonoBehaviourPunCallbacks
{
    public static SellObjFromMarketCard instance;

    [SerializeField] private GameObject SellMarketListWindow;
    [SerializeField] private MarketSellItemUI MarketSellItemUIPrefab;
    [SerializeField] private Transform MarketSellListParent;
    public List<MarketSellItemUI> _MarketItemList = new List<MarketSellItemUI>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        
        UpdateMarketItemList();
      
    }


    public void CancelList()
    {
        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(true);
        UIController.instance.SellButton.SetActive(false);


        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        for (int i = 0; i < _MarketItemList.Count; i++)
        {
            Destroy(_MarketItemList[i].gameObject);
        }
        _MarketItemList.Clear();
    }


    public void UpdateMarketItemList()
    {
        for (int i = 0; i < _MarketItemList.Count; i++)
        {
            Destroy(_MarketItemList[i].gameObject);
        }
        _MarketItemList.Clear();


        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house3s2 == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome32 == true)
        {
            for (int j = 0; j < GameManager.instace.playerList.Count; j++)
            {
                //generate a new list with update info
                for (int i = 0; i < GameManager.instace.playerList[j].house3s2List.Count; i++)
                {

                    //skip empty 
                    if (GameManager.instace.playerList[j].house3s2List.Count == 0)
                    {
                        continue;
                    }

                    MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                    newItem.MarketSellObjFromYourselfParent = this;
                    newItem.SetItemName(GameManager.instace.playerList[j].house3s2List[i].CardName);
                    newItem.SetItemDownPayment(GameManager.instace.playerList[j].house3s2List[i].DownPayment.ToString());
                    newItem.SetItemIncome(GameManager.instace.playerList[j].house3s2List[i].CashflowIncome.ToString());
                    newItem.SetPriceInItem(GameManager.instace.playerList[j].house3s2List[i].DownPayment, 0);
                    newItem.transform.SetParent(MarketSellListParent);

                    _MarketItemList.Add(newItem);
                }
            }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house2s1 == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome21 == true)
        {
                for (int j = 0; j < GameManager.instace.playerList.Count; j++)
                {
                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[j].house2s1List.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[j].house2s1List.Count == 0)
                        {
                            continue;
                        }

                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house2s1List[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].house2s1List[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].house2s1List[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].house2s1List[i].DownPayment, 0);
                        newItem.transform.SetParent(MarketSellListParent);

                        _MarketItemList.Add(newItem);
                    }
                }
            }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Condominium == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasCondominium21 == true)
        {
                for (int j = 0; j < GameManager.instace.playerList.Count; j++)
                {
                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[j].CondominiumList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[j].CondominiumList.Count == 0)
                        {
                            continue;
                        }

                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CondominiumList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].CondominiumList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].CondominiumList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].CondominiumList[i].DownPayment, 0);
                        newItem.transform.SetParent(MarketSellListParent);

                        _MarketItemList.Add(newItem);
                    }
                }
            }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].CommercialBuilding == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hascommercialBuilding == true)
        {
                for (int j = 0; j < GameManager.instace.playerList.Count; j++)
                {
                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[j].CommercialBuildingList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[j].CommercialBuildingList.Count == 0)
                        {
                            continue;
                        }

                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CommercialBuildingList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].CommercialBuildingList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].CommercialBuildingList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].CommercialBuildingList[i].DownPayment, GameManager.instace.playerList[j].CommercialBuildingList[i].count );
                        newItem.transform.SetParent(MarketSellListParent);

                        _MarketItemList.Add(newItem);
                    }
                }
            }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Apartment == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasApartment == true)
        {
                for (int j = 0; j < GameManager.instace.playerList.Count; j++)
                {
                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[j].ApartmentList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[j].ApartmentList.Count == 0)
                        {
                            continue;
                        }

                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].ApartmentList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].ApartmentList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].ApartmentList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].ApartmentList[i].DownPayment, GameManager.instace.playerList[j].ApartmentList[i].count);
                        newItem.transform.SetParent(MarketSellListParent);

                        _MarketItemList.Add(newItem);
                    }
                }
            }
        
        }
    }
}
