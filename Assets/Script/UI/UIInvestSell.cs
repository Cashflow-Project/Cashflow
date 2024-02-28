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

        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money +=+ Int32.Parse(sumCalculate.text);
        photonView.RPC("UpdateKeepForSellInvest", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1, Int32.Parse(inputNum.text));
        photonView.RPC("UpdateSellInvestMoney", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1,inputNum);
       UIController.instance.investSellCanvas.SetActive(false);
        UIController.instance.BlurBg.SetActive(false);
        UIController.instance.drawButton.SetActive(false);
        //UIController.instance.cardShow.enabled = false;
        UIController.instance.payButton.SetActive(false);
        UIController.instance.BigPayButton.SetActive(false);
        UIController.instance.SellButton.SetActive(false);
        UIController.instance.cancelButton.SetActive(false);
        UIController.instance.SmallPayButton.SetActive(false);
        UIController.instance.passButton.SetActive(false);

        

    
        
    }

    public void CancelClick()
    {
        UIController.instance.investSellCanvas.SetActive(false);
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
    void UpdateSellInvestMoney(int money, int x,int input)
    {
        GameManager.instace.playerList[x].money = money;
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
        //note collect
        GameManager.Note myNote = new GameManager.Note();
        myNote.CardName = "+ " + "Sell " + SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].cardName + " " + input.ToString();
        myNote.price = Int32.Parse(sumCalculate.text);
        GameManager.instace.playerList[x].Keep.Add(myNote);

        GameManager.Note myNote2 = new GameManager.Note();
        myNote2.CardName = "= ";
        myNote2.price = GameManager.instace.playerList[x].money;
        GameManager.instace.playerList[x].Keep.Add(myNote2);

    }




    [PunRPC]
    void UpdateKeepForSellInvest(int x, int input)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[x].hasON2U == true)
            {
                GameManager.instace.playerList[x].ON2UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].ON2UList[0].countShare * GameManager.instace.playerList[x].ON2UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].ON2UList[0].countShare);
                GameManager.instace.playerList[x].ON2UList[0].countShare -= input;
                if (GameManager.instace.playerList[x].ON2UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].ON2UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasON2U = false;
                    //GameManager.instace.playerList[x].ON2UList[0].pricePerShare = 0;

                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

            if (GameManager.instace.playerList[x].hasMYT4U == true)
            {
                GameManager.instace.playerList[x].MYT4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].MYT4UList[0].countShare * GameManager.instace.playerList[x].MYT4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].MYT4UList[0].countShare);
                GameManager.instace.playerList[x].MYT4UList[0].countShare -= input;
                if (GameManager.instace.playerList[x].MYT4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].MYT4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasMYT4U = false;
                    //GameManager.instace.playerList[x].MYT4UList[0].pricePerShare = 0;
                }
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[x].hasGRO4US == true)
            {
                GameManager.instace.playerList[x].GRO4USList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].GRO4USList[0].countShare * GameManager.instace.playerList[x].GRO4USList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].GRO4USList[0].countShare);
                GameManager.instace.playerList[x].GRO4USList[0].countShare -= input;
                if (GameManager.instace.playerList[x].GRO4USList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].GRO4USList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasGRO4US = false;
                    //GameManager.instace.playerList[x].GRO4USList[0].pricePerShare = 0;
                }
            }


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[x].hasOK4U == true)
            {
                GameManager.instace.playerList[x].OK4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].OK4UList[0].countShare * GameManager.instace.playerList[x].OK4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].OK4UList[0].countShare);
                GameManager.instace.playerList[x].OK4UList[0].countShare -= input;
                if (GameManager.instace.playerList[x].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].OK4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasOK4U = false;
                    //GameManager.instace.playerList[x].OK4UList[0].pricePerShare = 0;
                }
            }


        }
    }
}
