using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    
    public Image cardShow;
    public Text roomname;

    private void Awake()
    {
        instance = this;
    }

    public GameObject payButton;
    public GameObject loanButton;
    public GameObject cancelButton;
    public GameObject passButton;
    public GameObject drawButton;
    // Start is called before the first frame update
    void Start()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name +"actorNum " + PhotonNetwork.LocalPlayer.ActorNumber.ToString();
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DrawCard()
    {
        SpendDeckController.instance.DrawCardToHand();
    }

    public void PayCost()
    {
        SpendDeckController.instance.PayCost();
    }
    public void Cancel()
    {

    }
}
