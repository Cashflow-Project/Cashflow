using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class BigDealDeckController : MonoBehaviourPunCallbacks
{
    public static BigDealDeckController instance;
    public int cardcount = 0; 

    private void Awake()
    {
        instance = this;

    }

    public List<CardBigChangeScriptableObj> deckToUse = new List<CardBigChangeScriptableObj>();

    public List<CardBigChangeScriptableObj> activeCards = new List<CardBigChangeScriptableObj>();

    public List<CardBigChangeScriptableObj> usedCards = new List<CardBigChangeScriptableObj>();

    public List<CardBigChangeScriptableObj> tempDeck = new List<CardBigChangeScriptableObj>();

    public BigDealCard cardsToSpawns;

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
                photonView.RPC("CreateBigDealDeckStart", RpcTarget.All,selected);

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

        BigDealCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardBCSO = activeCards[0];

        UIController.instance.ChooseBigSmall.SetActive(false);
        
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(true);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        ShowBigDealController.instance.AddCardToShow(newCard);
        photonView.RPC("ShowCardToAllPlayerRPC", RpcTarget.All);
        /*
        UIController.instance.cardShow.enabled = true;
        ShowBigDealController.instance.AddCardToShow(newCard);

        
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
        */
        photonView.RPC("AddToUseCard", RpcTarget.All);

        //Destroy(newCard.gameObject, 1);
    }

    public void BuyCost()
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].DownPayment;
        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
        
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.passButton.SetActive(true);
    }

    public void Loan()
    {

    }
    

    private bool IsMyTurn()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return PhotonNetwork.LocalPlayer.ActorNumber - 1 == GameManager.instace.activePlayer;
    }

    [PunRPC]
    void EndTurnPlayer()
    {
        UIController.instance.passButton.SetActive(IsMyTurn());
    }

    [PunRPC]
    void CreateBigDealDeckStart(int selected)
    {
        activeCards.Add(tempDeck[selected]);
        tempDeck.RemoveAt(selected);

    }

    [PunRPC]
    void AddToUseCard()
    {
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
        //invest collect
        GameManager.DealKeep myDeal = new GameManager.DealKeep();
        myDeal.CardName = usedCards[cardcount - 1].cardName;
        myDeal.BusinessValue = usedCards[cardcount - 1].BusinessValue;
        myDeal.DownPayment = usedCards[cardcount - 1].DownPayment;
        myDeal.BankLoan = usedCards[cardcount - 1].BankLoan;
        myDeal.CashflowIncome = usedCards[cardcount - 1].CashflowIncome;
        myDeal.count = usedCards[cardcount - 1].count;
        if (usedCards[cardcount - 1].house3s2 == true)
        {
            myDeal.house3s2 = true;
        }
        if (usedCards[cardcount - 1].CommercialBuilding == true)
        {
            myDeal.CommercialBuilding = true;
        }
        if (usedCards[cardcount - 1].Apartment == true)
        {
            myDeal.Apartment = true;
        }
        if (usedCards[cardcount - 1].Business == true)
        {
            myDeal.Business = true;
        }
        GameManager.instace.playerList[GameManager.instace.activePlayer].DealList.Add(myDeal);
        
    }

    [PunRPC]
    void ShowCardToAllPlayerRPC()
    {
        UIController.instance.cardShow.enabled = true;
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
    }

    [PunRPC]
    void CalculateBigDealRPC(int money)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].DownPayment ;
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = money;
    }

    [PunRPC]
    void drawCardBigDeal()
    {
        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        BigDealCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardBCSO = activeCards[0];


        UIController.instance.cardShow.enabled = true;

        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        ShowBigDealController.instance.AddCardToShow(newCard);

        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);
        Destroy(newCard.gameObject, 1);
    }
}