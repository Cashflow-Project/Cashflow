using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SellObjFromYourself : MonoBehaviourPunCallbacks
{
    public static SellObjFromYourself instance;

    [SerializeField] private GameObject SellListWindow;
    [SerializeField] private SellItemUI SellItemUIPrefab;
    [SerializeField] private Transform sellListParent;
    private List<SellItemUI> _investItemList = new List<SellItemUI>();

    // Start is called before the first frame update
    void Start()
    {
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U == true)
        {
            UpdateON2UItemList();
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U == true)
        {
            UpdateMYT4UItemList();
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US == true)
        {
            UpdateGRO4USItemList();
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U == true)
        {
            UpdateOK4UItemList(); 
        }
        //UpdateInvestItemList();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void UpdateInvestItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for(int j = 0;j < GameManager.instace.playerList.Count; j++)
        {
            
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].InvestList.Count; i++)
            {
               
                //skip empty 
                if (GameManager.instace.playerList[j].InvestList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].InvestList[i].CardName);
                newItem.SetPriceInItem(GameManager.instace.playerList[j].InvestList[i].sumValue.ToString());
                newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
       
    }

    private void UpdateON2UItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].ON2UList.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[j].ON2UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].ON2UList[i].CardName);
                newItem.SetPriceInItem((SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value * GameManager.instace.playerList[j].ON2UList[i].countShare).ToString());
               newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
    }

    private void UpdateMYT4UItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].MYT4UList.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[j].MYT4UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].MYT4UList[i].CardName);
                newItem.SetPriceInItem((SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value * GameManager.instace.playerList[j].MYT4UList[i].countShare).ToString());
                newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
    }

    private void UpdateGRO4USItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].GRO4USList.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[j].GRO4USList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].GRO4USList[i].CardName);
                newItem.SetPriceInItem((SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value * GameManager.instace.playerList[j].GRO4USList[i].countShare).ToString());
                newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
    }

    private void UpdateOK4UItemList()
    {
        //clear the current list of item
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }

        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[j].OK4UList.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[j].OK4UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].OK4UList[i].CardName);
                newItem.SetPriceInItem((SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value * GameManager.instace.playerList[j].OK4UList[i].countShare).ToString());
                newItem.transform.SetParent(sellListParent);

                _investItemList.Add(newItem);
            }
        }
    }
}
