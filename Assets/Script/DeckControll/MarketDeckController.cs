using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MarketDeckController : MonoBehaviourPunCallbacks
{
    public static MarketDeckController instance;
    public int cardcount = 0;

    private void Awake()
    {
        instance = this;

    }

    public List<CardGlobalMarketScriptableObj> deckToUse = new List<CardGlobalMarketScriptableObj>();

    public List<CardGlobalMarketScriptableObj> activeCards = new List<CardGlobalMarketScriptableObj>();

    public List<CardGlobalMarketScriptableObj> usedCards = new List<CardGlobalMarketScriptableObj>();

    public List<CardGlobalMarketScriptableObj> tempDeck = new List<CardGlobalMarketScriptableObj>();

    //int iterations = 0;

    public MarketCard cardsToSpawns;

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
        UIController.instance.MarketDrawButton.SetActive(false);

        activeCards.Clear();
        tempDeck.AddRange(deckToUse);
        if (PhotonNetwork.IsMasterClient)
        {

            int iterations = 0;
            while (tempDeck.Count > 0 && iterations < 500)
            {
                int selected = Random.Range(0, tempDeck.Count);
                photonView.RPC("CreateMarketDeckStart", RpcTarget.All, selected);

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

        MarketCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardGMSO = activeCards[0];

        UIController.instance.payButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.MarketDrawButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(true);
        photonView.RPC("ShowCardToAllPlayerRPC", RpcTarget.All);


        photonView.RPC("AddToUseCard", RpcTarget.All);

        Destroy(newCard.gameObject, 1);
    }

    public void SellCost()
    {

            //Debug.Log("7");
            //photonView.RPC("CalculateSpendRPChasChild", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money);
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].Cost;
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].CardName = usedCards[cardcount - 1].cardName;
            //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].price = usedCards[cardcount - 1].payCost;

            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            //UIController.instance.cancelButton.SetActive(false);
            //UIController.instance.loanButton.SetActive(false);
            UIController.instance.payButton.SetActive(false);
            GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready = true;
            UIController.instance.passButton.SetActive(true);
            //photonView.RPC("EndTurnPlayer", RpcTarget.All);


    }

    public void Loan()
    {

    }



    private bool IsMyTurn()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return GameManager.instace.activePlayer == PhotonNetwork.LocalPlayer.ActorNumber - 1;
    }

    [PunRPC]
    void EndTurnPlayer()
    {
        UIController.instance.passButton.SetActive(IsMyTurn());
    }

    [PunRPC]
    void CreateMarketDeckStart(int selected)
    {
        activeCards.Add(tempDeck[selected]);
        tempDeck.RemoveAt(selected);
        //activeCards.AddRange(activeCards);
        //activeCards.Add(tempDeck[i]);
        //tempDeck.RemoveAt(i);
        //iterations++;

    }

    [PunRPC]
    void AddToUseCard()
    {
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);


    }

    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = usedCards[cardcount - 1].cardName;
        myNote.price = usedCards[cardcount - 1].Cost;
        GameManager.instace.playerList[GameManager.instace.activePlayer].Keep.Add(myNote);
        GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount++;

    }

    [PunRPC]
    void ShowCardToAllPlayerRPC()
    {
        UIController.instance.cardShow.enabled = true;
        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
    }

    [PunRPC]
    void CalculateMarketRPC(int money)
    {


        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].Cost;
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = money;
    }
}
