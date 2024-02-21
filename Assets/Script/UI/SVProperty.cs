using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SVProperty : MonoBehaviourPunCallbacks
{
    public static SVProperty instance;

    [SerializeField] private GameObject SVPropertyListWindow;
    [SerializeField] private ItemSVProperty ItemSVPropertyPrefab;
    [SerializeField] private Transform SVPropertyListParent;
    public List<ItemSVProperty> _SVPropertyItemList = new List<ItemSVProperty>();

    void Update()
    {

            UpdateSVPropertyItemList();
        
    }




    public void UpdateSVPropertyItemList()
    {
        for (int i = 0; i < _SVPropertyItemList.Count; i++)
        {
            Destroy(_SVPropertyItemList[i].gameObject);
        }
        _SVPropertyItemList.Clear();


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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house3s2List[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[j].house3s2List[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);

                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house2s1List[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[j].house2s1List[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);

                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CondominiumList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[j].CondominiumList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);

                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CommercialBuildingList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[j].CommercialBuildingList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);

                        _SVPropertyItemList.Add(newItem);
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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].ApartmentList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[j].ApartmentList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);

                        _SVPropertyItemList.Add(newItem);

                    }
                    }

                }
            }

        
    }
}
