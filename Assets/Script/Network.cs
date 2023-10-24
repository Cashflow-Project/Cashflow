using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class Network : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    public TMP_Text roomName;

    // Start is called before the first frame update
    void Start()
    {
        //Connect();
        PhotonNetwork.NickName = "Player " + Random.Range(0, 500);
        PhotonNetwork.ConnectUsingSettings();
        //roomName = LobbyManager.instance.room;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    #region PhotonCallBacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to master server");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("joining random room failed");
        PhotonNetwork.CreateRoom(null);
    }

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