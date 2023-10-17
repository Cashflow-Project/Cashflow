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
        _playerItemUIPrefab.statusPlayer.text = "Not Ready";
        UpdatePlayerList();
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePlayerList();
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
    /*
    public void ReadyButton()
    {
        if(_playerItemUIPrefab.statusPlayer.text == "Not Ready")
        {
            readyText.text = "Ready";
            playerReady++;
            _playerItemUIPrefab.SetStatusPlayer("Ready");
        }
        if (_playerItemUIPrefab.statusPlayer.text == "Ready")
        {
            readyText.text = "Not Ready";
            playerReady--;
            _playerItemUIPrefab.SetStatusPlayer("Not Ready");
        }
    }*/
    public void StartGame()
    {
        //player > 1
        if (/*playerReady == _playerList.Count && */_playerList.Count > 0)
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
        /*
        for (int i = 0; i < _playerList.Count; i++)
        {

            PlayerItemUI newPlayerItem = Instantiate(_playerItemUIPrefab);
            newPlayerItem.PlayerParent = this;
            newPlayerItem.SetName(_playerList[i]._playerName.text);
            newPlayerItem.transform.SetParent(_playerListParent);

            _playerList.Add(newPlayerItem);
        }*/
        
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItemUI newPlayerItem = Instantiate(_playerItemUIPrefab);
            newPlayerItem.transform.SetParent(_playerListParent);
            //newPlayerItem.SetName(PhotonNetwork.NickName);
            newPlayerItem.SetName(player.Value.NickName);
            Debug.Log("name player " );
            
            _playerList.Add(newPlayerItem);

        }
    }
}
