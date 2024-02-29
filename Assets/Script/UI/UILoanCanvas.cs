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
    void Start()
    {
        
    }

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
        photonView.RPC("UpdateLoan", RpcTarget.Others, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        UIController.instance.LoanCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        photonView.RPC("UpdateMoneyLoan", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        photonView.RPC("valueNewUpdate", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        
    }

    public void CancelClick()
    {
        UIController.instance.LoanCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    [PunRPC]
    void UpdateMoneyLoan(int money, int x)
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
    void valueNewUpdate(int x)
    {
        GameManager.instace.playerList[x].allRecieve = GameManager.instace.playerList[x].salary + GameManager.instace.playerList[x].income;
        GameManager.instace.playerList[x].InstallmentsBank = GameManager.instace.playerList[x].loanBank / 10;
        GameManager.instace.playerList[x].sumChild = GameManager.instace.playerList[x].child * GameManager.instace.playerList[x].perChild;
        GameManager.instace.playerList[x].paid = GameManager.instace.playerList[x].tax + GameManager.instace.playerList[x].homeMortgage + GameManager.instace.playerList[x].learnMortgage + GameManager.instace.playerList[x].carMortgage + GameManager.instace.playerList[x].creditcardMortgage + GameManager.instace.playerList[x].extraPay + GameManager.instace.playerList[x].InstallmentsBank + GameManager.instace.playerList[x].sumChild;
        GameManager.instace.playerList[x].getmoney = GameManager.instace.playerList[x].allRecieve - GameManager.instace.playerList[x].paid;
    }

}
