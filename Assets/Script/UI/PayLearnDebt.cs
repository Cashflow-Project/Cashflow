using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
public class PayLearnDebt : MonoBehaviourPunCallbacks
{
    public static PayLearnDebt instance;

    public TMP_Text LearnDebt;
    public TMP_Text LearnDebtNum;

    public GameObject cancelButton;
    public Button PayLearnDebtButton;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Int32.Parse(LearnDebtNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money)
        {
            PayLearnDebtButton.interactable = false;
        }
        else
        {
            PayLearnDebtButton.interactable = true;
        }
        LearnDebtNum.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt.ToString();

    }

    public void OkClick()
    {
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].money >= Int32.Parse(LearnDebtNum.text))
        {
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money -= Int32.Parse(LearnDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt -= Int32.Parse(LearnDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage = 0;
            photonView.RPC("UpdatePayLearnDebt", RpcTarget.Others, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            UIController.instance.PayLearnDebtCanvas.SetActive(false);
            UIController.instance.BlurBg.SetActive(false);
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            photonView.RPC("valueNewUpdate", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        }
        else
        {
            UIController.instance.PayLearnDebtCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }

    }

    public void CancelClick()
    {
        UIController.instance.PayLearnDebtCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    [PunRPC]
    void UpdatePayLearnDebt(int money,int learnDebt,int learnMortgage,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.instace.playerList[x].learnDebt = learnDebt;
        GameManager.instace.playerList[x].learnMortgage = learnMortgage;
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
    [PunRPC]
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + " Pay LearnDebt";
        myNote.price = Int32.Parse(LearnDebtNum.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
}
