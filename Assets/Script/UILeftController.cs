using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UILeftController : MonoBehaviour
{
    public static UIController instance;

    public Canvas page1;
    public Canvas page2;

    public GameObject opPage1;
    public GameObject opPage2;
    public GameObject opPage1_1;
    public GameObject opPage2_1;

    public TMP_Text income1;
    public TMP_Text income2;
    public TMP_Text salary1;
    public TMP_Text salary2;
    public TMP_Text allGet;
    public TMP_Text allPay;
    public TMP_Text incomeLeft;
    public TMP_Text tax;
    public TMP_Text house;
    public TMP_Text learn;
    public TMP_Text car;
    public TMP_Text creditCard;
    public TMP_Text addOn;
    public TMP_Text loan;
    public TMP_Text child1;
    public TMP_Text child2;
    public TMP_Text allChild;
    public TMP_Text money;
    public TMP_Text house1;
    public TMP_Text learn1;
    public TMP_Text car1;
    public TMP_Text creditCard1;
    public TMP_Text loan1;
    public TMP_Text Job;

    public TMP_Text firstmoneyP2;
    public TMP_Text leftmoneyP2;
    public TMP_Text allMoneyP2;

    // Start is called before the first frame update
    void Start()
    {
        page1.enabled = false;
        page2.enabled = false;

        opPage1.SetActive(true);
        opPage2.SetActive(true);
        opPage1_1.SetActive(false);
        opPage2_1.SetActive(false);

        SetPage1Value();
        SetPage2Value();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OperPage1()
    {
        SetPage1Value();
        page1.enabled = true;
        page2.enabled = false;

        opPage1.SetActive(false);
        opPage2.SetActive(false);
        opPage1_1.SetActive(true);
        opPage2_1.SetActive(true);
}

    public void OperPage2()
    {
        SetPage2Value();
        page1.enabled = false;
        page2.enabled = true;

        opPage1.SetActive(false);
        opPage2.SetActive(false);
        opPage1_1.SetActive(true);
        opPage2_1.SetActive(true);
        
    }

    public void closePage1()
    {
        SetPage1Value();
        SetPage2Value();
        if (page1.enabled == false && page2.enabled == true)
        {
            page1.enabled = true;
            page2.enabled = false;

            opPage1.SetActive(false);
            opPage2.SetActive(false);
            opPage1_1.SetActive(true);
            opPage2_1.SetActive(true);
        }
        else
        {
            page1.enabled = false;
            page2.enabled = false;

            opPage1.SetActive(true);
            opPage2.SetActive(true);
            opPage1_1.SetActive(false);
            opPage2_1.SetActive(false);
            
        }

    }

    public void closePage2()
    {
        SetPage1Value();
        SetPage2Value();
        if (page1.enabled == false && page2.enabled == true)
        {
            page1.enabled = false;
            page2.enabled = false;

            opPage1.SetActive(true);
            opPage2.SetActive(true);
            opPage1_1.SetActive(false);
            opPage2_1.SetActive(false);
        }
        else
        {
            page1.enabled = false;
            page2.enabled = true;

            opPage1.SetActive(false);
            opPage2.SetActive(false);
            opPage1_1.SetActive(true);
            opPage2_1.SetActive(true);
        }

    }
    public void SetPage1Value()
    {
     firstmoneyP2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].firstMoney.ToString();
    leftmoneyP2.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney).ToString();
    allMoneyP2.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].firstMoney + (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney)).ToString();
}
    public void SetPage2Value()
    {
        Job.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].playerJob.ToString() +" actorNum "+ PhotonNetwork.LocalPlayer.ActorNumber;
        income1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income.ToString();
        income2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income.ToString();

        salary1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary.ToString();
        salary2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary.ToString();

        allGet.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve.ToString();
        allPay.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid.ToString();
        incomeLeft.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney.ToString();
        tax.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].tax.ToString();
        house.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage.ToString();
        learn.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage.ToString();
        car.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage.ToString();
        creditCard.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditcardMortgage.ToString();
        addOn.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].extraPay.ToString();
        loan.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank.ToString();

        child1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].child.ToString();
        child2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].perChild.ToString();
        allChild.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild.ToString();
        money.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].firstMoney.ToString();
        house1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt.ToString();
        learn1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt.ToString();
        car1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt.ToString();
        creditCard1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditDebt.ToString();
        loan1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank.ToString();
    
    }
}

