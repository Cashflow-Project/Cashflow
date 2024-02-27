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

    public void UpdateSVDebtItemList()
    {
        for (int i = 0; i < _SVDebtItemList.Count; i++)
        {
            Destroy(_SVDebtItemList[i].gameObject);
        }
        _SVDebtItemList.Clear();


        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome32 == true)
        {

                //generate a new list with update info
                for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List.Count; i++)
                {

                    //skip empty 
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List.Count == 0)
                    {
                        continue;
                    }
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List[i].isSelected == false)
                    {
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVDebtItemList.Add(newItem);
                    }

                }
            
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasHome21 == true)
            {

                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List.Count == 0)
                        {
                            continue;
                        }
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List[i].isSelected == false)
                    {
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVDebtItemList.Add(newItem);

                    }
                    }
                
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasCondominium21 == true)
            {

                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList.Count == 0)
                        {
                            continue;
                        }
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList[i].isSelected == false)
                    {
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVDebtItemList.Add(newItem);
                    }

                    }
                
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hascommercialBuilding == true)
            {

                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList.Count == 0)
                        {
                            continue;
                        }
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList[i].isSelected == false)
                    {
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVDebtItemList.Add(newItem);
                    }

                    }
                
            }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasApartment == true)
            {

                    //generate a new list with update info
                    for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList.Count; i++)
                    {

                        //skip empty 
                        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList.Count == 0)
                        {
                            continue;
                        }
                    if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList[i].isSelected == false)
                    {
                        ItemSVDebt newItem = Instantiate(ItemSVDebtPrefab);
                        newItem.itemDebtParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList[i].CardName);
                        newItem.SetDebtInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList[i].BankLoan.ToString());
                        newItem.transform.SetParent(SVDebtListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVDebtItemList.Add(newItem);

                    }
                    }

                
            }

        
    }
}
