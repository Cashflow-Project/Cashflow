using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SVDebt : MonoBehaviourPunCallbacks
{
    public static SVDebt instance;

    [SerializeField] private GameObject SVDebtListWindow;
    [SerializeField] private ItemSVDebt ItemSVDebtPrefab;
    [SerializeField] private Transform SVDebtListParent;
    public List<ItemSVDebt> _SVDebtItemList = new List<ItemSVDebt>();

    void Update()
    {

            UpdateSVDebtItemList();
        
    }

    public void UpdateSVDebtItemList()
    {
        for (int i = 0; i < _SVDebtItemList.Count; i++)
        {
            Destroy(_SVDebtItemList[i].gameObject);
        }
        _SVDebtItemList.Clear();


        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome32 == true)
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
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house3s2List[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[j].house3s2List[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);

                        _SVDebtItemList.Add(newItem);
                    }

                }
            }
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome21 == true)
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
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house2s1List[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[j].house2s1List[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);

                        _SVDebtItemList.Add(newItem);

                    }
                    }
                }
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasCondominium21 == true)
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
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CondominiumList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[j].CondominiumList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);

                        _SVDebtItemList.Add(newItem);
                    }

                    }
                }
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hascommercialBuilding == true)
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
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CommercialBuildingList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[j].CommercialBuildingList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);

                        _SVDebtItemList.Add(newItem);
                    }

                    }
                }
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasApartment == true)
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
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].ApartmentList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[j].ApartmentList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);

                        _SVDebtItemList.Add(newItem);

                    }
                    }

                }
            }

        
    }
}
