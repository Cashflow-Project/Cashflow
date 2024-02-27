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

    public void UpdateSVPropertyItemList()
    {
        for (int i = 0; i < _SVPropertyItemList.Count; i++)
        {
            Destroy(_SVPropertyItemList[i].gameObject);
        }
        _SVPropertyItemList.Clear();


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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house3s2List[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].house2s1List[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CondominiumList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVPropertyItemList.Add(newItem);

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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].CommercialBuildingList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVPropertyItemList.Add(newItem);
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
                        ItemSVProperty newItem = Instantiate(ItemSVPropertyPrefab);
                        newItem.itemPropertyParent = this;
                        newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList[i].CardName);
                        newItem.SetCostInItem(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ApartmentList[i].BusinessValue.ToString());
                        newItem.transform.SetParent(SVPropertyListParent);
                        //ScaleObjectWithScreenSize(newItem.gameObject);
                        _SVPropertyItemList.Add(newItem);

                    }
                    }

                
            }

        
    }
}
