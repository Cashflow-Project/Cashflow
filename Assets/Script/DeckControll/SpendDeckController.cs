using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class SpendDeckController : MonoBehaviourPunCallbacks
{
    public static SpendDeckController instance;
    public int cardcount=0; 

    private void Awake()
    {
        instance = this;
        //PhotonPeer.RegisterType(typeof(CardSpendScriptableObj), 0, CardSpendScriptableObjSerialization.Serialize, CardSpendScriptableObjSerialization.Deserialize);

    }

    public List<CardSpendScriptableObj> deckToUse = new List<CardSpendScriptableObj>();

    public List<CardSpendScriptableObj> activeCards = new List<CardSpendScriptableObj>();

    public List<CardSpendScriptableObj> usedCards = new List<CardSpendScriptableObj>();

    public List<CardSpendScriptableObj> tempDeck = new List<CardSpendScriptableObj>();

    //int iterations = 0;

    public SpendCard cardsToSpawns;

    public int FordrawCard = 1;
    public float waitBetweenDrawingCard = .3f;


    // Start is called before the first frame update
    void Start()
    {
        //PhotonPeer.RegisterType(typeof(CardSpendScriptableObj), 0, CardSpendScriptableObjSerialization.Serialize, CardSpendScriptableObjSerialization.Deserialize);
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
        //photonView.RPC("EndTurnPlayer", RpcTarget.All, false);
        //UIController.instance.loanButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);

        //photonView.RPC("CreateSpendDeckStart", RpcTarget.All);
        /*
        activeCards.Clear();
        List<CardSpendScriptableObj> tempDeck = new List<CardSpendScriptableObj>();
        tempDeck.AddRange(deckToUse);

        int iterations = 0;
        while (tempDeck.Count > 0 && iterations < 500)
        {
            int selected = Random.Range(0, tempDeck.Count);

            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);

            iterations++;
        }*/
        activeCards.Clear();
        tempDeck.AddRange(deckToUse);
        if (PhotonNetwork.IsMasterClient)
        {
            //activeCards.Clear();
            //List<CardSpendScriptableObj> tempDeck = new List<CardSpendScriptableObj>();
            //tempDeck.AddRange(deckToUse);

            int iterations = 0;
            while (tempDeck.Count > 0 && iterations < 500)
            {
                int selected = Random.Range(0, tempDeck.Count);
                photonView.RPC("CreateSpendDeckStart", RpcTarget.All,selected);
                //activeCards.Add(tempDeck[selected]);
                //tempDeck.RemoveAt(selected);

                iterations++;
            }
            //photonView.RPC("CreateSpendDeckStart", RpcTarget.All);
        }

    }

    public void DrawCardToHand()
    {

        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        SpendCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardSpendSO = activeCards[0];


        //UIController.instance.cardShow.enabled = true;

        //UIController.instance.loanButton.SetActive(true);
        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        //ShowController.instance.AddCardToShow(newCard);
        photonView.RPC("ShowCardToAllPlayerRPC", RpcTarget.All);
        //GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - activeCards[0].payCost;

        //UIController.instance.cardShow.sprite = activeCards[0].cardSprite;

        photonView.RPC("AddToUseCard", RpcTarget.All);
        /*
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);
        
        Destroy(newCard.gameObject, 1);*/
        //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        Destroy(newCard.gameObject, 1);
    }

    public void PayCost()
    {

        if(usedCards[cardcount - 1].hasChildsOrNot == true && GameManager.instace.playerList[GameManager.instace.activePlayer].hasChild == false)
        {
            //photonView.RPC("CalculateSpendRPCNoChildCardHave", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money);
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            //UIController.instance.cancelButton.SetActive(false);
            //UIController.instance.loanButton.SetActive(false);
            UIController.instance.payButton.SetActive(false);

            UIController.instance.passButton.SetActive(true);
            //photonView.RPC("EndTurnPlayer", RpcTarget.All);
        }
        else
        {
            //Debug.Log("7");
            //photonView.RPC("CalculateSpendRPChasChild", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money);
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].payCost;
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].CardName = usedCards[cardcount - 1].cardName;
            //GameManager.instace.playerList[GameManager.instace.activePlayer].Keep[GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount].price = usedCards[cardcount - 1].payCost;
            
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            //UIController.instance.cancelButton.SetActive(false);
            //UIController.instance.loanButton.SetActive(false);
            UIController.instance.payButton.SetActive(false);

            UIController.instance.passButton.SetActive(true);
            //photonView.RPC("EndTurnPlayer", RpcTarget.All);
        }
        

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
    void CreateSpendDeckStart(int selected)
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
    void UpdateMoney(int money,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = usedCards[cardcount - 1].cardName;
        myNote.price = usedCards[cardcount - 1].payCost;
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
    void CalculateSpendRPC(int money)
    {
        

        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].payCost ;
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = money;
    }

}