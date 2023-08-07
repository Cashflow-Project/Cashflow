using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        WAITING,ROLL_DICE,ACTION,SWITCH_PLAYER
    }
    public States state;

    public int activePlayer;
    bool switchingPlayer;


    void Awake()
    {
        instace = this;
    }
    void Update()
    {
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.CPU)
        {
            switch (state)
            {
                case States.ROLL_DICE:
                    {
                        
                        Debug.Log("Turns player " + playerList[activePlayer].playerName + " Turn'" + playerList[activePlayer].myPlayers[0].turncounts);
                        StartCoroutine(RollDiceDelay());
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
                        playerList[activePlayer].myPlayers[0].turncounts++;
                        StartCoroutine(SwitchPlayer());
                        state = States.WAITING;
                    }
                    break;
            }
        }
        
    }

    void RollDice()
    {
        int DiceNumber = Random.Range(1, 7);
        //int DiceNumber = 6;

        /**if(DiceNumber == 6)
        {
            //check start node
            CheckStartNode(DiceNumber);
        }**/
        if(DiceNumber <= 6)
        {
            CheckStartNode(DiceNumber);
            MoveAPlayer(DiceNumber);
        }
        Debug.Log("Dice Rolled number : " + DiceNumber);
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(2);
        RollDice();
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
            else//start node empty
            {
                //if have inside base
                for (int j = 0; j < playerList[activePlayer].myPlayers.Length; i++)
                {
                    j++;
                    if (!playerList[activePlayer].myPlayers[i].ReturnIsOut())
                    {
                        //leave the base
                        playerList[activePlayer].myPlayers[i].leaveBase();
                        state = States.WAITING;
                        return;
                    }
                }
                //move player
                MoveAPlayer(DiceNumber);
            }
        }
    }
    void MoveAPlayer(int DiceNumber)
    {
        List<Player> moveablePlayers = new List<Player>();
        List<Player> moveKickPlayers = new List<Player>();

        for (int i = 0; i < playerList[activePlayer].myPlayers.Length; i++)
        {
            if (playerList[activePlayer].myPlayers[i].ReturnIsOut())
            {
                //check possible kick
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
        }
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

        state = States.ROLL_DICE;
    }
}
