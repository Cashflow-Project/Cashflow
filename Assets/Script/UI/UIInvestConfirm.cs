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

    }

    // Update is called once per frame
    void Update()
    {
        var isNumeric = int.TryParse(inputNum.text, out int n);
        if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) % 1 != 0 || Int32.Parse(sumCalculate.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money)
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
        if(GameManager.instace.playerList[GameManager.instace.activePlayer].money >= Int32.Parse(sumCalculate.text))
        {
            GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money - Int32.Parse(sumCalculate.text);
            photonView.RPC("UpdateKeepForInvest", RpcTarget.All, GameManager.instace.activePlayer,Int32.Parse(inputNum.text));
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[GameManager.instace.activePlayer].money, GameManager.instace.activePlayer);
            SetAllFalse();
            UIController.instance.BlurBg.SetActive(false);
            //UIController.instance.passButton.SetActive(true);
        }
        else
        {
            UIController.instance.LoanCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }
        
    }

    public void CancelClick()
    {
        UIController.instance.InvestCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    public void SetAllFalse()
    {

        UIController.instance.InvestCanvas.SetActive(false);
        UIController.instance.ChooseBigSmall.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);


    }
    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName + " " + inputNum.text;
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);
    }

    [PunRPC]
    void UpdateKeepForInvest(int x,int input)
    {
        
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
           
            if (GameManager.instace.playerList[x].hasON2U == true)
            {
                GameManager.instace.playerList[x].ON2UList[0].pricePerShare = (input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].ON2UList[0].countShare * GameManager.instace.playerList[x].ON2UList[0].pricePerShare)
                    / (input + GameManager.instace.playerList[x].ON2UList[0].countShare);
                GameManager.instace.playerList[x].ON2UList[0].countShare += input;
                
            }
            else
            {
                GameManager.ON2UKeep myON2U = new GameManager.ON2UKeep();
                myON2U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myON2U.countShare = input;
                myON2U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[x].ON2UList.Add(myON2U);
                GameManager.instace.playerList[x].hasON2U = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

            if (GameManager.instace.playerList[x].hasMYT4U == true)
            {
                GameManager.instace.playerList[x].MYT4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].MYT4UList[0].countShare * GameManager.instace.playerList[x].MYT4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].MYT4UList[0].countShare);
                GameManager.instace.playerList[x].MYT4UList[0].countShare += input;
            }
            else
            {
                GameManager.MYT4UKeep myMYT4U = new GameManager.MYT4UKeep();
                myMYT4U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myMYT4U.countShare = input;
                myMYT4U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[x].MYT4UList.Add(myMYT4U);
                GameManager.instace.playerList[x].hasMYT4U = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[x].hasGRO4US == true)
            {
                GameManager.instace.playerList[x].GRO4USList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].GRO4USList[0].countShare * GameManager.instace.playerList[x].GRO4USList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].GRO4USList[0].countShare);
                GameManager.instace.playerList[x].GRO4USList[0].countShare += input;
            }
            else
            {
                GameManager.GRO4USKeep myGRO4US = new GameManager.GRO4USKeep();
                myGRO4US.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myGRO4US.countShare = input;
                myGRO4US.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[x].GRO4USList.Add(myGRO4US);
                GameManager.instace.playerList[x].hasGRO4US = true;
            }
                
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[x].hasOK4U == true)
            {
                GameManager.instace.playerList[x].OK4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].OK4UList[0].countShare * GameManager.instace.playerList[x].OK4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].OK4UList[0].countShare);
                GameManager.instace.playerList[x].OK4UList[0].countShare += input;
            }
            else
            {
                GameManager.OK4UKeep myOK4U = new GameManager.OK4UKeep();
                myOK4U.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
                myOK4U.countShare = input;
                myOK4U.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
                GameManager.instace.playerList[x].OK4UList.Add(myOK4U);
                GameManager.instace.playerList[x].hasOK4U = true;
            }
            
        }
    }
}
