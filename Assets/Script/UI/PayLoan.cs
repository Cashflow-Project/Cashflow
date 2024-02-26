using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PayLoan : MonoBehaviourPunCallbacks
{
    public static PayLoan instance;

    public TMP_Text canLoan;
    public TMP_Text LoanAlready;

    public GameObject cancelButton;
    public Button PayLoanButton;
    public TMP_InputField PayLoanInputNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var isNumeric = int.TryParse(PayLoanInputNum.text, out int n);
        if (!isNumeric || Int32.Parse(PayLoanInputNum.text) <= 0 
            || Int32.Parse(PayLoanInputNum.text) > Int32.Parse(LoanAlready.text) 
            || Int32.Parse(PayLoanInputNum.text) % 10 != 0 
            || Int32.Parse(PayLoanInputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money)
        {
            PayLoanButton.interactable = false;
        }
        else
        {
            PayLoanButton.interactable = true;
        }
        LoanAlready.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank.ToString();
        canLoan.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney * 10).ToString();
    }

    public void PayClick()
    {
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money -= Int32.Parse(PayLoanInputNum.text);
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank -= Int32.Parse(PayLoanInputNum.text);
        photonView.RPC("UpdatePayLoan", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        UIController.instance.PayLoanCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        photonView.RPC("valueUpdate", RpcTarget.All);

    }

    public void CancelClick()
    {
        UIController.instance.PayLoanCanvas.SetActive(false);
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
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;

    }
    [PunRPC]
    void UpdatePayLoan(int money,int loanBank,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.instace.playerList[x].loanBank = loanBank;
    }

    [PunRPC]
    void valueUpdate()
    {
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank / 10;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].child * GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].perChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].tax + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditcardMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].extraPay + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid;
    }
    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " +  " Pay Loan" ;
        myNote.price = Int32.Parse(PayLoanInputNum.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
}
