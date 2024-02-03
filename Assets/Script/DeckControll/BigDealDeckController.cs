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


        UIController.instance.cardShow.enabled = true;
        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        ShowBigDealController.instance.AddCardToShow(newCard);

        
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;

        photonView.RPC("AddToUseCard", RpcTarget.All);

        Destroy(newCard.gameObject, 1);
    }

    public void PayCost()
    {
        //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].CardName = usedCards[cardcount - 1].cardName;
        //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].price = usedCards[cardcount - 1].payCost;
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