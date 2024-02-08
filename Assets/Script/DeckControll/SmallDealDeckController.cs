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
        
        SetUpDeck();

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void SetUpDeck()
    {

        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.passButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);


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

        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        SmallDealCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardSCSO = activeCards[0];

        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(true);
        UIController.instance.SellButton.SetActive(false);


        


        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        //UIController.instance.cardShow.enabled = true;
        Debug.Log("check card sprite" + activeCards[0].cardSprite);
        //ShowSmallDealController.instance.AddCardToShow(newCard);
        photonView.RPC("ShowCardToAllPlayerRPC", RpcTarget.All);

        //UIController.instance.cardShow.sprite = activeCards[0].cardSprite;

        //do somthing for invest on2u ok4u myt4u grou4us to other people

        photonView.RPC("AddToUseCard", RpcTarget.All);
        photonView.RPC("whoCanSell", RpcTarget.All);
        Destroy(newCard.gameObject, 1);
    }

    public void BuyCost()
    {
        if (usedCards[cardcount - 1].ON2U == true || usedCards[cardcount - 1].MYT4U == true || usedCards[cardcount - 1].GRO4US == true || usedCards[cardcount - 1].OK4U == true)
        {
            UIController.instance.InvestCanvas.SetActive(true);
            
        }
        else if (usedCards[cardcount - 1].extra1 == true || usedCards[cardcount - 1].extra2 == true || usedCards[cardcount - 1].extra3 == true)
        {
            //roll dice
        }
        else if(usedCards[cardcount - 1].special == true)
        {

        }
        else
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

       


    }

    public void Loan()
    {

    }
    

    [PunRPC]
    void CreateSmallDealDeckStart(int selected)
    {
        activeCards.Add(tempDeck[selected]);
        tempDeck.RemoveAt(selected);

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
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = usedCards[cardcount - 1].cardName;
        myNote.price = usedCards[cardcount - 1].DownPayment;
        GameManager.instace.playerList[GameManager.instace.activePlayer].Keep.Add(myNote);
        GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount++;
        
    }

    [PunRPC]
    void UpdateKeepForDeal()
    {
        //Deal collect
        GameManager.DealKeep myDeal = new GameManager.DealKeep();
        myDeal.CardName = usedCards[cardcount - 1].cardName;
        myDeal.BusinessValue = usedCards[cardcount - 1].BusinessValue;
        myDeal.DownPayment = usedCards[cardcount - 1].DownPayment;
        myDeal.BankLoan = usedCards[cardcount - 1].BankLoan;
        myDeal.CashflowIncome = usedCards[cardcount - 1].CashflowIncome;
        myDeal.count = usedCards[cardcount - 1].count;
        GameManager.instace.playerList[GameManager.instace.activePlayer].DealList.Add(myDeal);
    }

    [PunRPC]
    void UpdateKeepForInvest()
    {
        //invest collect
        GameManager.InvestKeep myInvest = new GameManager.InvestKeep();
        myInvest.CardName = usedCards[cardcount - 1].cardName;
        myInvest.countShare = usedCards[cardcount - 1].count;
        myInvest.pricePerShare = usedCards[cardcount - 1].value;
        if (usedCards[cardcount - 1].ON2U == true)
        {
            myInvest.ON2U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U = true;
        }
        if (usedCards[cardcount - 1].MYT4U == true)
        {
            myInvest.MYT4U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U = true;
        }
        if (usedCards[cardcount - 1].GRO4US == true)
        {
            myInvest.GRO4US = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US = true;
        }
        if (usedCards[cardcount - 1].OK4U == true)
        {
            myInvest.OK4U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U = true;
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
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U == usedCards[cardcount - 1].ON2U && GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U == usedCards[cardcount - 1].MYT4U && GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US == usedCards[cardcount - 1].GRO4US && GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U == usedCards[cardcount - 1].OK4U && GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U == true)
        {
            UIController.instance.SellButton.SetActive(true);
        }
    }
}