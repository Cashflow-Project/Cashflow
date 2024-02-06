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


        UIController.instance.cardShow.enabled = true;

        
        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        ShowSmallDealController.instance.AddCardToShow(newCard);

        
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;

        photonView.RPC("AddToUseCard", RpcTarget.All);

        Destroy(newCard.gameObject, 1);
    }

    public void PayCost()
    {

        GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount++;
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.payButton.SetActive(false);
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
    void CreateSmallDealDeckStart(int selected)
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
    }

    [PunRPC]
    void CalculateSmallDealRPC(int money)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].DownPayment;
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = money;
    }

    [PunRPC]
    void drawCardSmallDeal()
    {
        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        SmallDealCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardSCSO = activeCards[0];


        UIController.instance.cardShow.enabled = true;

        
        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        ShowSmallDealController.instance.AddCardToShow(newCard);

        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);
        Destroy(newCard.gameObject, 1);
    }
}