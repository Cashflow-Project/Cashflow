using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instace;
    public int playerid;

    [Header("ROUTES")]
    public Route commonRoute;
    public Route outerRoute;
    public Route StartRoute;

    public List<Node> outterRoute = new List<Node>();
    public List<Node> fullRoute = new List<Node>();
    [Header("NODES")]
    public Node baseNode;
    public Node startNode;
    public Node currentNode;
    public Node lastNode;

    int routePosition;
    int startNodeIndex;

    int steps;
    int doneSteps;
    public int turncounts = 1;

    [Header("BOOLS")]
    public bool isOut = true;
    bool isMoving;

    bool hasTurn;//human input


    [Header("SELECTOR")]
    public GameObject selector;

    float amplitude = 0.5f;
    float cTime = 0f;

    void Start()
    {
        startNodeIndex = commonRoute.RequestPosition(startNode.gameObject.transform);

        CreateFullRoute();

        for(int i = 0; i< GameManager.instace.playerList.Count ;i++)
        {
            GameManager.instace.playerList[i].playerJob = GameManager.RandomEnum.Of<GameManager.Entity.Jobs>();

            // DOCTOR, LAWER, POLICE, TRUCK_DRIVER, TEACHER, MACHANIC, NURSE, SECRETARY, CLEANING_STAFF, MANAGER, PILOT, ENGINEER
            //---------------------------------------------------------------------doctor-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.DOCTOR)
            {
                GameManager.instace.playerList[i].money = 35000;
                GameManager.instace.playerList[i].salary = 132000;

                GameManager.instace.playerList[i].tax = 32000;
                GameManager.instace.playerList[i].homeMortgage = 19000;
                GameManager.instace.playerList[i].learnMortgage = 7000;
                GameManager.instace.playerList[i].carMortgage = 3000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 20000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 2020000;
                GameManager.instace.playerList[i].learnDebt = 1500000;
                GameManager.instace.playerList[i].carDebt = 190000;
                GameManager.instace.playerList[i].creditDebt = 100000;

                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 7000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;

            }
            //---------------------------------------------------------------------lawer-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.LAWER)
            {
                GameManager.instace.playerList[i].money = 20000;
                GameManager.instace.playerList[i].salary = 75000;

                GameManager.instace.playerList[i].tax = 18000;
                GameManager.instace.playerList[i].homeMortgage = 11000;
                GameManager.instace.playerList[i].learnMortgage = 3000;
                GameManager.instace.playerList[i].carMortgage = 2000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 15000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 1150000;
                GameManager.instace.playerList[i].learnDebt = 780000;
                GameManager.instace.playerList[i].carDebt = 110000;
                GameManager.instace.playerList[i].creditDebt = 70000;

                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 4000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------police-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.POLICE)
            {
                GameManager.instace.playerList[i].money = 5000;
                GameManager.instace.playerList[i].salary = 30000;

                GameManager.instace.playerList[i].tax = 6000;
                GameManager.instace.playerList[i].homeMortgage = 4000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 1000;
                GameManager.instace.playerList[i].extraPay = 7000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 460000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 50000;
                GameManager.instace.playerList[i].creditDebt = 30000;

                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 2000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------truck driver-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TRUCK_DRIVER)
            {
                GameManager.instace.playerList[i].money = 8000;
                GameManager.instace.playerList[i].salary = 25000;

                GameManager.instace.playerList[i].tax = 5000;
                GameManager.instace.playerList[i].homeMortgage = 4000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 1000;
                GameManager.instace.playerList[i].extraPay = 6000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 380000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 40000;
                GameManager.instace.playerList[i].creditDebt = 30000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 2000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------teacher-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TEACHER)
            {
                GameManager.instace.playerList[i].money = 4000;
                GameManager.instace.playerList[i].salary = 33000;

                GameManager.instace.playerList[i].tax = 5000;
                GameManager.instace.playerList[i].homeMortgage = 5000;
                GameManager.instace.playerList[i].learnMortgage = 1000;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 7000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 500000;
                GameManager.instace.playerList[i].learnDebt = 120000;
                GameManager.instace.playerList[i].carDebt = 50000;
                GameManager.instace.playerList[i].creditDebt = 40000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 2000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------machanic-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MACHANIC)
            {
                GameManager.instace.playerList[i].money = 7000;
                GameManager.instace.playerList[i].salary = 20000;

                GameManager.instace.playerList[i].tax = 4000;
                GameManager.instace.playerList[i].homeMortgage = 3000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 1000;
                GameManager.instace.playerList[i].extraPay = 4000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 310000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 30000;
                GameManager.instace.playerList[i].creditDebt = 30000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 1000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------nurse-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.NURSE)
            {
                GameManager.instace.playerList[i].money = 5000;
                GameManager.instace.playerList[i].salary = 31000;

                GameManager.instace.playerList[i].tax = 6000;
                GameManager.instace.playerList[i].homeMortgage = 4000;
                GameManager.instace.playerList[i].learnMortgage = 1000;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 6000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 470000;
                GameManager.instace.playerList[i].learnDebt = 60000;
                GameManager.instace.playerList[i].carDebt = 50000;
                GameManager.instace.playerList[i].creditDebt = 40000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 2000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------secretary-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.SECRETARY)
            {
                GameManager.instace.playerList[i].money = 7000;
                GameManager.instace.playerList[i].salary = 25000;

                GameManager.instace.playerList[i].tax = 5000;
                GameManager.instace.playerList[i].homeMortgage = 4000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 1000;
                GameManager.instace.playerList[i].extraPay = 6000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 380000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 40000;
                GameManager.instace.playerList[i].creditDebt = 30000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 1000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------cleaning staff-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.CLEANING_STAFF)
            {
                GameManager.instace.playerList[i].money = 6000;
                GameManager.instace.playerList[i].salary = 16000;

                GameManager.instace.playerList[i].tax = 3000;
                GameManager.instace.playerList[i].homeMortgage = 2000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 1000;
                GameManager.instace.playerList[i].extraPay = 3000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 200000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 40000;
                GameManager.instace.playerList[i].creditDebt = 30000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 1000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------manager-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MANAGER)
            {
                GameManager.instace.playerList[i].money = 4000;
                GameManager.instace.playerList[i].salary = 46000;

                GameManager.instace.playerList[i].tax = 9000;
                GameManager.instace.playerList[i].homeMortgage = 7000;
                GameManager.instace.playerList[i].learnMortgage = 1000;
                GameManager.instace.playerList[i].carMortgage = 1000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 10000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 750000;
                GameManager.instace.playerList[i].learnDebt = 120000;
                GameManager.instace.playerList[i].carDebt = 60000;
                GameManager.instace.playerList[i].creditDebt = 40000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 3000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------pilot-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.PILOT)
            {
                GameManager.instace.playerList[i].money = 25000;
                GameManager.instace.playerList[i].salary = 95000;

                GameManager.instace.playerList[i].tax = 20000;
                GameManager.instace.playerList[i].homeMortgage = 10000;
                GameManager.instace.playerList[i].learnMortgage = 0;
                GameManager.instace.playerList[i].carMortgage = 3000;
                GameManager.instace.playerList[i].creditcardMortgage = 7000;
                GameManager.instace.playerList[i].extraPay = 20000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 900000;
                GameManager.instace.playerList[i].learnDebt = 0;
                GameManager.instace.playerList[i].carDebt = 150000;
                GameManager.instace.playerList[i].creditDebt = 220000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 4000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;
            }
            //---------------------------------------------------------------------engineer-----------------------------------------------------------------
            if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.ENGINEER)
            {
                GameManager.instace.playerList[i].money = 4000;
                GameManager.instace.playerList[i].salary = 49000;

                GameManager.instace.playerList[i].tax = 10000;
                GameManager.instace.playerList[i].homeMortgage = 7000;
                GameManager.instace.playerList[i].learnMortgage = 1000;
                GameManager.instace.playerList[i].carMortgage = 2000;
                GameManager.instace.playerList[i].creditcardMortgage = 2000;
                GameManager.instace.playerList[i].extraPay = 10000;
                GameManager.instace.playerList[i].InstallmentsBank = GameManager.instace.playerList[i].loanBank * (1 / 10);

                GameManager.instace.playerList[i].homeDebt = 750000;
                GameManager.instace.playerList[i].learnDebt = 120000;
                GameManager.instace.playerList[i].carDebt = 70000;
                GameManager.instace.playerList[i].creditDebt = 50000;
                GameManager.instace.playerList[i].loanBank = 0;

                GameManager.instace.playerList[i].child = 0;
                GameManager.instace.playerList[i].perChild = 2000;
                GameManager.instace.playerList[i].sumChild = GameManager.instace.playerList[i].child * GameManager.instace.playerList[i].perChild;

                GameManager.instace.playerList[i].paid = GameManager.instace.playerList[i].tax + GameManager.instace.playerList[i].homeMortgage + GameManager.instace.playerList[i].learnMortgage + GameManager.instace.playerList[i].carMortgage + GameManager.instace.playerList[i].creditcardMortgage + GameManager.instace.playerList[i].extraPay + GameManager.instace.playerList[i].InstallmentsBank + GameManager.instace.playerList[i].sumChild;

                GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].salary - GameManager.instace.playerList[i].paid;

                GameManager.instace.playerList[i].hasJob1 = true;
                GameManager.instace.playerList[i].hasJob2 = true;
                GameManager.instace.playerList[i].hasChild = false;
                GameManager.instace.playerList[i].hasDonate = false;
                GameManager.instace.playerList[i].hasOutside = false;

            }

            
        }

    SetSelector(false);
    }

    void CreateFullRoute()
    {
        for (int i = 0; i < commonRoute.childNodeList.Count; i++)
        {
            int tempPos = startNodeIndex + i;
            tempPos %= commonRoute.childNodeList.Count;

            fullRoute.Add(commonRoute.childNodeList[tempPos+1].GetComponent<Node>());
        }
        
        for (int i = 0; i < outerRoute.childNodeList.Count; i++)
        {
            outterRoute.Add(outerRoute.childNodeList[i].GetComponent<Node>());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = UnityEngine.Random.Range(1, 7); 
            //turncounts++;
            Debug.Log("Turns" + turncounts);
            Debug.Log("Dice output" + steps);
            /*if(turncounts == 1)
            {
                steps--;
            }
            StartCoroutine(Move());*/
            /**
            if (doneSteps + steps < fullRoute.Count)
            {
                StartCoroutine(Move());
            }else
            {
                Debug.Log("Nuber is to high");
            }**/
            
        }

        
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            routePosition++;
            routePosition %= fullRoute.Count;
            Vector3 nextPos = fullRoute[routePosition].gameObject.transform.position;

            Vector3 startPos = fullRoute[routePosition].gameObject.transform.position;
            //while (MoveToNextNode(nextPos,8f)){yield return null;}
            while (MoveInArcToNextNode(startPos, nextPos, 8f)) { yield return null; }
            //orange pass
            if (routePosition % fullRoute.Count == 6 || routePosition % fullRoute.Count == 14 || routePosition % fullRoute.Count == 22)
            {
                Debug.Log("pass orange route");
                UIController.instance.passButton.SetActive(true);
                GameManager.instace.playerList[GameManager.instace.activePlayer].money = GameManager.instace.playerList[GameManager.instace.activePlayer].money + GameManager.instace.playerList[GameManager.instace.activePlayer].getmoney;
                //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            yield return new WaitForSeconds(0.1f);
            cTime = 0;
            steps--;
            doneSteps++;
            //Debug.Log(doneSteps);
        }

        
        /** lastNode = fullRoute[routePosition];
        if (lastNode.isTaken)
        {
            //return to start base node
            lastNode.player.ReturnToBase();
        }
        currentNode.player = null;
        currentNode.isTaken = false;

        lastNode.player = this;
        lastNode.isTaken = true;

        currentNode = lastNode;
        lastNode = null;**/
        /**
        if (winCondition())
        {
            GameManager.instace.ReportWinning();
        }**/
        
        isMoving = false;
        //green route
        if (routePosition % 2 == 1)
        {
            Debug.Log("in green route");
            UIController.instance.passButton.SetActive(true);
            //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        //red route
        if (routePosition % fullRoute.Count == 2 || routePosition % fullRoute.Count == 10 || routePosition % fullRoute.Count == 18)
        {
            Debug.Log("in red route");
            UIController.instance.drawButton.SetActive(true);
        }
        
        //blue route
        if (routePosition % fullRoute.Count == 8 || routePosition % fullRoute.Count == 16 || routePosition % fullRoute.Count == 24)
        {
            Debug.Log("in blue route");
            UIController.instance.passButton.SetActive(true);
            //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        //purple1 route
        if (routePosition % fullRoute.Count == 4 )
        {
            Debug.Log("in purple 1 route");
            UIController.instance.passButton.SetActive(true);
            //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        //purple2 route
        if (routePosition % fullRoute.Count == 12)
        {
            Debug.Log("in purple 2 route");
            UIController.instance.passButton.SetActive(true);
            //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasJob1 = false;
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasJob2 = false;
        }
        //purple3 route
        if (routePosition % fullRoute.Count == 20)
        {
            Debug.Log("in purple 3 route");
            UIController.instance.passButton.SetActive(true);
            GameManager.instace.playerList[GameManager.instace.activePlayer].hasChild = true;
            if(GameManager.instace.playerList[GameManager.instace.activePlayer].child <= 3)
            {
                GameManager.instace.playerList[GameManager.instace.activePlayer].child += 1;
            }
            
            //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        }
        //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;

    }
    bool MoveToNextNode(Vector3 lastPos,float speed)
    {
        return lastPos != (transform.position = Vector3.MoveTowards(transform.position, lastPos, speed * Time.deltaTime));

    }

    bool MoveInArcToNextNode(Vector3 startPos,Vector3 lastPos,float speed)
    {
        cTime += speed * Time.deltaTime;
        Vector3 myPos = Vector3.Lerp(startPos, lastPos, cTime);

        myPos.y += amplitude * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

        

        return lastPos != (transform.position = Vector3.Lerp(transform.position, myPos, cTime));
    }

    

    public bool ReturnIsOut()
    {
        return isOut;
    }

    public void leaveBase()
    {
        steps = 0;
        isOut = true;
        routePosition = 0;
        //start coroutine
        
        //StartCoroutine(MoveOut());
    }

    IEnumerator MoveOut()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            //routePosition++;
            routePosition %= fullRoute.Count;
            Vector3 nextPos = fullRoute[routePosition].gameObject.transform.position;
            //while (MoveToNextNode(nextPos, 8f)) { yield return null; }
            Vector3 startPos = baseNode.gameObject.transform.position;
            while (MoveInArcToNextNode(startPos, nextPos, 4f)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            cTime = 0;
            steps--;
            doneSteps++;

        }

        //update node
        /**lastNode = fullRoute[routePosition];
         if (lastNode.isTaken)
         {
             //return to start base node
            lastNode.player.ReturnToBase();
         }
         lastNode.player = this;
         lastNode.isTaken = true;

         currentNode = lastNode;
         lastNode = null;

        **/
        //report to game manager
        GameManager.instace.state = GameManager.States.ROLL_DICE;
        isMoving = false;

    }


    public bool CheckPossible(int DiceNumber)
    {
        int tempPos = routePosition + DiceNumber;
        if(tempPos >= fullRoute.Count)
        {
            return true;
        }
        return !fullRoute[tempPos].isTaken;
    }

    public bool CheckPossibleKick(int playerid,int DiceNumber)
    {
        int tempPos = routePosition + DiceNumber;
        if (tempPos >= fullRoute.Count)
        {
            return false;
        }
        if (fullRoute[tempPos].isTaken)
        {
            if(playerid == fullRoute[tempPos].player.playerid)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public void StartTheMove(int DiceNumber)
    {
        steps = DiceNumber;
        if (turncounts == 1)
        {
            //steps--;
            
            
        }
        //Debug.Log("Turns player "+ playerid + " Turn'"+ turncounts);
        StartCoroutine(Move());
        //turncounts++;
    }

    public void ReturnToBase()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        GameManager.instace.ReportTurnPossible(false);
        routePosition = 0;
        currentNode = null;
        lastNode = null;
        isOut = false;
        doneSteps = 0;

        Vector3 baseNodePos = baseNode.gameObject.transform.position;
        while (MoveToNextNode(baseNodePos, 100f))
        {
            yield return null;
        }
        GameManager.instace.ReportTurnPossible(true);
    }

    bool winCondition()
    {
        for (int i = 0; i < outerRoute.childNodeList.Count; i++)
        {
            if (!outerRoute.childNodeList[i].GetComponent<Node>().isTaken)
            {
                return false;
            }
        }
        return true;
    }

    //---------------------------------Human input------------------------------
    public void SetSelector(bool on)
    {
        selector.SetActive(on);
        hasTurn = on;
        

    }
  
    public void tohasturn()
    {
        if (hasTurn)
        {
            StartTheMove(GameManager.instace.rolledhumanDice);
        }
        GameManager.instace.DeactivateAllSelector();
    }
}

 