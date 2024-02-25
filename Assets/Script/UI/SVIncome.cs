using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class SVIncome : MonoBehaviourPunCallbacks
{
    public static SVIncome instance;

    [SerializeField] private GameObject SVIncomeListWindow;
    [SerializeField] private ItemSVIncome ItemSVIncomePrefab;
    [SerializeField] private Transform SVIncomeListParent;
    public List<ItemSVIncome> _SVIncomeItemList = new List<ItemSVIncome>();

    void Update()
    {

            UpdateSVIncomeItemList();
        
        
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

    public void UpdateSVIncomeItemList()
    {
        for (int i = 0; i < _SVIncomeItemList.Count; i++)
        {
            Destroy(_SVIncomeItemList[i].gameObject);
        }
        _SVIncomeItemList.Clear();


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
                        ItemSVIncome newItem = Instantiate(ItemSVIncomePrefab);
                        newItem.itemIncomeParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house3s2List[i].CardName);
                        newItem.SetIncomeInItem(GameManager.instace.playerList[j].house3s2List[i].CashflowIncome.ToString());
                        newItem.transform.SetParent(SVIncomeListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVIncomeItemList.Add(newItem);

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
                        ItemSVIncome newItem = Instantiate(ItemSVIncomePrefab);
                        newItem.itemIncomeParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].house2s1List[i].CardName);
                        newItem.SetIncomeInItem(GameManager.instace.playerList[j].house2s1List[i].CashflowIncome.ToString());
                        newItem.transform.SetParent(SVIncomeListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVIncomeItemList.Add(newItem);

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
                        ItemSVIncome newItem = Instantiate(ItemSVIncomePrefab);
                        newItem.itemIncomeParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CondominiumList[i].CardName);
                        newItem.SetIncomeInItem(GameManager.instace.playerList[j].CondominiumList[i].CashflowIncome.ToString());
                        newItem.transform.SetParent(SVIncomeListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVIncomeItemList.Add(newItem);
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
                        ItemSVIncome newItem = Instantiate(ItemSVIncomePrefab);
                        newItem.itemIncomeParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].CommercialBuildingList[i].CardName);
                        newItem.SetIncomeInItem(GameManager.instace.playerList[j].CommercialBuildingList[i].CashflowIncome.ToString());
                        newItem.transform.SetParent(SVIncomeListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVIncomeItemList.Add(newItem);
                    }

                    }
                }
            }
            if ( GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasApartment == true)
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
                        ItemSVIncome newItem = Instantiate(ItemSVIncomePrefab);
                        newItem.itemIncomeParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[j].ApartmentList[i].CardName);
                        newItem.SetIncomeInItem(GameManager.instace.playerList[j].ApartmentList[i].CashflowIncome.ToString());
                        newItem.transform.SetParent(SVIncomeListParent);
                        ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVIncomeItemList.Add(newItem);

                    }
                    }

                }
            }

        
    }
}
