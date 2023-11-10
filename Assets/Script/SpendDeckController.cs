using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpendDeckController : MonoBehaviourPunCallbacks
{
    public static SpendDeckController instance;
    public int cardcount=0; 

    private void Awake()
    {
        instance = this;
    }

    public List<CardSpendScriptableObj> deckToUse = new List<CardSpendScriptableObj>();

    public List<CardSpendScriptableObj> activeCards = new List<CardSpendScriptableObj>();

    public List<CardSpendScriptableObj> usedCards = new List<CardSpendScriptableObj>();

    public SpendCard cardsToSpawns;

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
        activeCards.Clear();
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.cancelButton.SetActive(false);
        //UIController.instance.passButton.SetActive(false);
        photonView.RPC("EndTurnPlayer", RpcTarget.All, false);
        UIController.instance.loanButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);

        List<CardSpendScriptableObj> tempDeck = new List<CardSpendScriptableObj>();
        tempDeck.AddRange(deckToUse);

        int iterations = 0;
        while (tempDeck.Count > 0 && iterations < 500)
        {
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);

            iterations++;
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


        UIController.instance.cardShow.enabled = true;
        
        UIController.instance.loanButton.SetActive(true);
        UIController.instance.payButton.SetActive(true);
        UIController.instance.drawButton.SetActive(false);

        ShowController.instance.AddCardToShow(newCard);

        //GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - activeCards[0].payCost;

        UIController.instance.cardShow.sprite = activeCards[0].cardSprite;
        usedCards.Add(activeCards[0]);
        cardcount++;
        activeCards.RemoveAt(0);
        Destroy(newCard.gameObject, 1);
        
        //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        
    }

    public void PayCost()
    {

        if(usedCards[cardcount - 1].hasChildsOrNot == true && GameManager.instace.playerList[GameManager.instace.activePlayer].hasChild == false)
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money;
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            //UIController.instance.cancelButton.SetActive(false);
            UIController.instance.loanButton.SetActive(false);
            UIController.instance.payButton.SetActive(false);

            //UIController.instance.passButton.SetActive(true);
            photonView.RPC("EndTurnPlayer", RpcTarget.All, true);
        }
        else
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - usedCards[cardcount - 1].payCost;
            UIController.instance.drawButton.SetActive(false);
            UIController.instance.cardShow.enabled = false;
            //UIController.instance.cancelButton.SetActive(false);
            UIController.instance.loanButton.SetActive(false);
            UIController.instance.payButton.SetActive(false);

            //UIController.instance.passButton.SetActive(true);
            photonView.RPC("EndTurnPlayer", RpcTarget.All, true);
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
    void EndTurnPlayer(bool isTurn)
    {
        UIController.instance.passButton.SetActive(isTurn);
    }
}