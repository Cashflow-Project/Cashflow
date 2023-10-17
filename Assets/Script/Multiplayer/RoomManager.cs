using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{


    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private Button leaveRoomBtn;

    [SerializeField] private Button ReadyBtn;
    [SerializeField] private TMP_Text readyText;

    [SerializeField] private Button StartBtn;

    [SerializeField] private int playerReady;
    [SerializeField] private PlayerItemUI _playerItemUIPrefab;
    [SerializeField] private Transform _playerListParent;


    private List<PlayerItemUI> _playerList = new List<PlayerItemUI>();
    // Start is called before the first frame update

    void Start()
    {
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        //_playerItemUIPrefab._playerName.text = PhotonNetwork.NickName;
        _playerItemUIPrefab.statusPlayer.text = "Not Ready";
       
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerList();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        
        PhotonNetwork.LoadLevel("Lobby");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room " + PhotonNetwork.CurrentRoom.Name);
    }
    public void ReadyButton()
    {
        if(_playerItemUIPrefab.statusPlayer.text == "Not Ready")
        {
            _playerItemUIPrefab.statusPlayer.text = "Ready";
        }
        if (_playerItemUIPrefab.statusPlayer.text == "Ready")
        {
            _playerItemUIPrefab.statusPlayer.text = "Not Ready";
        }
    }
    public void StartGame()
    {
        for (int i = 0; i < _playerList.Count; i++)
        {
            if (_playerList[i].statusPlayer.text == "Ready")
            {
                playerReady++;
            }
        }
        if (playerReady == _playerList.Count && _playerList.Count > 1)
        {
            PhotonNetwork.LoadLevel("Game");
        }
        
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        //clear current player list
        for (int i = 0; i < _playerList.Count; i++)
        {
            Destroy(_playerList[i].gameObject);
        }

        _playerList.Clear();

        if(PhotonNetwork.CurrentRoom == null) { return; }
        //generate new player list
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItemUI newPlayerItem = Instantiate(_playerItemUIPrefab);

            newPlayerItem.transform.SetParent(_playerListParent);
            newPlayerItem.SetName(player.Value.NickName);

            _playerList.Add(newPlayerItem);

        }
    }
}
