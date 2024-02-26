using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
public class SellObjFromMarketCard : MonoBehaviourPunCallbacks
{
    public static SellObjFromMarketCard instance;

    [SerializeField] private GameObject SellMarketListWindow;
    [SerializeField] private MarketSellItemUI MarketSellItemUIPrefab;
    [SerializeField] private Transform MarketSellListParent;
    public List<MarketSellItemUI> _MarketItemList = new List<MarketSellItemUI>();

    void Update()
    {
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house3s2 == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house2s1 == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Condominium == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].CommercialBuilding == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Apartment == true)
        {
            UpdateItemSelect();
        }


    }

    private void OnEnable()
    {
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house3s2 == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house2s1 == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Condominium == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].CommercialBuilding == true
            || MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Apartment == true)
        {
            UpdateMarketItemList();
        }

    }


    void ScaleObjectWithScreenSize(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();

        // Get the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // You may need to adjust these values based on your specific requirements
        float scaleFactorX = screenWidth / 1920f; // 1920 is a reference width
        float scaleFactorY = screenHeight / 1080f; // 1080 is a reference height

        // Apply the scale to the object's RectTransform
        rectTransform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1f);
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
                        GameManager.instace.playerList[j].hasHome32 = false;
                        continue;
                    }
                    if (GameManager.instace.playerList[j].house3s2List[i].isSelected == false)
                    {
                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house3s2List[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].house3s2List[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].house3s2List[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].house3s2List[i].BusinessValue, 0, GameManager.instace.playerList[j].house3s2List[i].BankLoan);
                        newItem.SetItemSelect(GameManager.instace.playerList[j].house3s2List[i].isSelected);
                        newItem.transform.SetParent(MarketSellListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _MarketItemList.Add(newItem);
                    }
                    if (GameManager.instace.playerList[j].house3s2List[i].isSelected == true)
                    {
                        GameManager.instace.playerList[j].house3s2List.RemoveAt(i);
                    }

                }
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
                        GameManager.instace.playerList[j].hasHome21 = false;
                        continue;
                    }
                    if (GameManager.instace.playerList[j].house2s1List[i].isSelected == false)
                    {
                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house2s1List[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].house2s1List[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].house2s1List[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].house2s1List[i].BusinessValue, 0, GameManager.instace.playerList[j].house2s1List[i].BankLoan);
                        newItem.SetItemSelect(GameManager.instace.playerList[j].house2s1List[i].isSelected);

                        newItem.transform.SetParent(MarketSellListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _MarketItemList.Add(newItem);
                    }
                    if (GameManager.instace.playerList[j].house2s1List[i].isSelected == true)
                    {
                        GameManager.instace.playerList[j].house2s1List.RemoveAt(i);
                    }
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
                        GameManager.instace.playerList[j].hasCondominium21 = false;
                        continue;
                    }
                    if (GameManager.instace.playerList[j].CondominiumList[i].isSelected == false)
                    {
                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CondominiumList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].CondominiumList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].CondominiumList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].CondominiumList[i].BusinessValue, 0, GameManager.instace.playerList[j].CondominiumList[i].BankLoan);
                        newItem.SetItemSelect(GameManager.instace.playerList[j].CondominiumList[i].isSelected);
                        newItem.transform.SetParent(MarketSellListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _MarketItemList.Add(newItem);
                    }
                    if (GameManager.instace.playerList[j].CondominiumList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[j].CondominiumList.RemoveAt(i);
                    }

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
                        GameManager.instace.playerList[j].hascommercialBuilding = false;
                        continue;
                    }
                    if (GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected == false)
                    {
                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CommercialBuildingList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].CommercialBuildingList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].CommercialBuildingList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].CommercialBuildingList[i].DownPayment, GameManager.instace.playerList[j].CommercialBuildingList[i].count, GameManager.instace.playerList[j].CommercialBuildingList[i].BankLoan);
                        newItem.SetItemSelect(GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected);
                        newItem.transform.SetParent(MarketSellListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _MarketItemList.Add(newItem);
                    }
                    if (GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[j].CommercialBuildingList.RemoveAt(i);
                    }
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
                        GameManager.instace.playerList[j].hasApartment = false;
                        continue;
                    }
                    if (GameManager.instace.playerList[j].ApartmentList[i].isSelected == false)
                    {
                        MarketSellItemUI newItem = Instantiate(MarketSellItemUIPrefab);
                        newItem.MarketSellObjFromYourselfParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].ApartmentList[i].CardName);
                        newItem.SetItemDownPayment(GameManager.instace.playerList[j].ApartmentList[i].DownPayment.ToString());
                        newItem.SetItemIncome(GameManager.instace.playerList[j].ApartmentList[i].CashflowIncome.ToString());
                        newItem.SetPriceInItem(GameManager.instace.playerList[j].ApartmentList[i].BusinessValue, GameManager.instace.playerList[j].ApartmentList[i].count, GameManager.instace.playerList[j].ApartmentList[i].BankLoan);
                        newItem.SetItemSelect(GameManager.instace.playerList[j].ApartmentList[i].isSelected);
                        newItem.transform.SetParent(MarketSellListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _MarketItemList.Add(newItem);
                    }
                    if(GameManager.instace.playerList[j].ApartmentList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[j].ApartmentList.RemoveAt(i);
                    }
                }

            }
        }


    }

    public void UpdateItemSelect()
    {

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
                    if (GameManager.instace.playerList[j].house3s2List[i].isSelected == false)
                    {
                        GameManager.instace.playerList[j].house3s2List[i].isSelected = _MarketItemList[i].isSelected;

                    }
                    if (GameManager.instace.playerList[j].house3s2List[i].isSelected == true)
                    {
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(_MarketItemList[i].itemPrice.text);
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income -= GameManager.instace.playerList[j].house3s2List[i].CashflowIncome;
                        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1, i);
                        photonView.RPC("UpdateIncome", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                        UIController.instance.SellListFromMarketCanvas.SetActive(false);
                        UIController.instance.BlurBg.SetActive(false);
                        

                    }


                }
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
                    if (GameManager.instace.playerList[j].house2s1List[i].isSelected == false)
                    {
                        GameManager.instace.playerList[j].house2s1List[i].isSelected = _MarketItemList[i + GameManager.instace.playerList[j].house3s2List.Count].isSelected;

                    }
                    if (GameManager.instace.playerList[j].house2s1List[i].isSelected == true)
                    {
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(_MarketItemList[i].itemPrice.text);
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income -= GameManager.instace.playerList[j].house2s1List[i].CashflowIncome;
                        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1, i);
                        photonView.RPC("UpdateIncome", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                        UIController.instance.SellListFromMarketCanvas.SetActive(false);
                        UIController.instance.BlurBg.SetActive(false);
                        

                    }

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
                    if (GameManager.instace.playerList[j].CondominiumList[i].isSelected == false)
                    {
                        GameManager.instace.playerList[j].CondominiumList[i].isSelected = _MarketItemList[i + GameManager.instace.playerList[j].house3s2List.Count + GameManager.instace.playerList[j].house2s1List.Count].isSelected;

                    }
                    if (GameManager.instace.playerList[j].CondominiumList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(_MarketItemList[i].itemPrice.text);
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income -= GameManager.instace.playerList[j].CondominiumList[i].CashflowIncome;
                        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1, i);
                        photonView.RPC("UpdateIncome", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                        UIController.instance.SellListFromMarketCanvas.SetActive(false);
                        UIController.instance.BlurBg.SetActive(false);
                        

                    }
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
                    if (GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected == false)
                    {
                        GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected = _MarketItemList[i + GameManager.instace.playerList[j].house3s2List.Count + GameManager.instace.playerList[j].house2s1List.Count + GameManager.instace.playerList[j].CondominiumList.Count].isSelected;

                    }
                    if (GameManager.instace.playerList[j].CommercialBuildingList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(_MarketItemList[i].itemPrice.text);
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income -= GameManager.instace.playerList[j].CommercialBuildingList[i].CashflowIncome;
                        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1, i);
                        photonView.RPC("UpdateIncome", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                        UIController.instance.SellListFromMarketCanvas.SetActive(false);
                        UIController.instance.BlurBg.SetActive(false);
                        

                    }
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
                    if (GameManager.instace.playerList[j].ApartmentList[i].isSelected == false)
                    {
                        GameManager.instace.playerList[j].ApartmentList[i].isSelected = _MarketItemList[i + GameManager.instace.playerList[j].house3s2List.Count + GameManager.instace.playerList[j].house2s1List.Count + GameManager.instace.playerList[j].CondominiumList.Count + GameManager.instace.playerList[j].CommercialBuildingList.Count].isSelected;
                        
                    }
                    if (GameManager.instace.playerList[j].ApartmentList[i].isSelected == true)
                    {
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(_MarketItemList[i].itemPrice.text);
                        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income -= GameManager.instace.playerList[j].ApartmentList[i].CashflowIncome;
                        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1,i);
                        photonView.RPC("UpdateIncome", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income, PhotonNetwork.LocalPlayer.ActorNumber - 1); 
                        UIController.instance.SellListFromMarketCanvas.SetActive(false);
                        UIController.instance.BlurBg.SetActive(false);
                        

                    }
                }

            }
        }


    }

    [PunRPC]
    void UpdateMoney(int money, int x,int i)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "+ " + "Sell " + _MarketItemList[i].itemName.text;
        myNote.price = Int32.Parse(_MarketItemList[i].itemPrice.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= " ;
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);
    }

    [PunRPC]
    void UpdateIncome(int income,int x)
    {
        GameManager.instace.playerList[x].income = income;
    }

    [PunRPC]
    void whoCanSell()
    {
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].GoldCoins == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGoldCoins == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house3s2 == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome32 == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].house2s1 == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome21 == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Condominium == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasCondominium21 == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].CommercialBuilding == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hascommercialBuilding == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Apartment == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasApartment == true)
        {
            UIController.instance.MarketSellButton.SetActive(true);
        }
    }
}

