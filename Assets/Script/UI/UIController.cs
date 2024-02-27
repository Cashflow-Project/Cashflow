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

    public Canvas left_page1;
    public Canvas left_page2;
    public GameObject left_set1;

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
    public TMP_Text MyIncomeleftText;
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

    public GameObject menubar;
    public GameObject outbar;

    public TMP_Text yourColor;
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

    public Sprite DonateCard;
    public GameObject PayDonateBtn;

    public GameObject BlurBg;
    // Start is called before the first frame update
    void Start()
    {
        roomname.text = PhotonNetwork.CurrentRoom.Name;
        //+"actorNum " + PhotonNetwork.LocalPlayer.ActorNumber.ToString();
        yourColor.text = "�բͧ�س��� "+ GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ColorPlayer.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        MyIncomeleftText.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income).ToString();
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

    public void PayDonate()
    {
        DonateCalculate();
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


    public void MarketPay()
    {
        MarketDeckController.instance.PayCost();
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
        
        if(left_page1.enabled == true || left_page2.enabled == true)
        {
            left_page1.enabled = false;
            left_page2.enabled = false;
            left_set1.SetActive(true);
        }
        
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
        PayDonateBtn.SetActive(on);
    }


    public void menuClick()
    {
        outbar.SetActive(true);

        flimSeemore.SetActive(true);

    }


    public void outOutBar()
    {
        outbar.SetActive(false);

        flimSeemore.SetActive(false);
    }

    public void Quit()
    {
        photonView.RPC("UpdateToAllPlayerState", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        UIController.instance.BlurBg.SetActive(true);
    }


    [PunRPC]
    void UpdateToAllPlayerState(int x)
    {
        GameManager.instace.playerList[x].playerType = GameManager.Entity.PlayerTypes.NO_PLAYER;
        if (IsMyTurn())
        {
            GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            // Find the next player to become the Master Client
            Player[] players = PhotonNetwork.PlayerListOthers;
            if (players.Length > 0)
            {
                // Transfer Master Client role to the next player
                PhotonNetwork.SetMasterClient(players[0]);
            }
        }


    }

    private bool IsMyTurn()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return GameManager.instace.activePlayer == PhotonNetwork.LocalPlayer.ActorNumber - 1;
    }

    public void DonateCalculate()
    {
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].money >= GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve / 10)
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money -= GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve / 10;
            photonView.RPC("UpdateMoneyDonate", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonate = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonateCount = 4;
            photonView.RPC("setDonate", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonate, GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonateCount);
            photonView.RPC("valueUpdate", RpcTarget.All);
            UIController.instance.PayDonateBtn.SetActive(false);
            UIController.instance.cancelButton.SetActive(false);
            //UIController.instance.passButton.SetActive(IsMyTurn());
            photonView.RPC("EndTurnPlayer", RpcTarget.All);
        }
        else
        {
            UIController.instance.LoanCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }

    }
    [PunRPC]
    void UpdateMoneyDonate(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + "Donate";
        myNote.price = GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve / 10;
        GameManager.instace.playerList[GameManager.instace.activePlayer].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
    [PunRPC]
    void valueUpdate()
    {
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank / 10;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].child * GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].perChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].tax + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditcardMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].extraPay + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid;
    }

    [PunRPC]
    void setDonate(bool isDonate, int count)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonate = isDonate;
        GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonateCount = count;
    }

    [PunRPC]
    void EndTurnPlayer()
    {
        UIController.instance.passButton.SetActive(IsMyTurn());

    }
}
