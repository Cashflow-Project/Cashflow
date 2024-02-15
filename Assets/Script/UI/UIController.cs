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

    public GameObject InvestCanvas;
    public GameObject SellListCanvas;
    public GameObject SellListFromMarketCanvas;
    public Text infoText;
    public Image cardShow;
    public Text roomname;
    public GameObject LoanCanvas;
    public GameObject investSellCanvas;
    private void Awake()
    {
        instance = this;
        infoText.text = "";
    }

    public GameObject payButton;
    public GameObject SmallPayButton;
    public GameObject BigPayButton;
    public GameObject cancelButton;
    public GameObject passButton;
    public GameObject drawButton;
    public GameObject ChooseBigSmall;
    public GameObject SellButton;
    public GameObject MarketDrawButton;
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
        GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn = false;
        drawButton.SetActive(false);
    }

    public void DrawMarketCard()
    {
        MarketDeckController.instance.DrawCardToHand();
        MarketDrawButton.SetActive(false);
    }

    public void DrawBigCard()
    {
        BigDealDeckController.instance.DrawCardToHand();
        ChooseBigSmall.SetActive(false);

    }

    public void DrawSmallCard()
    {
        SmallDealDeckController.instance.DrawCardToHand();
        ChooseBigSmall.SetActive(false);
    }

    public void PayCost()
    {
        SpendDeckController.instance.PayCost();
    }

    public void SmallPayCost()
    {
        SmallDealDeckController.instance.BuyCost();
    }

    public void BigPayCost()
    {
        BigDealDeckController.instance.BuyCost();
    }
    public void Cancel()
    {
        SetAllFalse(false);

        passButton.SetActive(true);
    }

    public void Sell()
    {
        SellListCanvas.SetActive(true);
        payButton.SetActive(false);
        SmallPayButton.SetActive(false);
        BigPayButton.SetActive(false);
        //cancelButton.SetActive(false);
        drawButton.SetActive(false);
        ChooseBigSmall.SetActive(false);
        SellButton.SetActive(false);
    }

    public void MarketSell()
    {
        SellListFromMarketCanvas.SetActive(true);
        payButton.SetActive(false);
        SmallPayButton.SetActive(false);
        BigPayButton.SetActive(false);
        cancelButton.SetActive(false);
        drawButton.SetActive(false);
        ChooseBigSmall.SetActive(false);
        SellButton.SetActive(false);
    }

    public void CloseSellList()
    {
        SellListCanvas.SetActive(false);
    }
    public void SetAllFalse(bool on)
    {

        cardShow.enabled = on;
        payButton.SetActive(on);
        SmallPayButton.SetActive(on);
        BigPayButton.SetActive(on);
        cancelButton.SetActive(on);
        drawButton.SetActive(on);
        ChooseBigSmall.SetActive(on);
        SellButton.SetActive(on);

    }

    public void Loan()
    {

    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

}
