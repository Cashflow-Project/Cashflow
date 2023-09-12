using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instace;

    [System.Serializable]
    public class Entity
    {
        public int money;
        public int getmoney;
        public enum Jobs
        {
            DOCTOR,LAWER,POLICE,TRUCK_DRIVER,TEACHER,MACHANIC,NURSE,SECRETARY,CLEANING_STAFF,MANAGER,PILOT,ENGINEER
        }
        public Jobs playerJob;
        public int paid;

        public string playerName;
        public Player[] myPlayers;
        public bool hasTurn;
        public enum PlayerTypes
        {
            HUMAN,CPU,NO_PLAYER
        }
        public PlayerTypes playerType;
        public bool hasOutside;
        public bool hasDonate;
        public bool hasJob = true;
        

    }

    public List<Entity> playerList = new List<Entity>();

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
    public Text TurnUI;
    [HideInInspector]public int rolledhumanDice;

    public Dice dice;
    void Awake()
    {
        instace = this;

        for (int i = 0; i < playerList.Count; i++)
        {
            if (SaveSettings.players[i] == "HUMAN")
            {
                playerList[i].playerType = Entity.PlayerTypes.HUMAN;
            }
            if (SaveSettings.players[i] == "CPU")
            {
                playerList[i].playerType = Entity.PlayerTypes.CPU;
            }
            if (SaveSettings.players[i] == "NP")
            {
                playerList[i].playerType = Entity.PlayerTypes.NO_PLAYER;
            }
        }
    }

    void Start()
    {
        ActivateButton(false);

        int randomPlayer = Random.Range(0, playerList.Count);
        activePlayer = randomPlayer;
        while(playerList[activePlayer].playerType == Entity.PlayerTypes.NO_PLAYER)
        {
            activePlayer++;
        }
        info.instance.showMessage(playerList[activePlayer].playerName + " starts first");
    }
    void Update()
    {
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.CPU)
        {
            switch (state)
            {
                case States.START_TURN:
                    {
                        playerList[activePlayer].myPlayers[0].SetSelector(true);
                        playerList[activePlayer].myPlayers[0].turncounts++;
                        state = States.ROLL_DICE;
                    }
                    break;
                case States.ROLL_DICE:
                    {
                        if (turnPossible) {
                            
                            Debug.Log("Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);
                            TurnUI.text = "Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts;
                            StartCoroutine(RollDiceDelay());
                            state = States.WAITING; 
                        }
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
                        if (turnPossible)
                        {
                            playerList[activePlayer].myPlayers[0].SetSelector(false);
                            StartCoroutine(SwitchPlayer());
                            state = States.WAITING;
                        }
                    }
                    break;
            }
        }
        if (playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN)
        {
            switch (state)
            {
                case States.START_TURN:
                    {
                        playerList[activePlayer].myPlayers[0].SetSelector(true);
                        playerList[activePlayer].myPlayers[0].turncounts++;
                        state = States.ROLL_DICE;
                    }
                    break;
                case States.ROLL_DICE:
                    {
                        if (turnPossible)
                        {
                            
                            Debug.Log("Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);
                            TurnUI.text = "Turn player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts;
                            //Deactivate Highlight
                            ActivateButton(true);
                            state = States.WAITING;
                        }
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
                        if (turnPossible)
                        {
                            //Deactivate button

                            //Deactivate Highlight
                            playerList[activePlayer].myPlayers[0].SetSelector(false);
                            StartCoroutine(SwitchPlayer());
                            state = States.WAITING;
                        }
                    }
                    break;
            }
        }

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

    void CPUDice()
    {
        dice.RollDice();
    }
    public void RollDice(int _diceNumber)
    {
        int DiceNumber = _diceNumber;//Random.Range(1, 7);
        //int DiceNumber = 6;

        /**if(DiceNumber == 6)
        {
            //check start node
            CheckStartNode(DiceNumber);
        }**/
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.CPU)
        {
            if (DiceNumber <= 6)
            {
                //CheckStartNode(DiceNumber);
                MoveAPlayer(DiceNumber);
            }
        }
        if (playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN)
        {
            rolledhumanDice = _diceNumber;
            HumanRollDice();
        }
            Debug.Log("Dice Rolled number : " + DiceNumber);
        info.instance.showMessage("Roll Dice Number:" + _diceNumber);
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(2);
        // RollDice();
        CPUDice();
    }

    void CheckStartNode(int DiceNumber)
    {
        //is anyone on start node
        bool startnodeFull = false;
        for (int i = 0; i < playerList[activePlayer].myPlayers.Length; i++)
        {
            if(playerList[activePlayer].myPlayers[i].currentNode == playerList[activePlayer].myPlayers[i].startNode)
            {
                startnodeFull = true;
                break;
            }
            if (startnodeFull)
            {
                //move player
                
                MoveAPlayer(DiceNumber);
                Debug.Log("Start node is full");
            }
            /**else//start node empty
            {
                //if have inside base
                for (int j = 0; j < playerList[activePlayer].myPlayers.Length; i++)
                {
                    j++;
                    if (!playerList[activePlayer].myPlayers[i].ReturnIsOut())
                    {
                        //leave the base
                        Debug.Log("2");
                        playerList[activePlayer].myPlayers[i].leaveBase();
                        state = States.WAITING;
                        return;
                    }
                }
                //move player
                MoveAPlayer(DiceNumber);
            }**/
        }
    }
    void MoveAPlayer(int DiceNumber)
    {
        List<Player> moveablePlayers = new List<Player>();
        //List<Player> moveKickPlayers = new List<Player>();
        /**
        for (int i = 0; i < playerList[activePlayer].myPlayers.Length; i++)
        {
            if (playerList[activePlayer].myPlayers[i].ReturnIsOut())
            {
                //check possible kick
                /**
                if (playerList[activePlayer].myPlayers[i].CheckPossibleKick(playerList[activePlayer].myPlayers[i].playerid, DiceNumber))
                {
                    moveKickPlayers.Add(playerList[activePlayer].myPlayers[i]);
                    continue;
                }

                //check for possible move
                if (playerList[activePlayer].myPlayers[i].CheckPossible(DiceNumber))
                {
                    moveablePlayers.Add(playerList[activePlayer].myPlayers[i]);
                }

            }
        }**/
        moveablePlayers.Add(playerList[activePlayer].myPlayers[0]);
        //perform kick if possible
        /**
        if (moveKickPlayers.Count > 0)
        {
            int num = Random.Range(0, moveKickPlayers.Count);
            moveKickPlayers[num].StartTheMove(DiceNumber);
            state = States.WAITING;
            return;
        }**/
        if (moveablePlayers.Count > 0)
        {
            //playerList[activePlayer].myPlayers[0].turncounts++;
            int num = moveablePlayers.Count;
            moveablePlayers[num-1].StartTheMove(DiceNumber);
            state = States.WAITING;
            return;
        }
        Debug.Log("Should switch player ");
        state = States.SWITCH_PLAYER;
    }

    IEnumerator SwitchPlayer()
    {
        if (switchingPlayer)
        {
            yield break;
        }
        switchingPlayer = true;

        yield return new WaitForSeconds(2);

        //SET NEXT PLAYER
        SetNextActivePlayer();

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

        int available = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].hasOutside)
            {
                available++;
            }
        }

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

    //---------------------------human input--------------------
    void ActivateButton(bool on)
    {
        diceButton.SetActive(on);
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
    }

    public void HumanRoll()
    {
        dice.RollDice();
        ActivateButton(false);
    }

    public void HumanRollDice()
    {
       

        //roll dice
        //rolledhumanDice = Random.Range(1, 7);
       // Debug.Log("Dice Rolled number : " + rolledhumanDice);
        List<Player> moveablePlayers = new List<Player>();
        //List<Player> moveKickPlayers = new List<Player>();
        /**
        for (int i = 0; i < playerList[activePlayer].myPlayers.Length; i++)
        {
            if (playerList[activePlayer].myPlayers[i].ReturnIsOut())
            {
                //check possible kick
                /**
                if (playerList[activePlayer].myPlayers[i].CheckPossibleKick(playerList[activePlayer].myPlayers[i].playerid, rolledhumanDicer))
                {
                    moveablePlayers.Add(playerList[activePlayer].myPlayers[i]);
                    continue;
                }

                //check for possible move
                if (playerList[activePlayer].myPlayers[i].CheckPossible(rolledhumanDice))
                {
                    moveablePlayers.Add(playerList[activePlayer].myPlayers[i]);
                }

            }
        }**/
        moveablePlayers.Add(playerList[activePlayer].myPlayers[0]);
        /**
        if(rolledhumanDice <= 6)
        {
            moveablePlayers.AddRange(PossiblePlayer());
        }**/
        //state = States.SWITCH_PLAYER;
        /**
        if (moveablePlayers.Count > 0)
        {
            int num = moveablePlayers.Count;
            moveablePlayers[num - 1].StartTheMove(rolledhumanDice);
            state = States.WAITING;
            return;
        }
        Debug.Log("Should switch player ");
        state = States.SWITCH_PLAYER;**/
        
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

    List <Player> PossiblePlayer()
    {
        List<Player> tempList = new List<Player>();

        tempList.Add(playerList[activePlayer].myPlayers[0]);
        return tempList;
    }

    
}
