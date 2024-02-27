using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class SmallDealDeckController : MonoBehaviourPunCallbacks
{
    public static SmallDealDeckController instance;
    public int cardcount=0; 

    private void Awake()
    {
        instance = this;

    }

    public List<CardSmallChangeScriptableObj> deckToUse = new List<CardSmallChangeScriptableObj>();

    public List<CardSmallChangeScriptableObj> activeCards = new List<CardSmallChangeScriptableObj>();

    public List<CardSmallChangeScriptableObj> usedCards = new List<CardSmallChangeScriptableObj>();

    public List<CardSmallChangeScriptableObj> tempDeck = new List<CardSmallChangeScriptableObj>();

    public SmallDealCard cardsToSpawns;

    public int FordrawCard = 1;
    public float waitBetweenDrawingCard = .3f;


    // Start is called before the first frame update
    void Start()
    {
        //photonView.RPC("setUpdeckToEveryone", RpcTarget.All);
        SetUpDeck();
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.passButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void SetUpDeck()
    {

        


        activeCards.Clear();
        tempDeck.AddRange(deckToUse);
        if (PhotonNetwork.IsMasterClient)
        {
            int iterations = 0;
            while (tempDeck.Count > 0 && iterations < 500)
            {
                int selected = Random.Range(0, tempDeck.Count);
                photonView.RPC("CreateSmallDealDeckStart", RpcTarget.All,selected);

                iterations++;
            }

        }

    }

    public void DrawCardToHand()
    {


        SmallDealCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardSCSO = activeCards[0];

        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(true);
        UIController.instance.cancelButton.SetActive(true);

        //UIController.instance.cardShow.enabled = true;
        Debug.Log("check card sprite" + activeCards[0].cardSprite);
        //ShowSmallDealController.instance.AddCardToShow(newCard);
        photonView.RPC("ShowCardToAllPlayerRPC", RpcTarget.All);

        //UIController.instance.cardShow.sprite = activeCards[0].cardSprite;

        //do somthing for invest on2u ok4u myt4u grou4us to other people

        
        
        photonView.RPC("AddToUseCard", RpcTarget.All);

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true || SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true || SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true || SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {
            photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        }
        photonView.RPC("whoCanSell", RpcTarget.All);
        Destroy(newCard.gameObject, 1);
    }

    public void BuyCost()
    {
        if(usedCards[cardcount - 1].ON2U == true || usedCards[cardcount - 1].MYT4U == true || usedCards[cardcount - 1].GRO4US == true || usedCards[cardcount - 1].OK4U == true) 
        {
            
                
                UIController.instance.InvestCanvas.SetActive(true);
                UIController.instance.BlurBg.SetActive(true);

        }
        else if (GameManager.instace.playerList[GameManager.instace.activePlayer].money >= usedCards[cardcount - 1].value && usedCards[cardcount - 1].GoldCoins == true)
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].value;
            GameManager.instace.playerList[GameManager.instace.activePlayer].GoldCoins = GameManager.instace.playerList[GameManager.instace.activePlayer].GoldCoins + usedCards[cardcount - 1].count;
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            photonView.RPC("UpdateGoldcoins", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].GoldCoins, GameManager.instace.activePlayer);
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            UIController.instance.payButton.SetActive(false);
            UIController.instance.BigPayButton.SetActive(false);
            UIController.instance.SellButton.SetActive(false);
            UIController.instance.cancelButton.SetActive(false);
            UIController.instance.SmallPayButton.SetActive(false);
            UIController.instance.passButton.SetActive(true);
        }
        else if(GameManager.instace.playerList[GameManager.instace.activePlayer].money >= usedCards[cardcount - 1].DownPayment &&(usedCards[cardcount - 1].house2s1 || usedCards[cardcount - 1].Condominium))
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].DownPayment;
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            photonView.RPC("UpdateKeepForDeal", RpcTarget.All);
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            UIController.instance.payButton.SetActive(false);
            UIController.instance.BigPayButton.SetActive(false);
            UIController.instance.SellButton.SetActive(false);
            UIController.instance.cancelButton.SetActive(false);
            UIController.instance.SmallPayButton.SetActive(false);
            UIController.instance.passButton.SetActive(true);
        }
        else
        {
            UIController.instance.LoanCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }
            
            
        
        
        

        photonView.RPC("valueUpdate", RpcTarget.All);


    }

    

    [PunRPC]
    void CreateSmallDealDeckStart(int selected)
    {
        activeCards.Add(tempDeck[selected]);
        tempDeck.RemoveAt(selected);

    }

    [PunRPC]
    void setUpdeckToEveryone()
    {
        SetUpDeck();

    }

    [PunRPC]
    void AddToUseCard()
    {
        Debug.Log("this is activetivecard0 " + activeCards[0]);
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);

        
    }

    [PunRPC]
    void UpdateMoney(int money,int x)
    {
        GameManager.instace.playerList[x].money = money;
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + usedCards[cardcount - 1].cardName;
        myNote.price = usedCards[cardcount - 1].DownPayment;
        if(usedCards[cardcount - 1].GoldCoins == true)
        {
            myNote.price = usedCards[cardcount - 1].value;
        }
        GameManager.instace.playerList[x].Keep.Add(myNote);


        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
    [PunRPC]
    void UpdateGoldcoins(int goldcoins, int x)
    {
        GameManager.instace.playerList[x].GoldCoins = goldcoins;
        GameManager.instace.playerList[GameManager.instace.activePlayer].hasGoldCoins = true;

    }
    [PunRPC]
    void UpdateKeepForDeal()
    {
        //Deal collect
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].house2s1 == true)
        {
            GameManager.house2s1Keep myHouse2n1 = new GameManager.house2s1Keep();
            myHouse2n1.CardName = usedCards[cardcount - 1].cardName;
            myHouse2n1.BusinessValue = usedCards[cardcount - 1].BusinessValue;
            myHouse2n1.DownPayment = usedCards[cardcount - 1].DownPayment;
            myHouse2n1.BankLoan = usedCards[cardcount - 1].BankLoan;
            myHouse2n1.CashflowIncome = usedCards[cardcount - 1].CashflowIncome;
            GameManager.instace.playerList[GameManager.instace.activePlayer].income += usedCards[cardcount - 1].CashflowIncome;
            GameManager.instace.playerList[GameManager.instace.activePlayer].getmoney = GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve - GameManager.instace.playerList[GameManager.instace.activePlayer].paid;
            GameManager.instace.playerList[GameManager.instace.activePlayer].house2s1List.Add(myHouse2n1);
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasHome21 = true;
        }

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].Condominium == true)
        {
            GameManager.CondominiumKeep myCondominium = new GameManager.CondominiumKeep();
            myCondominium.CardName = usedCards[cardcount - 1].cardName;
            myCondominium.BusinessValue = usedCards[cardcount - 1].BusinessValue;
            myCondominium.DownPayment = usedCards[cardcount - 1].DownPayment;
            myCondominium.BankLoan = usedCards[cardcount - 1].BankLoan;
            myCondominium.CashflowIncome = usedCards[cardcount - 1].CashflowIncome;
            myCondominium.count = usedCards[cardcount - 1].count;
            GameManager.instace.playerList[GameManager.instace.activePlayer].income += usedCards[cardcount - 1].CashflowIncome;
            GameManager.instace.playerList[GameManager.instace.activePlayer].getmoney = GameManager.instace.playerList[GameManager.instace.activePlayer].allRecieve - GameManager.instace.playerList[GameManager.instace.activePlayer].paid;
            GameManager.instace.playerList[GameManager.instace.activePlayer].CondominiumList.Add(myCondominium);
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasCondominium21 = true;
        }

        
    }

    
    [PunRPC]
    void CalculateSmallDealRPC(int money)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].DownPayment;
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = money;
    }

    [PunRPC]
    void ShowCardToAllPlayerRPC()
    {
        UIController.instance.cardShow.enabled = true;
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
    }

    [PunRPC]
    void whoCanSell()
    {
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U == usedCards[cardcount - 1].ON2U && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U == usedCards[cardcount - 1].MYT4U && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US == usedCards[cardcount - 1].GRO4US && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U == usedCards[cardcount - 1].OK4U && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }

        for (int j = 0; j < GameManager.instace.playerList.Count; j++)
        {
            if (usedCards[cardcount - 1].ON2U == true && GameManager.instace.playerList[j].hasON2U == false)
            {
                UIController.instance.cancelButton.SetActive(false);
            }
            if (usedCards[cardcount - 1].MYT4U == true && GameManager.instace.playerList[j].hasMYT4U == false)
            {
                UIController.instance.cancelButton.SetActive(false);
            }
            if (usedCards[cardcount - 1].GRO4US == true && GameManager.instace.playerList[j].hasGRO4US == false)
            {
                UIController.instance.cancelButton.SetActive(false);
            }
            if (usedCards[cardcount - 1].OK4U == true && GameManager.instace.playerList[j].hasOK4U == false)
            {
                UIController.instance.cancelButton.SetActive(false);
            }
        }
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
    void UpdateEachKeepForInvest(int x)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[x].ON2UList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].ON2UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].ON2UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasON2U = false;


                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (GameManager.instace.playerList[x].MYT4UList[0].countShare > 0)
            {

                if (GameManager.instace.playerList[x].MYT4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].MYT4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasMYT4U = false;

                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[x].GRO4USList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].GRO4USList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].GRO4USList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasGRO4US = false;

                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[x].OK4UList[0].countShare > 0)
            {


                if (GameManager.instace.playerList[x].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].OK4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasOK4U = false;

                }
            }

        }
    }
}