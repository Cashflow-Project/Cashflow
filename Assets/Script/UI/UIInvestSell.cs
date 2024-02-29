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
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U == true)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare);
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U = false;
                    photonView.RPC("UpdateStillInvestSell", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber - 1, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U);
                }
                photonView.RPC("UpdateKeepForSellInvestButNoLeft", RpcTarget.All);
            }
            
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U == true)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare);
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U = false;
                    photonView.RPC("UpdateStillInvestSell", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber - 1, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U);
                }
                photonView.RPC("UpdateKeepForSellInvestButNoLeft", RpcTarget.All);
            }
            
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US == true)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare);
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US = false;
                    photonView.RPC("UpdateStillInvestSell", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber - 1, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US);
                }
                photonView.RPC("UpdateKeepForSellInvestButNoLeft", RpcTarget.All);
            }
            
        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U == true)
            {
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare -= Int32.Parse(inputNum.text);
                photonView.RPC("UpdateEachKeepForInvest", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare); 
                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U = false;
                    photonView.RPC("UpdateStillInvestSell", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber - 1, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U);
                }
                photonView.RPC("UpdateKeepForSellInvestButNoLeft", RpcTarget.All);
            }
            
        }

        photonView.RPC("UpdateMoneySell", RpcTarget.All, GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money, PhotonNetwork.LocalPlayer.ActorNumber - 1);
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
    void UpdateMoneySell (int money, int x)
    {
        GameManager.instace.playerList[x].money = money;
        UIController.instance.MyMoneyText.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
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
    void UpdateSellInvestMoney(int money, int x, int input)
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
    void UpdateEachKeepForInvest(int countShare)
    {
        
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare = countShare;
                
            

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare = countShare;
                
            

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare = countShare;
                
            

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare = countShare;
                
            

        }
    }



    [PunRPC]
    void UpdateStillInvestSell(int x,bool stillHave)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
                    GameManager.instace.playerList[x].hasON2U = stillHave;


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

                    GameManager.instace.playerList[x].hasMYT4U = stillHave;


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

                    GameManager.instace.playerList[x].hasGRO4US = stillHave;

            


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

                    GameManager.instace.playerList[x].hasOK4U = stillHave;

        }

        
    }
    [PunRPC]
    void UpdateKeepForSellInvest(int x)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[x].hasON2U == true)
            {
                
                /*GameManager.instace.playerList[x].ON2UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].ON2UList[0].countShare * GameManager.instace.playerList[x].ON2UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].ON2UList[0].countShare);
                GameManager.instace.playerList[x].ON2UList[0].countShare -= input;*/
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
               /* GameManager.instace.playerList[x].MYT4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].MYT4UList[0].countShare * GameManager.instace.playerList[x].MYT4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].MYT4UList[0].countShare);
                GameManager.instace.playerList[x].MYT4UList[0].countShare -= input;*/
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
               /*GameManager.instace.playerList[x].GRO4USList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].GRO4USList[0].countShare * GameManager.instace.playerList[x].GRO4USList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].GRO4USList[0].countShare);
                GameManager.instace.playerList[x].GRO4USList[0].countShare -= input;*/
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
               /* GameManager.instace.playerList[x].OK4UList[0].pricePerShare = ((input * SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].value)
                    + (GameManager.instace.playerList[x].OK4UList[0].countShare * GameManager.instace.playerList[x].OK4UList[0].pricePerShare))
                    / (input + GameManager.instace.playerList[x].OK4UList[0].countShare);
                GameManager.instace.playerList[x].OK4UList[0].countShare -= input;*/
                if (GameManager.instace.playerList[x].OK4UList[0].countShare == 0)
                {
                    GameManager.instace.playerList[x].OK4UList.RemoveAt(0);
                    GameManager.instace.playerList[x].hasOK4U = false;
                    //GameManager.instace.playerList[x].OK4UList[0].pricePerShare = 0;
                }
            }


        }
    }


    [PunRPC]
    void UpdateKeepForSellInvestButNoLeft()
    {
        //UIController.instance.infoText.text = "in UpdateKeepForSellInvestButNoLeft";
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasON2U == false)
            {
                //UILeftController.instance.ON2Ucount.text = "0";
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList.RemoveAt(0);
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasMYT4U == false)
            {
                //UILeftController.instance.MYT4Ucount.text = "0";
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList.RemoveAt(0);

            } 
            

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {
            //UIController.instance.infoText.text = "in UpdateKeepForSellInvestButNoLeft in card";
            if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasGRO4US == false)
            {
                //UIController.instance.infoText.text = "in UpdateKeepForSellInvestButNoLeft remove";
                //UILeftController.instance.GRO4UScount.text = "0";
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList.RemoveAt(0);

            }


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

                if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].hasOK4U == false)
                {
                //UILeftController.instance.OK4Ucount.text = "0";
                GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList.RemoveAt(0);

                }

        }
    }

    [PunRPC]
    void UpdateSellInvest(int x, int countShare)
    {

        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].ON2U == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasON2U == true)
            {

                GameManager.instace.playerList[GameManager.instace.activePlayer].ON2UList[0].countShare = countShare;

            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].MYT4U == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasMYT4U == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].MYT4UList[0].countShare = countShare;
            }

        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].GRO4US == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasGRO4US == true)
            {

                GameManager.instace.playerList[GameManager.instace.activePlayer].GRO4USList[0].countShare = countShare;
            }


        }
        if (SmallDealDeckController.instance.usedCards[SmallDealDeckController.instance.cardcount - 1].OK4U == true)
        {

            if (GameManager.instace.playerList[GameManager.instace.activePlayer].hasOK4U == true)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].OK4UList[0].countShare = countShare;
            }

        }
    }
}
