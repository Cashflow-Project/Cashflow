using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
public class ListKeep : MonoBehaviourPunCallbacks
{
    public static ListKeep instance;

    [SerializeField] private GameObject KeepListWindow;
    [SerializeField] private ItemKeep ItemKeepPrefab;
    [SerializeField] private ItemLine ItemLinePrefab;
    [SerializeField] private Transform KeepListParent; 
    public List<ItemKeep> _KeepItemList = new List<ItemKeep>();
    public List<ItemLine> _LineItemList = new List<ItemLine>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeepItemList();
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

    public void UpdateKeepItemList()
    {
        for (int i = 0; i < _KeepItemList.Count; i++)
        {
            Destroy(_KeepItemList[i].gameObject);
        }
        _KeepItemList.Clear();

            //generate a new list with update info
            for (int i = 0; i < GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].Keep.Count; i++)
            {

                //skip empty 
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].Keep.Count == 0)
                {
                    continue;
                }

                ItemKeep newItem = Instantiate(ItemKeepPrefab);
                newItem.itemKeepParent = this;
                newItem.SetItemName(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].Keep[i].CardName);
                newItem.SetPriceInItem((GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].Keep[i].price).ToString());
                newItem.transform.SetParent(KeepListParent);
                ScaleObjectWithScreenSize(newItem.gameObject);
                _KeepItemList.Add(newItem);

            }
        }
    
}
