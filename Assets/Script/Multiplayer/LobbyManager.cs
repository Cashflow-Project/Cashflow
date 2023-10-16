using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;

    [SerializeField] private TMP_InputField _roomInput;
    [SerializeField] private RoomItemUI roomItemUIPrefab;
    [SerializeField] private Transform roomListParent;
    [SerializeField] private int PlayerInRoom;

    private List<RoomItemUI> _roomList = new List<RoomItemUI>();


    public Canvas createRoom;
    public Canvas ListRoom;
    //public GameObject createObj;

    private void Start()
    {
        Connect();
    }

    #region PhotonCallBacks
    private void Connect()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Join Lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room " + PhotonNetwork.CurrentRoom.Name);
    }



    #endregion
   

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        //clear the current list of room
        for(int i = 0;i < _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }

        _roomList.Clear();
        //generate a new list with update info
        for(int i = 0; i < roomList.Count; i++)
        {
            //skip empty room
            if(roomList[i].PlayerCount == 0)
            {
                continue;
            }

            RoomItemUI newRoomItem = Instantiate(roomItemUIPrefab);
            newRoomItem.LobbyParent = this;
            newRoomItem.SetName(roomList[i].Name);
            newRoomItem.SetPlayerInRoom(roomList[i].PlayerCount.ToString() + " /6");
            //newRoomItem.playerInRoom.text =  roomList[i].PlayerCount.ToString() + " /6";
            newRoomItem.transform.SetParent(roomListParent);

            _roomList.Add(newRoomItem);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomInput.text) == false)
        {
            PhotonNetwork.CreateRoom(_roomInput.text, new RoomOptions() { MaxPlayers = 6 }, null);
        }
        
    }

    public void openCreateRoom()
    {
        createRoom.enabled = true;
    }

    public void closeCreateRoom()
    {
        createRoom.enabled = false;
    }

    public void openListRoom()
    {
        ListRoom.enabled = true;
    }

    public void closeListRoom()
    {
        ListRoom.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
