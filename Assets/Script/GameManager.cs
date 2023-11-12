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

        //ทรัพย์สิน
        public int money;
        public int firstMoney;

        //ธุรกิจ

        //รายรับ
        public int salary;
        public int income;
        public int allRecieve;
        public int getmoney;
        //รายจาย
        
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
        public bool hasJob1 = true;
        public bool hasJob2 = true;

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

    }


    public List<Entity> playerList = new List<Entity>();
    public List<Entity> SortplayerList = new List<Entity>();

    //STATEMACHINE
    public enum States
    {
        WAITING,ROLL_DICE,ACTION,SWITCH_PLAYER,START_TURN
    }
    public States state;

    public int activePlayer;
    bool switchingPlayer;
    bool turnPossible = true;

    public GameObject diceButton;
    public GameObject DoublediceButton;
    public Text TurnUI;
    [HideInInspector]public int rolledhumanDice;

    public PhotonView photonView;
    public Dice dice;
    public Dice dice2;

    //private PhotonView photonView;
    void Awake()
    {
        instace = this;

        photonView = GetComponent<PhotonView>();

        for (int i = 0; i < playerList.Count; i++)
        {
            if (SaveSettings.players[i] == "HUMAN")
            {
                playerList[i].playerType = Entity.PlayerTypes.HUMAN;
            }
            if (SaveSettings.players[i] == "NP")
            {
                //playerList.RemoveAt(i);
                playerList[i].playerType = Entity.PlayerTypes.NO_PLAYER;
            }
        }
    }

    void Start()
    {
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
                        Debug.Log(IsMyTurnSure());
                        Debug.Log("Localplayer now " + PhotonNetwork.LocalPlayer.ActorNumber);
                        Debug.Log("activeplayer now " + activePlayer);
                        playerList[activePlayer].myPlayers[0].SetSelector(true);
                        playerList[activePlayer].myPlayers[0].turncounts++;
                        photonView.RPC("turnCountRPC", RpcTarget.All, playerList[activePlayer].myPlayers[0].turncounts);


                        if (playerList[activePlayer].hasJob1 == true && playerList[activePlayer].hasJob2 == true)
                            {
                                state = States.ROLL_DICE;
                            }
                            else
                            {
                                if (playerList[activePlayer].hasJob1 == true && playerList[activePlayer].hasJob2 == false)
                                {
                                photonView.RPC("hasJob2", RpcTarget.All);
                            }
                                if (playerList[activePlayer].hasJob1 == false && playerList[activePlayer].hasJob2 == false)
                                {
                                photonView.RPC("hasJob1", RpcTarget.All);
                            }
                                state = States.SWITCH_PLAYER;
                            }

                        
                        
                    }
                    break;
                case States.ROLL_DICE:
                    {

                            //Deactivate Highlight
                            ActivateButton(true);
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
        info.instance.showMessage("Roll Dice Number:" + _diceNumber);
    }

    /*
    void MoveAPlayer(int DiceNumber)
    {
        List<Player1> moveablePlayers = new List<Player1>();
        moveablePlayers.Add(playerList[activePlayer].myPlayers[0]);
        if (moveablePlayers.Count > 0)
        {
            moveablePlayers[activePlayer].StartTheMove(DiceNumber);
            /*
            int num = moveablePlayers.Count;
            moveablePlayers[num-1].StartTheMove(DiceNumber);
            state = States.WAITING;
            return;
        }
        Debug.Log("Should switch player ");
        state = States.SWITCH_PLAYER;
    }*/

    /*public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        object activePlayerObj;
        if (propertiesThatChanged.TryGetValue("activePlayer", out activePlayerObj))
        {
            activePlayer = (int)activePlayerObj;

            // Here you can update your game UI, gameplay logic, etc., based on the turn change.
        }
    }*/
    IEnumerator SwitchPlayer()
    {
        
        Debug.Log(activePlayer);
        if (switchingPlayer || PhotonNetwork.LocalPlayer.ActorNumber - 1 != activePlayer )
            {
                yield break;
            }
            switchingPlayer = true;

            yield return new WaitForSeconds(2);
            
            //SET NEXT PLAYER
        SetNextActivePlayer();
        Debug.Log(activePlayer);
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
        activePlayer++;
        activePlayer %= playerList.Count;
        photonView.RPC("nextPlayerRPC", RpcTarget.All, activePlayer);
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
        }

        state = States.START_TURN;
    }

    public void ReportTurnPossible(bool possible)
    {

        turnPossible = possible;
       
    }


    public void ReportWinning()
    {
        //show some ui
        playerList[activePlayer].hasOutside = true;

        for (int i = 0; i < SaveSettings.winners.Length; i++)
        {
            if (SaveSettings.winners[i] == "")
            {
                SaveSettings.winners[i] = playerList[activePlayer].playerName;
                break;
            }
        }

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
    void ActivateButton(bool on)
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
        dice.RollDice();
        dice2.RollDice();
        //Dice.instace.diceValue = dice.diceValue + dice2.diceValue;
        ActivateButton(false);
    }
    public void PassTurn()
    {
        state = States.SWITCH_PLAYER;
        UIController.instance.passButton.SetActive(false);
    }

    public void HumanRollDice()
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
                //photonView.RPC("nextPlayerRPC", RpcTarget.All, activePlayer);
                state = States.SWITCH_PLAYER;
            }
        }
    } 

    List <Player1> PossiblePlayer()
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
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("2 player in room");

            playerList[2].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[3].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            Debug.Log("3 player in room");

            playerList[3].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            Debug.Log("4 player in room");

            playerList[4].playerType = Entity.PlayerTypes.NO_PLAYER;
            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            //SaveSettings.players[5] = "NP";
            Debug.Log("5 player in room");

            playerList[5].playerType = Entity.PlayerTypes.NO_PLAYER;
        }
    }

    public void valueUpdate()
    {
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].salary + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].income;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].loanBank * (1 / 10);
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].child * GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].perChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].tax + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].homeMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].learnMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].carMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].creditcardMortgage + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].extraPay + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].InstallmentsBank + GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].sumChild;
        GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].getmoney = GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].allRecieve - GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].paid;
    }
    public void playerJobSetting()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < GameManager.instace.playerList.Count; i++)
            {
                GameManager.instace.playerList[i].playerJob = GameManager.RandomEnum.Of<GameManager.Entity.Jobs>();
                photonView.RPC("SetPlayerJob", RpcTarget.All, playerList[i].playerJob,i);
                //CheckingJob(i);
            }

            //photonView.RPC("SetPlayerFirstMoney", RpcTarget.All, playerList[activePlayer].firstMoney, playerList[activePlayer].money, playerList[activePlayer].salary, playerList[activePlayer].income, playerList[activePlayer].allRecieve, playerList[activePlayer].tax, playerList[activePlayer].homeMortgage, playerList[activePlayer].learnMortgage, playerList[activePlayer].carMortgage, playerList[activePlayer].creditcardMortgage, playerList[activePlayer].extraPay, playerList[activePlayer].InstallmentsBank);

            /*
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].homeDebt);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].learnDebt);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].carDebt);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].creditDebt);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].loanBank);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].child);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].perChild);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].sumChild);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].paid);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].getmoney);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].hasJob1);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].hasJob2);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].hasChild);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].hasDonate);
            photonView.RPC("SetPlayerJob", RpcTarget.AllBuffered, playerList[activePlayer].hasOutside);*/
        }

    }

    private bool IsMyTurnSure()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return PhotonNetwork.LocalPlayer.ActorNumber - 1 == GameManager.instace.activePlayer;
    }

    public void CheckingJob(int i)
    {
        // DOCTOR, LAWER, POLICE, TRUCK_DRIVER, TEACHER, MACHANIC, NURSE, SECRETARY, CLEANING_STAFF, MANAGER, PILOT, ENGINEER
        //---------------------------------------------------------------------doctor-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.DOCTOR)
        {

            GameManager.instace.playerList[i].firstMoney = 35000;

            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;

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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;

        }
        //---------------------------------------------------------------------lawer-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.LAWER)
        {
            GameManager.instace.playerList[i].firstMoney = 20000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------police-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.POLICE)
        {
            GameManager.instace.playerList[i].firstMoney = 5000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------truck driver-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TRUCK_DRIVER)
        {
            GameManager.instace.playerList[i].firstMoney = 8000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------teacher-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.TEACHER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------machanic-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MACHANIC)
        {
            GameManager.instace.playerList[i].firstMoney = 7000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------nurse-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.NURSE)
        {
            GameManager.instace.playerList[i].firstMoney = 5000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------secretary-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.SECRETARY)
        {
            GameManager.instace.playerList[i].firstMoney = 7000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------cleaning staff-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.CLEANING_STAFF)
        {
            GameManager.instace.playerList[i].firstMoney = 6000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------manager-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.MANAGER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------pilot-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.PILOT)
        {
            GameManager.instace.playerList[i].firstMoney = 25000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;
        }
        //---------------------------------------------------------------------engineer-----------------------------------------------------------------
        if (GameManager.instace.playerList[i].playerJob == GameManager.Entity.Jobs.ENGINEER)
        {
            GameManager.instace.playerList[i].firstMoney = 4000;
            GameManager.instace.playerList[i].money = GameManager.instace.playerList[i].firstMoney;
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

            GameManager.instace.playerList[i].hasJob1 = true;
            GameManager.instace.playerList[i].hasJob2 = true;
            GameManager.instace.playerList[i].hasChild = false;
            GameManager.instace.playerList[i].hasDonate = false;
            GameManager.instace.playerList[i].hasOutside = false;

        }

    
}


    [PunRPC]
    void SetPlayerJob(Entity.Jobs jobPlayer,int i)
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
    void hasJob2()
    {
        playerList[activePlayer].hasJob2 = true;
        Debug.Log("unemployee 2");
    }

    [PunRPC]
    void hasJob1()
    {
        playerList[activePlayer].hasJob1 = true;
        Debug.Log("unemployee 1");
    }

    [PunRPC]
    void SetStartingPlayer(int startingPlayer)
    {
        activePlayer = startingPlayer;
        while (playerList[activePlayer].playerType == Entity.PlayerTypes.NO_PLAYER)
        {
            activePlayer++;
            if (activePlayer >= playerList.Count)
            {
                activePlayer = 0;
            }
        }
        Debug.Log("SetStartingPlayer RPC : " + activePlayer);
        info.instance.showMessage("Player " + activePlayer + " starts first");
        TurnUI.text = "Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts;
        Debug.Log("Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);

    }

    [PunRPC]
    void rollDice()
    {
        dice.RollDice();
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
}
