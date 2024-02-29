using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UILeftController : MonoBehaviourPunCallbacks
{
    public static UILeftController instance;

    public Canvas page1;
    public Canvas page2;

    public GameObject set1;
    public GameObject set2;
    public GameObject set3;


    public GameObject opPage1;
    public GameObject opPage2;
    public GameObject opPage1_1;
    public GameObject opPage2_1;
    public GameObject Loan1;
    public GameObject Loan2;

    public GameObject housePay;
    public GameObject learnPay;
    public GameObject carPay;
    public GameObject creditPay;
    public GameObject loanPay;

    public TMP_Text ON2Ucount;
    public TMP_Text ON2U;
    public TMP_Text GRO4UScount;
    public TMP_Text GRO4US;
    public TMP_Text OK4U;
    public TMP_Text OK4Ucount;
    public TMP_Text MYT4U;
    public TMP_Text MYT4Ucount;

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
    public TMP_Text MyMoney;
    public TMP_Text Goldcoins;
    public TMP_Text firstmoneyP2;
    public TMP_Text leftmoneyP2;
    public TMP_Text allMoneyP2;

    // Start is called before the first frame update
    void Start()
    {
        

        page1.enabled = false;
        page2.enabled = false;

        set1.SetActive(true);

        SetPage1Value();
        SetPage2Value();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (page2.enabled == true)
        {
            SetPage2Value();
        }
    }


    public void OperPage1()
    {

        SetPage1Value();

        page1.enabled = true;
        page2.enabled = false;
        set1.SetActive(false);

    }

    public void OperPage2()
    {
        SetPage2Value();
        page1.enabled = false;
        page2.enabled = true;
        set1.SetActive(false);

    }

    public void closePage1()
    {
        SetPage1Value();
        SetPage2Value();
        if (page1.enabled == false && page2.enabled == true)
        {
            page1.enabled = true;
            page2.enabled = false;
            set1.SetActive(false);
        }
        else
        {
            page1.enabled = false;
            page2.enabled = false;
            set1.SetActive(true);
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
            set1.SetActive(true);
        }
        else
        {
            page1.enabled = false;
            page2.enabled = true;
            set1.SetActive(false);
        }

    }

    public void OpenLoanCanvas()
    {
        UIController.instance.LoanCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }

    public void SetPage1Value()
    {
        firstmoneyP2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].SaveFirstMoney.ToString();
        leftmoneyP2.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].SaveGetMoney).ToString();
        allMoneyP2.text = (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].SaveAllMoney).ToString();
    }
    public void SetPage2Value()
    {
        Job.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].playerJob.ToString();
        MyMoney.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].money.ToString();
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

        Goldcoins.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GoldCoins.ToString();

        child1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].child.ToString();
        child2.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].perChild.ToString();
        allChild.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild.ToString();
        money.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].firstMoney.ToString();
        house1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt.ToString();
        learn1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt.ToString();
        car1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt.ToString();
        creditCard1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditDebt.ToString();
        loan1.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank.ToString();

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt == 0)
        {
            housePay.SetActive(false);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeDebt > 0)
        {
            housePay.SetActive(true);
        }

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt == 0)
        {
            learnPay.SetActive(false);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnDebt > 0)
        {
            learnPay.SetActive(true);
        }

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt == 0)
        {
            carPay.SetActive(false);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carDebt > 0)
        {
            carPay.SetActive(true);
        }

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditDebt == 0)
        {
            creditPay.SetActive(false);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditDebt > 0)
        {
            creditPay.SetActive(true);
        }

        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank == 0)
        {
            loanPay.SetActive(false);
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank > 0)
        {
            loanPay.SetActive(true);
        }

        if(GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList.Count > 0)
        {
            ON2Ucount.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].countShare.ToString();
            ON2U.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].ON2UList[0].pricePerShare.ToString();
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList.Count > 0)
        {
            GRO4UScount.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].countShare.ToString();
            GRO4US.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].GRO4USList[0].pricePerShare.ToString();
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList.Count > 0)
        {
            OK4Ucount.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].countShare.ToString();
            OK4U.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].OK4UList[0].pricePerShare.ToString();
        }
        if (GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList.Count > 0)
        {
            MYT4Ucount.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].countShare.ToString();
            MYT4U.text = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].MYT4UList[0].pricePerShare.ToString();
        }
}

    public void PayLoan()
    {
        UIController.instance.PayLoanCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }
    public void PayHouseDebt()
    {
        UIController.instance.PayHouseDebtCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }
    public void PayLearnDebt()
    {
        UIController.instance.PayLearnDebtCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }
    public void PayCarDebt()
    {
        UIController.instance.PayCarDebtCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }
    public void PayCreditDebt()
    {
        UIController.instance.PayCreditDebtCanvas.SetActive(true);
        UIController.instance.BlurBg.SetActive(true);
    }

    [PunRPC]
    public void setIsOpen(int x,bool isOpen)
    {
        GameManager.instace.playerList[x].isOpenPage1 = isOpen;
    }
    
}

