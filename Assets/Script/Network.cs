using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Network : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(0, 500);
        PhotonNetwork.ConnectUsingSettings();
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
