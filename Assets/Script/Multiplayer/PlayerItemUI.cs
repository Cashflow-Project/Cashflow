using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
{
    public RoomManager RoomParent;
    [SerializeField] private TMP_Text _roomName;
    public TMP_Text playerInRoom;


    public void SetName(string roomName)
    {
        _roomName.text = roomName;
    }

    public void SetPlayerInRoom(string playerInroom)
    {
        playerInRoom.text = playerInroom;
    }

    
}

