using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class Network : MonoBehaviourPunCallbacks
{
    public TMP_Text roomName;

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
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {PhotonNetwork.LocalPlayer.ActorNumber} enter room");
    }
    #endregion


}