using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks

{
    public static GameManager instace;

    [System.Serializable]
    public class DealKeep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public bool house3s2;
        public bool house2s1;
        public bool Condominium;
        public bool CommercialBuilding;
        public bool Apartment;
        public bool Business;
        public int count;
    }

    [System.Serializable]
    public class house3s2Keep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public bool isSelected;

    }
    [System.Serializable]
    public class house2s1Keep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public bool isSelected;
    }

    [System.Serializable]
    public class CondominiumKeep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public int count;
        public bool isSelected;
    }

    [System.Serializable]
    public class CommercialBuildingKeep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public int count;
        public bool isSelected;
    }
    [System.Serializable]
    public class ApartmentKeep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public int count;
        public bool isSelected;
    }
    [System.Serializable]
    public class BusinessKeep
    {
        public string CardName;
        public int BusinessValue;
        public int DownPayment;
        public int BankLoan;
        public int CashflowIncome;

        public bool isSelected;
    }


    [System.Serializable]
    public class OtherInvestKeep
    {
        public string CardName;

    }

    [System.Serializable]
    public class ON2UKeep
    {
        public string CardName;
        public int countShare;
        public float pricePerShare;
    }
    [System.Serializable]
    public class MYT4UKeep
    {
        public string CardName;
        public int countShare;
        public float pricePerShare;

    }
    [System.Serializable]
    public class GRO4USKeep
    {
        public string CardName;
        public int countShare;
        public float pricePerShare;

    }

    [System.Serializable]
    public class OK4UKeep
    {
        public string CardName;
        public int countShare;
        public float pricePerShare;

    }

    [System.Serializable]
    public class Note
    {
        public string CardName;
        public int price;
    }

    [System.Serializable]
    public class Entity
    {

        public string playerName;
        public enum PlayerTypes
        {
            HUMAN, NO_PLAYER
        }
        public PlayerTypes playerType;
        public enum Jobs
        {
            DOCTOR, LAWER, POLICE, TRUCK_DRIVER, TEACHER, MACHANIC, NURSE, SECRETARY, CLEANING_STAFF, MANAGER, PILOT, ENGINEER
        }
        public enum Color
        {
            RED, BLUE, YELLOW, PURPLE, GREEN, ORANGE
        }

        public Jobs playerJob;
        public Color ColorPlayer;

        //��Ѿ���Թ
        public int money;
        public int firstMoney;

        //��áԨ
        //����Ѻ
        public int salary;
        public int income;
        public int allRecieve;
        public int getmoney;
        //��¨��

        public int tax;
        public int homeMortgage;
        public int learnMortgage;
        public int carMortgage;
        public int creditcardMortgage;
        public int extraPay;
        public int loanBank;

        public bool hasChild;
        public int child;
        public int perChild;
        public int sumChild;

        public int paid;

        public int homeDebt;
        public int learnDebt;
        public int carDebt;
        public int creditDebt;
        public int InstallmentsBank;


        public Player1[] myPlayers;
        public bool hasTurn;

        public bool hasOutside;
        public bool hasDonate;
        public bool isClick2Dice;

        public int hasJobCount;
        public int hasDonateCount;

        public bool hasON2U;
        public bool hasMYT4U;
        public bool hasGRO4US;
        public bool hasOK4U;
        public bool hasGoldCoins;
        public bool hasHome32;
        public bool hasHome21;
        public bool hasCondominium21;
        public bool hascommercialBuilding;
        public bool hasApartment;
        public bool hasBusiness;
        public int GoldCoins;

        public bool isInRedRoute;
        public bool isDrawButtonOn;
        public bool isSpendAlready;
        public bool remiderPosition;
        public bool EnterOuter;

        public bool isOpenPage1;
        public List<DealKeep> DealList = new List<DealKeep>();


        public List<ON2UKeep> ON2UList = new List<ON2UKeep>();
        public List<MYT4UKeep> MYT4UList = new List<MYT4UKeep>();
        public List<GRO4USKeep> GRO4USList = new List<GRO4USKeep>();
        public List<OK4UKeep> OK4UList = new List<OK4UKeep>();

        public List<house3s2Keep> house3s2List = new List<house3s2Keep>();
        public List<house2s1Keep> house2s1List = new List<house2s1Keep>();
        public List<CondominiumKeep> CondominiumList = new List<CondominiumKeep>();
        public List<CommercialBuildingKeep> CommercialBuildingList = new List<CommercialBuildingKeep>();
        public List<ApartmentKeep> ApartmentList = new List<ApartmentKeep>();
        public List<BusinessKeep> BusinessList = new List<BusinessKeep>();

        public List<OtherInvestKeep> OtherInvestList = new List<OtherInvestKeep>();
        public List<Note> Keep = new List<Note>();

    }

    public List<Entity> playerList = new List<Entity>();
    public List<Entity> SortplayerList = new List<Entity>();

    //STATEMACHINE
    public enum States
    {
        WAITING, ROLL_DICE, ACTION, SWITCH_PLAYER, START_TURN
    }
    public States state;

    public int activePlayer;
    bool switchingPlayer;
    bool turnPossible = true;

    public GameObject diceButton;
    public GameObject DoublediceButton;
    public Text TurnUI;
    [HideInInspector] public int rolledhumanDice;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;


    //public PhotonView photonView;
    public Dice dice;
    public Dice dice2;

    //private PhotonView photonView;
    void Awake()
    {
        instace = this;

        //photonView = GetComponent<PhotonView>();


    }

    void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();
        playerInRoomChecking();
        RandomFirstPlayer();
        playerJobSetting();
        ActivateButton(false);

    }


    void Update()
    {

        //----------------------------------------------------------------------------HUMAN
        if (playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN && IsMyTurnSure())
        {
            switch (state)
            {
                case States.START_TURN:
                    {
                        //check win condition
                        if (playerList[activePlayer].income > playerList[activePlayer].paid)
                        {
                            playerList[activePlayer].EnterOuter = true;
                            playerList[activePlayer].hasOutside = true;
                            GameManager.instace.playerList[GameManager.instace.activePlayer].playerType = GameManager.Entity.PlayerTypes.NO_PLAYER;
                            GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                            UIController.instance.BlurBg.SetActive(true);
                            UIController.instance.winShow.SetActive(true);
                        }

                        Debug.Log(IsMyTurnSure());
                        Debug.Log("Localplayer now " + PhotonNetwork.LocalPlayer.ActorNumber);
                        Debug.Log("activeplayer now " + activePlayer);
                        playerList[activePlayer].myPlayers[0].SetSelector(true);
                        //check donate
                        if (playerList[activePlayer].hasDonateCount > 0)
                        {
                            playerList[activePlayer].hasDonateCount--;
                            if (playerList[activePlayer].hasDonateCount == 0)
                            {
                                playerList[activePlayer].hasDonate = false;

                            }
                            photonView.RPC("setDonate", RpcTarget.All, playerList[activePlayer].hasDonate, playerList[activePlayer].hasDonateCount);
                        }

                        playerList[activePlayer].myPlayers[0].turncounts++;

                        photonView.RPC("turnCountRPC", RpcTarget.All, playerList[activePlayer].myPlayers[0].turncounts);

                        //check unemployee
                        if (playerList[activePlayer].hasJobCount == 0)
                        {
                            state = States.ROLL_DICE;
                        }
                        else if (playerList[activePlayer].hasJobCount > 0)
                        {
                            playerList[activePlayer].hasJobCount--;
                            photonView.RPC("setJobCount", RpcTarget.All, playerList[activePlayer].hasJobCount);

                            state = States.SWITCH_PLAYER;


                        }

                        playerList[activePlayer].isSpendAlready = false;

                    }
                    break;
                case States.ROLL_DICE:
                    {

                        //Deactivate Highlight
                        ActivateButton(true);
                        photonView.RPC("DiceReset", RpcTarget.All);

                        state = States.WAITING;
                    }
                    break;
                case States.WAITING:
                    {


                    }
                    break;
                case States.ACTION:
                    {

                    }
                    break;
                case States.SWITCH_PLAYER:
                    {

                        //Deactivate button

                        //Deactivate Highlight
                        playerList[activePlayer].myPlayers[0].SetSelector(false);


                        StartCoroutine(SwitchPlayer());

                        state = States.WAITING;

                    }
                    break;
            }

        }
        //----------------------------------------------------------------------------NO PLAYER
        if (playerList[activePlayer].playerType == Entity.PlayerTypes.NO_PLAYER)
        {
            switch (state)
            {
                case States.START_TURN:
                    {
                        state = States.SWITCH_PLAYER;
                    }
                    break;
                case States.ROLL_DICE:
                    {

                    }
                    break;
                case States.WAITING:
                    {

                    }
                    break;
                case States.ACTION:
                    {

                    }
                    break;
                case States.SWITCH_PLAYER:
                    {
                        StartCoroutine(noPlayerPassturn());
                        //state = States.WAITING;
                    }
                    break;
            }
        }
    }

    public void RollDice(int _diceNumber)
    {
        int DiceNumber = _diceNumber;

        if (playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN)
        {
            if (dice2.diceValue > 0)
            {
                _diceNumber = _diceNumber + dice2.diceValue;
            }
            rolledhumanDice = _diceNumber;

            photonView.RPC("HumanRollD", RpcTarget.All);
            //HumanRollDice();
        }
        Debug.Log("Dice Rolled number : " + DiceNumber);
        UIController.instance.showMessage("Roll Dice Number:" + _diceNumber);
    }


    IEnumerator SwitchPlayer()
    {

        Debug.Log(activePlayer);
        if (switchingPlayer || GameManager.instace.activePlayer != activePlayer)
        {
            yield break;
        }
        switchingPlayer = true;

        yield return new WaitForSeconds(2);

        //SET NEXT PLAYER
        SetNextActivePlayer();
        //Debug.Log(activePlayer);
        //playerInRoomChecking();
        switchingPlayer = false;


    }
    IEnumerator noPlayerPassturn()
    {
        if (switchingPlayer)
        {
            yield break;
        }
        switchingPlayer = true;
        //SET NEXT PLAYER

        SetNextActivePlayer();

        switchingPlayer = false;
    }


    void SetNextActivePlayer()
    {
        //activePlayer++;
        //activePlayer %= playerList.Count;
        photonView.RPC("nextPlayerSetRPC", RpcTarget.All);
        //photonView.RPC("nextPlayerRPC", RpcTarget.All, activePlayer);
        /*
        int available = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].hasOutside)
            {
                available++;
            }
        }
        //out
        if (playerList[activePlayer].hasOutside && available > 1)
        {
            SetNextActivePlayer();
            return;
        }
        else if(available<2)
        {
            //last one player
            state = States.WAITING;
            return;
        }*/


    }

    public void ReportTurnPossible(bool possible)
    {

        turnPossible = possible;

    }


    public void ReportWinning()
    {
        //show some ui
        playerList[activePlayer].hasOutside = true;
        /*
                for (int i = 0; i < SaveSettings.winners.Length; i++)
                {
                    if (SaveSettings.winners[i] == "")
                    {
                        SaveSettings.winners[i] = playerList[activePlayer].playerName;
                        break;
                    }
                }*/

    }

    public static class RandomEnum
    {
        private static System.Random _Random = new System.Random(Environment.TickCount);

        public static T Of<T>()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException("Must use Enum type");

            Array enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(_Random.Next(enumValues.Length));
        }
    }

    //---------------------------human input--------------------
    public void ActivateButton(bool on)
    {
        diceButton.SetActive(on);
        if (playerList[activePlayer].hasDonate)
        {
            DoublediceButton.SetActive(on);
        }
    }

    public void DeactivateAllSelector()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            for (int j = 0; j < playerList[i].myPlayers.Length; j++)
            {
                playerList[i].myPlayers[j].SetSelector(false);
            }
        }
        //photonView.RPC("endTurn", RpcTarget.All,IsMyTurnSure());
    }

    public void HumanRoll()
    {
        photonView.RPC("rollDice", RpcTarget.All);
        ActivateButton(false);
    }

    public void DoubleRoll()
    {
        photonView.RPC("rollDice", RpcTarget.All);
        photonView.RPC("rollDice2", RpcTarget.All);
        playerList[activePlayer].isClick2Dice = true;
        photonView.RPC("CheckClick2Dice", RpcTarget.All, playerList[activePlayer].isClick2Dice);
        ActivateButton(false);
    }
    public void PassTurn()
    {
        UIController.instance.passButton.SetActive(false);
        playerList[activePlayer].isInRedRoute = false;

        photonView.RPC("setOtherOff", RpcTarget.All);
        state = States.SWITCH_PLAYER;

    }

    public void HumanRollDice()
    {

        List<Player1> moveablePlayers = new List<Player1>();
        moveablePlayers.Add(playerList[activePlayer].myPlayers[0]);

        for (int i = 0; i < moveablePlayers.Count; i++)
        {
            if (moveablePlayers.Count > 0)
            {
                moveablePlayers[i].SetSelector(true);
                moveablePlayers[i].tohasturn();

            }
            else
            {
                //photonView.RPC("nextPlayerRPC", RpcTarget.All, activePlayer);
                state = States.SWITCH_PLAYER;
            }
        }
    }

    List<Player1> PossiblePlayer()
    {
        List<Player1> tempList = new List<Player1>();

        tempList.Add(playerList[activePlayer].myPlayers[0]);
        return tempList;
    }


    public void RandomFirstPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomPlayer = Random.Range(0, playerList.Count);
            for (int i = 0; i < GameManager.instace.playerList.Count; i++)
            {
                photonView.RPC("SetStartingPlayer", RpcTarget.All, randomPlayer);
            }

        }
    }

    public void playerInRoomChecking()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("1 player in room");
            playerList[1].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[2].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[3].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);
            player5.SetActive(false);
            player6.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("2 player in room");

            playerList[2].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[3].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
            player3.SetActive(false);
            player4.SetActive(false);
            player5.SetActive(false);
            player6.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            Debug.Log("3 player in room");

            playerList[3].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
            player4.SetActive(false);
            player5.SetActive(false);
            player6.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            Debug.Log("4 player in room");

            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
            player5.SetActive(false);
            player6.SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            //SaveSettings.players[5] = "NP";
            Debug.Log("5 player in room");

            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
            player6.SetActive(false);
        }
    }


    public void playerJobSetting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < GameManager.instace.playerList.Count; i++)
            {
                GameManager.instace.playerList[i].playerJob = GameManager.RandomEnum.Of<GameManager.Entity.Jobs>();
                photonView.RPC("SetPlayerJob", RpcTarget.All, playerList[i].playerJob, i);
                //CheckingJob(i);
            }

        }

    }

    private bool IsMyTurnSure()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return GameManager.instace.activePlayer == PhotonNetwork.LocalPlayer.ActorNumber - 1;
    }

    public void CheckingJob(int i)
    {
        // DOCTOR, LAWER, POLICE, TRUCK_DRIVER, TEACHER, MACHANIC, NURSE, SECRETARY, CLEANING_STAFF, MANAGER, PILOT, ENGINEER
        //---------------------------------------------------------------------doctor-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.DOCTOR)
        {

            GameManager.instace.playerList[i].firstMoney = 35000;



            GameManager.instace.playerList[i].salary = 132000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;

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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;

            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------lawer-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.LAWER)
        {
            GameManager.instace.playerList[i].firstMoney = 20000;

            GameManager.instace.playerList[i].salary = 75000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;

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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------police-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.POLICE)
        {
            GameManager.instace.playerList[i].firstMoney = 5000;

            GameManager.instace.playerList[i].salary = 30000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------truck driver-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TRUCK_DRIVER)
        {
            GameManager.instace.playerList[i].firstMoney = 8000;

            GameManager.instace.playerList[i].salary = 25000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------teacher-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TEACHER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;

            GameManager.instace.playerList[i].salary = 33000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------machanic-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MACHANIC)
        {
            GameManager.instace.playerList[i].firstMoney = 7000;

            GameManager.instace.playerList[i].salary = 20000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------nurse-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.NURSE)
        {
            GameManager.instace.playerList[i].firstMoney = 5000;

            GameManager.instace.playerList[i].salary = 31000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------secretary-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.SECRETARY)
        {
            GameManager.instace.playerList[i].firstMoney = 7000;

            GameManager.instace.playerList[i].salary = 25000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------cleaning staff-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.CLEANING_STAFF)
        {
            GameManager.instace.playerList[i].firstMoney = 6000;

            GameManager.instace.playerList[i].salary = 16000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------manager-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MANAGER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;

            GameManager.instace.playerList[i].salary = 46000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------pilot-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.PILOT)
        {
            GameManager.instace.playerList[i].firstMoney = 25000;

            GameManager.instace.playerList[i].salary = 95000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;
        }
        //---------------------------------------------------------------------engineer-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.ENGINEER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;

            GameManager.instace.playerList[i].salary = 49000;
            GameManager.instace.playerList[i].income = 0;
            GameManager.instace.playerList[i].allRecieve = GameManager.instace.playerList[i].salary + GameManager.instace.playerList[i].income;
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

            GameManager.instace.playerList[i].getmoney = GameManager.instace.playerList[i].allRecieve - GameManager.instace.playerList[i].paid;

            GameManager.instace.playerList[i].hasJobCount = 0;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney + GameManager.instace.playerList[i].getmoney;

        }


    }


    [PunRPC]
    void SetPlayerJob(Entity.Jobs jobPlayer, int i)
    {
        Debug.Log("SetPlayerJob RPC called with jobPlayer: " + jobPlayer);
        playerList[i].playerJob = jobPlayer;
        CheckingJob(i);
    }
    [PunRPC]
    void turnCountRPC(int turncount)
    {
        playerList[activePlayer].myPlayers[0].turncounts = turncount;
        TurnUI.text = "Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts;
        Debug.Log("Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);
    }
    [PunRPC]
    void nextPlayerRPC(int nextPlayer)
    {
        activePlayer = nextPlayer;

    }
    [PunRPC]
    void nextPlayerSetRPC()
    {
        activePlayer++;
        while (playerList[activePlayer].playerType == Entity.PlayerTypes.NO_PLAYER)
        {
            activePlayer++;

            if (activePlayer >= playerList.Count)
            {
                activePlayer = 0;
            }
            //activePlayer %= playerList.Count;
        }
        //activePlayer %= playerList.Count;
        state = States.START_TURN;
    }

    [PunRPC]
    void SetStartingPlayer(int startingPlayer)
    {
        activePlayer = startingPlayer;
        while (playerList[activePlayer].playerType == Entity.PlayerTypes.NO_PLAYER)
        {
            activePlayer++;
            /*
            if (activePlayer >= playerList.Count)
            {
                activePlayer = 0;
            }*/
            activePlayer %= playerList.Count;
        }
        Debug.Log("SetStartingPlayer RPC : " + activePlayer);
        UIController.instance.showMessage("Player " + activePlayer + " starts first");
        TurnUI.text = "Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts;
        Debug.Log("Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);

    }

    [PunRPC]
    void rollDice()
    {
        dice.RollDice();
    }

    [PunRPC]
    void rollDice2()
    {
        dice2.RollDice();
    }
    [PunRPC]
    void DiceReset()
    {
        dice.Reset();
        dice2.Reset();
    }


    [PunRPC]
    void HumanRollD()
    {
        List<Player1> moveablePlayers = new List<Player1>();
        moveablePlayers.Add(playerList[activePlayer].myPlayers[0]);
        //moveablePlayers[activePlayer].SetSelector(true);
        //moveablePlayers[activePlayer].tohasturn();

        for (int i = 0; i < moveablePlayers.Count; i++)
        {
            if (moveablePlayers.Count > 0)
            {
                moveablePlayers[i].SetSelector(true);
                moveablePlayers[i].tohasturn();

            }
            else
            {
                state = States.SWITCH_PLAYER;
            }
        }
    }

    [PunRPC]
    void endTurn(bool on)
    {
        playerList[activePlayer].hasTurn = on;
    }

    [PunRPC]
    void setOtherOff()
    {
        UIController.instance.SetAllFalse(false);
    }

    [PunRPC]
    void setJobCount(int count)
    {
        playerList[activePlayer].hasJobCount = count;

    }

    [PunRPC]
    void setDonate(bool isDonate, int count)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonate = isDonate;
        GameManager.instace.playerList[GameManager.instace.activePlayer].hasDonateCount = count;
    }

    [PunRPC]
    void CheckClick2Dice(bool is2dice)
    {
        GameManager.instace.playerList[GameManager.instace.activePlayer].isClick2Dice = is2dice;
    }
}
