using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class Network : MonoBehaviourPunCallbacks
{
    

    // Start is called before the first frame update
    void Start()
    {
        Connect();
        
    }

    public void Connect()
    {
     PhotonNetwork.ConnectUsingSettings();
    }

    #region PhotonCallBacks

    public override void OnJoinedRoom()
    {
        Debug.Log($"Player {PhotonNetwork.LocalPlayer.ActorNumber} join the room");
        object activePlayerObj;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("activePlayer", out activePlayerObj))
        {
            GameManager.instace.activePlayer = (int)activePlayerObj;
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {PhotonNetwork.LocalPlayer.ActorNumber} enter room");
    }
    #endregion


}