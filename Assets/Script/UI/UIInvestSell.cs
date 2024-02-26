using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class UIInvestSell : MonoBehaviourPunCallbacks
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
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
            if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare || Int32.Parse(inputNum.text)%1 != 0 )
            {
                SellButton.interactable = false;
            }
            else
            {
                SellButton.interactable = true;
            } 
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare || Int32.Parse(inputNum.text) % 1 != 0 )
            {
                SellButton.interactable = false;
            }
            else
            {
                SellButton.interactable = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {
            if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare || Int32.Parse(inputNum.text) % 1 != 0 )
            {
                SellButton.interactable = false;
            }
            else
            {
                SellButton.interactable = true;
            }
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {
            if (!isNumeric || Int32.Parse(inputNum.text) <= 0 || Int32.Parse(inputNum.text) > GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare || Int32.Parse(inputNum.text) % 1 != 0 )
            {
                SellButton.interactable = false;
            }
            else
            {
                SellButton.interactable = true;
            }
        }
        sumCalculate.text = (Int32.Parse(inputNum.text) * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value).ToString();

    }

    public void OkSellClick()
    {
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money + Int32.Parse(sumCalculate.text);
        UIController.instance.investSellCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        UIController.instance.cardShow.enabled = false;
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.passButton.SetActive(false);

        photonView.RPC("UpdateMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare > 0)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare);
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare > 0)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare);
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare > 0)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare);
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare > 0)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare);
            }

        }
    
        
    }

    public void CancelClick()
    {
        UIController.instance.investSellCanvas.SetActive(false);

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
        myNote.CardName = "+ " + "Sell " + SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName + " " + inputNum.text;
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }


    [PunRPC]
    void UpdateEachKeepForInvest(int countShare)
    {
        
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare > 0)
            {
                
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare = countShare;
                
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList.RemoveAt(0);
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U = false;
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].pricePerShare = 0;
                    
                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare > 0)
            {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare = countShare;
                
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList.RemoveAt(0);
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U = false;
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].pricePerShare = 0;
                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare > 0)
            {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare = countShare;
                
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList.RemoveAt(0);
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US = false;
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].pricePerShare = 0;
                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare > 0)
            {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare = countShare;
                
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList.RemoveAt(0);
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U = false;
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].pricePerShare = 0;
                }
            }

        }
    }

}
