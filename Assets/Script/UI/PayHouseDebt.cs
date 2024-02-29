using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PayHouseDebt : MonoBehaviourPunCallbacks
{
    public static PayHouseDebt instance;

    public TMP_Text HouseDebt;
    public TMP_Text HouseDebtNum;

    public GameObject cancelButton;
    public Button PayHouseDebtButton;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Int32.Parse(HouseDebtNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money)
        {
            PayHouseDebtButton.interactable = false;
        }
        else
        {
            PayHouseDebtButton.interactable = true;
        }
        HouseDebtNum.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt.ToString();
        
    }

    public void OkClick()
    {
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].money >= Int32.Parse(HouseDebtNum.text))
        {
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money -= Int32.Parse(HouseDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt -= Int32.Parse(HouseDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage = 0;
            photonView.RPC("UpdatePayHouseDebt", RpcTarget.Others, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            UIController.instance.PayHouseDebtCanvas.SetActive(false);
            UIController.instance.BlurBg.SetActive(false);
            photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            photonView.RPC("valueNewUpdate", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        }
        else
        {
            UIController.instance.PayHouseDebtCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }

    }

    public void CancelClick()
    {
        UIController.instance.PayHouseDebtCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    [PunRPC]
    void UpdatePayHouseDebt(int money,int homeDebt,int homeMortgage,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.instace.playerList[x].homeDebt = homeDebt;
        GameManager.instace.playerList[x].homeMortgage = homeMortgage;
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
        myNote.CardName = "- " + " Pay HouseDebt";
        myNote.price = Int32.Parse(HouseDebtNum.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
}
