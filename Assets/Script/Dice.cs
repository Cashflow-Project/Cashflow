using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Dice : MonoBehaviourPunCallbacks
{
    public static Dice instace;
    Rigidbody rb;
    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public DiceSide[] dicesides;
    public int diceValue;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void RollDice()
    {
        Reset();
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 5000), Random.Range(0, 5000), Random.Range(0, 5000));
        }
        else if(thrown && hasLanded)
        {
            //reset dice
            Reset();
        }
    }

    void Reset()
    {
        transform.position = initPosition;
        rb.isKinematic = false;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    void Update()
    {
        //photonView.RPC("dice", RpcTarget.All);
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = true;
            rb.isKinematic = true;

            //check value
            SideValueCheck();
        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            //roll dice again
            RollAgain();
        }
    
}

    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 5000), Random.Range(0, 5000), Random.Range(0, 5000));
    }

    void SideValueCheck()
    {
        diceValue = 0;
        foreach(DiceSide side in dicesides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                //send result to gamemanager
                diceValue = 2;
                GameManager.instace.RollDice(diceValue);
            }
        }
    }

    [PunRPC]
    void dice()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = true;
            rb.isKinematic = true;

            //check value
            SideValueCheck();
        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            //roll dice again
            RollAgain();
        }
    }
}
