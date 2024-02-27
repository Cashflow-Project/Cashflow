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
    public List<SellItemUI> _investItemList = new List<SellItemUI>();

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

    public void UpdateON2UItemList()
    {
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }
        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {


                //skip empty 
                if (GameManager.instace.playerList[j].ON2UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].ON2UList[0].CardName);
                newItem.SetCountInItem((GameManager.instace.playerList[j].ON2UList[0].countShare).ToString());
               newItem.transform.SetParent(sellListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _investItemList.Add(newItem);
            
        }
    }

    public void UpdateMYT4UItemList()
    {
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }
        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info


                //skip empty 
                if (GameManager.instace.playerList[j].MYT4UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].MYT4UList[0].CardName);
                newItem.SetCountInItem((GameManager.instace.playerList[j].MYT4UList[0].countShare).ToString());
                newItem.transform.SetParent(sellListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _investItemList.Add(newItem);
            
        }
    }

    public void UpdateGRO4USItemList()
    {
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }
        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info


                //skip empty 
                if (GameManager.instace.playerList[j].GRO4USList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].GRO4USList[0].CardName);
                newItem.SetCountInItem((GameManager.instace.playerList[j].GRO4USList[0].countShare).ToString());
                newItem.transform.SetParent(sellListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _investItemList.Add(newItem);
            
        }
    }

    public void UpdateOK4UItemList()
    {
        for (int i = 0; i < _investItemList.Count; i++)
        {
            Destroy(_investItemList[i].gameObject);
        }
        _investItemList.Clear();

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            //generate a new list with update info


                //skip empty 
                if (GameManager.instace.playerList[j].OK4UList.Count == 0)
                {
                    continue;
                }

                SellItemUI newItem = Instantiate(SellItemUIPrefab);
                newItem.sellObjFromYourselfParent = this;
                newItem.SetItemName(GameManager.instace.playerList[j].OK4UList[0].CardName);
                newItem.SetCountInItem((GameManager.instace.playerList[j].OK4UList[0].countShare).ToString());
                newItem.transform.SetParent(sellListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _investItemList.Add(newItem);
            
        }
    }


    [PunRPC]
    void UpdateEachKeepForInvest(int x)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[x].ON2UList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].ON2UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].ON2UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasON2U = false;


                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (GameManager.instace.playerList[x].MYT4UList[0].countShare > 0)
            {

                if (GameManager.instace.playerList[x].MYT4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].MYT4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasMYT4U = false;

                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[x].GRO4USList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].GRO4USList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].GRO4USList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasGRO4US = false;

                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[x].OK4UList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].OK4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasOK4U = false;

                }
            }

        }
    }
}
