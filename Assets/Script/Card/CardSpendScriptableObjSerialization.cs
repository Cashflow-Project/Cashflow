using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class CardSpendScriptableObjSerialization
{
    public static byte[] Serialize(object customObject)
    {
        //Debug.Log("in serialize");
        var card = (CardSpendScriptableObj)customObject;
        var byteList = new List<byte>();

        // Serialize cardName
        var nameBytes = Encoding.UTF8.GetBytes(card.cardName);
        byteList.AddRange(BitConverter.GetBytes(nameBytes.Length));
        byteList.AddRange(nameBytes);

        // Serialize payCost
        byteList.AddRange(BitConverter.GetBytes(card.payCost));

        // Serialize hasChildsOrNot
        byteList.Add((byte)(card.hasChildsOrNot ? 1 : 0));

        return byteList.ToArray();
    }

    public static object Deserialize(byte[] data)
    {
        var card = ScriptableObject.CreateInstance<CardSpendScriptableObj>();
        var byteList = new List<byte>(data);
        int offset = 0;

        // Deserialize cardName
        int nameLength = BitConverter.ToInt32(data, offset);
        offset += 4;
        card.cardName = Encoding.UTF8.GetString(data, offset, nameLength);
        offset += nameLength;

        // Deserialize payCost
        card.payCost = BitConverter.ToInt32(data, offset);
        offset += 4;

        // Deserialize hasChildsOrNot
        card.hasChildsOrNot = byteList[offset] == 1;

        return card;
    }
}



