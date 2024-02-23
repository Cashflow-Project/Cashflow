using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class GoldSell : MonoBehaviourPunCallbacks
{
    public static UIInvestSell instance;

    public TMP_Text sumCalculate;
    public GameObject cancelButton;
    public Button SellButton;
    public TMP_InputField inputNum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var isNumeric = int.TryParse(inputNum.text, out int n);
        if (MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].GoldCoins == true)
        {
            if (!isNumeric || Int32.Parse(inputNum.text) <= 0 
                || Int32.Parse(inputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GoldCoins )
            {
                SellButton.interactable = false;
            }
            else
            {
                SellButton.interactable = true;
            }
        }
        sumCalculate.text = (Int32.Parse(inputNum.text) * MarketDeckController.instance.usedCards[MarketDeckController.instance.cardcount - 1].Cost).ToString();

    }

    public void OkSellClick()
    {
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(sumCalculate.text);
        UIController.instance.investSellCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.BlurBg.SetActive(false);
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.GoldCoinsSell.SetActive(false);
        UIController.instance.MarketPayButton.SetActive(false); ;
        UIController.instance.MarketSellButton.SetActive(false);
        photonView.RPC("UpdateGoldcoins", RpcTarget.All);
        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);

    }

    public void CancelClick()
    {
        UIController.instance.GoldCoinsSell.SetActive(false);
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
    void UpdateMoney(int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "+ " + "Sell Goldcoins" +  inputNum.text;
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].Keep.Add(myNote);

    }


    [PunRPC]
    void UpdateGoldcoins()
    {

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GoldCoins > 0)
        {

            GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GoldCoins -= Int32.Parse(inputNum.text);
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GoldCoins == 0)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGoldCoins = false;
            }
        }

    }
}
