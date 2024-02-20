using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;
    //public bool head;
    [SerializeField] private TMP_InputField _roomInput;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private RoomItemUI roomItemUIPrefab;
    [SerializeField] private Transform roomListParent;
    [SerializeField] private int PlayerInRoom;

    [SerializeField] private TMP_Text status;
    [SerializeField] private Button leaveRoomBtn;
    [SerializeField] private Button startGameBtn;

    [SerializeField] private TMP_Text _currentLocation;

    [SerializeField] private PlayerItemUI _playerItemUIPrefab;
    [SerializeField] private Transform _playerListParent;

    [SerializeField] private GameObject _roomListWindow;

    [SerializeField] private GameObject _playerListWindow;
    [SerializeField] private GameObject _createRoomWindow;

    public List<RoomItemUI> _roomList = new List<RoomItemUI>();
    public List<PlayerItemUI> _playerList = new List<PlayerItemUI>();


    //public GameObject createObj;

    private void Start()
    {
        Initialize();
        Connect();
    }

   
    #region PhotonCallbacks
    public override void OnConnectedToMaster()
    {
        status.text = "Connect to master server";
        Debug.Log("Connect to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(status == null) { return; }
        status.text = "Disconnected";
        Debug.Log("Disconnected");
    }

    public override void OnJoinedLobby()
    {
        _currentLocation.text = "Room";
        Debug.Log("Join Lobby");
    }

    public override void OnJoinedRoom()
    {
        leaveRoomBtn.interactable = true;
        status.text = "Joined "+ PhotonNetwork.CurrentRoom.Name;
        _currentLocation.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("Join Room " + PhotonNetwork.CurrentRoom.Name);
        //PhotonNetwork.LoadLevel("Room");
        if (PhotonNetwork.IsMasterClient)
        {
            startGameBtn.interactable = true;
        }
        ShowWindow(false);
        PlayerInRoom++;
        UpdatePlayerList();
    }

    public override void OnLeftRoom()
    {
        if (status != null)
        {
            status.text = "ON LOBBY ";
        }
        if(_currentLocation != null) { 
            _currentLocation.text = "Room";
            //Debug.Log("Left Room " + PhotonNetwork.CurrentRoom.Name);
        }
        leaveRoomBtn.interactable = false;
        startGameBtn.interactable = false;
        ShowWindow(true);
        PlayerInRoom--;
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdatePlayerList();
    }

    #endregion

    private void Initialize()
    {
        leaveRoomBtn.interactable = false;
        startGameBtn.interactable = false;
        
    }
    private void Connect()
    {
        PhotonNetwork.NickName = "Player " + Random.Range(0,500);
        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void UpdateRoomList(List<RoomInfo> roomList)
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

    private void UpdatePlayerList()
    {
        //clear current player list
        for (int i = 0; i < _playerList.Count; i++)
        {
            Destroy(_playerList[i].gameObject);
        }

        _playerList.Clear();

        if (PhotonNetwork.CurrentRoom == null) { return; }

        //generate new player list
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItemUI newPlayerItem = Instantiate(_playerItemUIPrefab);
            newPlayerItem.transform.SetParent(_playerListParent);
            newPlayerItem.SetName(player.Value.NickName);
            //Debug.Log("name player ");

            _playerList.Add(newPlayerItem);
            
        }
    }
    private void ShowWindow(bool isRoomList)
    {
        _roomListWindow.SetActive(isRoomList);
        _playerListWindow.SetActive(!isRoomList);
        _createRoomWindow.SetActive(isRoomList);
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

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        
    }



    public void BackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Main Menu");
    }

    public void OnStartGamePressed()
    {
        PhotonNetwork.LoadLevel("Game");

    }
}
