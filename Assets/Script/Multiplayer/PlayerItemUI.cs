using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
{
    public RoomManager PlayerParent;
    [SerializeField] private TMP_Text _playerName;
    public TMP_Text statusPlayer;
    

    public void SetName(string playerName)
    {
        _playerName.text = playerName;
    }

    public void SetPlayerInRoom(string playerStatus)
    {
        statusPlayer.text = playerStatus;
    }

    
}

