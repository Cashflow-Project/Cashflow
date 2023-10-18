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

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to master server");
        PhotonNetwork.JoinOrCreateRoom("Room" + Random.Range(0,500),new RoomOptions() { MaxPlayers = 6},null );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected");
        
    }
}
