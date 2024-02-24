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

public class UIController : MonoBehaviourPunCallbacks
{
    public static UIController instance;
    public GameObject lostShow;
    public GameObject lost_SeemoreBtn;
    public GameObject lost_outBtn;
    public GameObject winShow;
    public GameObject win_SeemoreBtn;
    public GameObject win_outBtn;
    public GameObject flimSeemore;


    public GameObject InvestCanvas;
    public GameObject SellListCanvas;
    public GameObject SellListFromMarketCanvas;
    public Text infoText;
    public TMP_Text MyMoneyText;
    public Image cardShow;
    public Text roomname;
    public GameObject LoanCanvas;
    public GameObject investSellCanvas;

    public GameObject GoldCoinsSell;

    public GameObject PayLoanCanvas;
    public GameObject PayHouseDebtCanvas;
    public GameObject PayLearnDebtCanvas;
    public GameObject PayCarDebtCanvas;
    public GameObject PayCreditDebtCanvas;

    

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
    public GameObject MarketSellButton;
    public GameObject MarketPayButton;

    public GameObject BlurBg;
    // Start is called before the first frame update
    void Start()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name +"actorNum " + PhotonNetwork.LocalPlayer.ActorNumber.ToString();
        

    }

    // Update is called once per frame
    void Update()
    {
        MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
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
        
        BlurBg.SetActive(true);

    }

    public void MarketSell()
    {
        SellListFromMarketCanvas.SetActive(true);
        BlurBg.SetActive(true);
    }

    public void CloseSellList()
    {
        SellListCanvas.SetActive(false);
        BlurBg.SetActive(false);

    }

    public void CloseMarketSellList()
    {
        SellListFromMarketCanvas.SetActive(false);
        BlurBg.SetActive(false);
    }

    public void SeeMore()
    {
        UILeftController.instance.page1.enabled = false;
        UILeftController.instance.page2.enabled = false;
        UILeftController.instance.set1.SetActive(true);
        winShow.SetActive(false);
        lostShow.SetActive(false);
        BlurBg.SetActive(false);
        flimSeemore.SetActive(true);
    }

    public void SetAllFalse(bool on)
    {

        cardShow.enabled = on;
        GameManager.instace.diceButton.SetActive(on);
        payButton.SetActive(on);
        SmallPayButton.SetActive(on);
        BigPayButton.SetActive(on);
        cancelButton.SetActive(on);
        drawButton.SetActive(on);
        ChooseBigSmall.SetActive(on);
        SellButton.SetActive(on);

    }


    public void Quit()
    {
        photonView.RPC("UpdateToAllPlayerState", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.Disconnect();
        //PhotonNetwork.LoadLevel("Lobby");
    }

    [PunRPC]
    void UpdateToAllPlayerState(int x)
    {
        GameManager.instace.playerList[x].playerType = GameManager.Entity.PlayerTypes.NO_PLAYER;
        if (IsMyTurn())
        {
            GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        UIController.instance.BlurBg.SetActive(true);
        //PhotonNetwork.LeaveRoom();

    }

    private bool IsMyTurn()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return GameManager.instace.activePlayer == PhotonNetwork.LocalPlayer.ActorNumber - 1;
    }

}
