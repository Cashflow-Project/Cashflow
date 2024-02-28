using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class UILoanCanvas : MonoBehaviourPunCallbacks
{
    public static UILoanCanvas instance;

    public TMP_Text canLoan;
    public TMP_Text LoanAlready;

    public GameObject cancelButton;
    public Button LoanButton;
    public TMP_InputField LoanInputNum;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        var isNumeric = int.TryParse(LoanInputNum.text, out int n);
        if (!isNumeric || Int32.Parse(LoanInputNum.text) <= 0 || Int32.Parse(LoanInputNum.text) > Int32.Parse(canLoan.text) || Int32.Parse(LoanInputNum.text)%10 != 0)
        {
            LoanButton.interactable = false;
        }
        else
        {
            LoanButton.interactable = true;
        }
        LoanAlready.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank.ToString();
        canLoan.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney * 10).ToString();
        
    }

    public void OkClick()
    {

        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money += Int32.Parse(LoanInputNum.text);
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank += Int32.Parse(LoanInputNum.text);
        photonView.RPC("UpdateLoan", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        UIController.instance.LoanCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        photonView.RPC("valueUpdate", RpcTarget.All);


    }

    public void CancelClick()
    {
        UIController.instance.LoanCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "+ " + "Loan";
        myNote.price = Int32.Parse(LoanInputNum.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);
    }

    [PunRPC]
    void UpdateLoan(int money,int loanbank,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.instace.playerList[x].loanBank = loanbank;
        
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

}
