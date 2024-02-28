using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PayCarDebt : MonoBehaviourPunCallbacks
{
    public static PayCarDebt instance;

    public TMP_Text CarDebt;
    public TMP_Text CarDebtNum;

    public GameObject cancelButton;
    public Button PayCarDebtButton;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Int32.Parse(CarDebtNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money)
        {
            PayCarDebtButton.interactable = false;
        }
        else
        {
            PayCarDebtButton.interactable = true;
        }
        CarDebtNum.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt.ToString();

    }

    public void OkClick()
    {
        if (GameManager.instace.playerList[GameManager.instace.activePlayer].money >= Int32.Parse(CarDebtNum.text))
        {
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money -= Int32.Parse(CarDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt -= Int32.Parse(CarDebtNum.text);
            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage = 0;
            photonView.RPC("UpdatePayCarDebt", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            UIController.instance.PayCarDebtCanvas.SetActive(false);
            UIController.instance.BlurBg.SetActive(false);
            photonView.RPC("UpdatePayCarMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
            photonView.RPC("valueUpdate", RpcTarget.All);
        }
        else
        {
            UIController.instance.PayCarDebtCanvas.SetActive(true);
            UIController.instance.BlurBg.SetActive(true);
        }

    }

    public void CancelClick()
    {
        UIController.instance.PayCarDebtCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
    }

    [PunRPC]
    void UpdatePayCarDebt(int money,int carDebt,int carMortgage,int x)
    {
        GameManager.instace.playerList[x].money = money;
        GameManager.instace.playerList[x].carDebt = carDebt;
        GameManager.instace.playerList[x].carMortgage = carMortgage ;
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
    void UpdatePayCarMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "- " + " Pay CarDebt";
        myNote.price = Int32.Parse(CarDebtNum.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }
}
