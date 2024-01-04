using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Text infoText;
    public Image cardShow;
    public Text roomname;

    private void Awake()
    {
        instance = this;
        infoText.text = "";
    }

    public GameObject payButton;
    public GameObject loanButton;
    public GameObject cancelButton;
    public GameObject passButton;
    public GameObject drawButton;
    public GameObject ChooseBigSmall;
    // Start is called before the first frame update
    void Start()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name +"actorNum " + PhotonNetwork.LocalPlayer.ActorNumber.ToString();
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showMessage(string text)
    {
        infoText.text = text;
    }

    internal void showMessage(object p)
    {
        throw new NotImplementedException();
    }

    public void DrawCard()
    {
        SpendDeckController.instance.DrawCardToHand();
    }

    public void DrawBigCard()
    {
        BigDealDeckController.instance.DrawCardToHand();
    }

    public void DrawSmallCard()
    {
        SmallDealDeckController.instance.DrawCardToHand();
    }

    public void PayCost()
    {
        SpendDeckController.instance.PayCost();
    }
    public void Cancel()
    {

    }

    public void Loan()
    {

    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    [PunRPC]
    void AddToUseCard()
    {


    }
}
