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

    public Sprite DonateCard;
    public GameObject PayDonateBtn;

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


    public void Quit()
    {
        photonView.RPC("UpdateToAllPlayerState", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
                // Leave the current room
         PhotonNetwork.LeaveRoom();
         SceneManager.LoadScene("Lobby");
        /*
        if (PhotonNetwork.IsMasterClient)
        {
            if (GameManager.instace.playerList.Count > 0)
            {
                Player[] players = PhotonNetwork.PlayerListOthers;
                // Transfer Master Client role to the next player
                PhotonNetwork.SetMasterClient(players[0]);
            }
        }*/
        
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

    }
    [PunRPC]
    void valueUpdate()
    {
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve = GameManager.instace.playerList[GameManager.instace.activePlayer].salary + GameManager.instace.playerList[GameManager.instace.activePlayer].income;
        GameManager.instace.playerList[GameManager.instace.activePlayer].InstallmentsBank = GameManager.instace.playerList[GameManager.instace.activePlayer].loanBank / 10;
        GameManager.instace.playerList[GameManager.instace.activePlayer].sumChild = GameManager.instace.playerList[GameManager.instace.activePlayer].child * GameManager.instace.playerList[GameManager.instace.activePlayer].perChild;
        GameManager.instace.playerList[GameManager.instace.activePlayer].paid = GameManager.instace.playerList[GameManager.instace.activePlayer].tax + GameManager.instace.playerList[GameManager.instace.activePlayer].homeMortgage + GameManager.instace.playerList[GameManager.instace.activePlayer].learnMortgage + GameManager.instace.playerList[GameManager.instace.activePlayer].carMortgage + GameManager.instace.playerList[GameManager.instace.activePlayer].creditcardMortgage + GameManager.instace.playerList[GameManager.instace.activePlayer].extraPay + GameManager.instace.playerList[GameManager.instace.activePlayer].InstallmentsBank + GameManager.instace.playerList[GameManager.instace.activePlayer].sumChild;
        GameManager.instace.playerList[GameManager.instace.activePlayer].getmoney = GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve - GameManager.instace.playerList[GameManager.instace.activePlayer].paid;
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
