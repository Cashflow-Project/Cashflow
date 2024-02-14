using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class UIInvestConfirm : MonoBehaviourPunCallbacks
{
    public static UIInvestConfirm instance;

    public TMP_Text sumCalculate;
    public GameObject cancelButton;
    public Button BuyButton;
    public TMP_InputField inputNum;
    
    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.payButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var isNumeric = int.TryParse(inputNum.text, out int n);
        if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) % 1 != 0)
        {
            BuyButton.interactable = false;
        }
        else
        {
            BuyButton.interactable = true;
        }
        sumCalculate.text = (Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value).ToString();
        
    }

    public void OkClick()
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - Int32.Parse(sumCalculate.text);
        SetAllFalse();
        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
        photonView.RPC("UpdateKeepForInvest", RpcTarget.All);
        UIController.instance.passButton.SetActive(true);
    }

    public void CancelClick()
    {
        UIController.instance.InvestCanvas.SetActive(false);

        UIController.instance.payButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(true);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(true);
        UIController.instance.ChooseBigSmall.SetActive(false);

    }

    public void SetAllFalse()
    {

        UIController.instance.InvestCanvas.SetActive(false);

        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;

    }
    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName + " " + inputNum.text;
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[GameManager.instace.activePlayer].Keep.Add(myNote);
        GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount++;

    }

    [PunRPC]
    void UpdateKeepForInvest()
    {
        
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
           
            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].pricePerShare = ((Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].countShare * GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].pricePerShare))
                    / (Int32.Parse(inputNum.text) + GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].countShare);
                GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].countShare += Int32.Parse(inputNum.text);
                
            }
            else
            {
                GameManager.ON2UKeep myON2U = new GameManager.ON2UKeep();
                myON2U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myON2U.countShare = Int32.Parse(inputNum.text);
                myON2U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList.Add(myON2U);
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].pricePerShare = ((Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].countShare * GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].pricePerShare))
                    / (Int32.Parse(inputNum.text) + GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].countShare);
                GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].countShare += Int32.Parse(inputNum.text);
            }
            else
            {
                GameManager.MYT4UKeep myMYT4U = new GameManager.MYT4UKeep();
                myMYT4U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myMYT4U.countShare = Int32.Parse(inputNum.text);
                myMYT4U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList.Add(myMYT4U);
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].pricePerShare = ((Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].countShare * GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].pricePerShare))
                    / (Int32.Parse(inputNum.text) + GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].countShare);
                GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].countShare += Int32.Parse(inputNum.text);
            }
            else
            {
                GameManager.GRO4USKeep myGRO4US = new GameManager.GRO4USKeep();
                myGRO4US.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myGRO4US.countShare = Int32.Parse(inputNum.text);
                myGRO4US.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList.Add(myGRO4US);
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US = true;
            }
                
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].pricePerShare = ((Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].countShare * GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].pricePerShare))
                    / (Int32.Parse(inputNum.text) + GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].countShare);
                GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].countShare += Int32.Parse(inputNum.text);
            }
            else
            {
                GameManager.OK4UKeep myOK4U = new GameManager.OK4UKeep();
                myOK4U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myOK4U.countShare = Int32.Parse(inputNum.text);
                myOK4U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList.Add(myOK4U);
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U = true;
            }
            
        }
    }
}
