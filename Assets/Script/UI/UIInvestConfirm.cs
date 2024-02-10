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
        if (!isNumeric || Int32.Parse(inputNum.text) <= 0)
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
        myNote.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName + " " + inputNum.text;
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[GameManager.instace.activePlayer].Keep.Add(myNote);
        GameManager.instace.playerList[GameManager.instace.activePlayer].KeepCount++;

    }

    [PunRPC]
    void UpdateKeepForInvest()
    {
        //invest collect
        GameManager.InvestKeep myInvest = new GameManager.InvestKeep();
        myInvest.CardName = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName;
        myInvest.countShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].count;
        myInvest.pricePerShare = SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value;
        myInvest.sumValue = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[GameManager.instace.activePlayer].InvestList.Add(myInvest);
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
            myInvest.ON2U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U = true;
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            myInvest.MYT4U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U = true;
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {
            myInvest.GRO4US = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US = true;
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {
            myInvest.OK4U = true;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U = true;
        }
    }
}
