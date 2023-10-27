using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player1 : Photon.Pun.MonoBehaviourPun
{
    public static Player1 instace;
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

        //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        
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
                    UIController.instance.passButton.SetActive(IsMyTurn());
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
                UIController.instance.passButton.SetActive(IsMyTurn());
                //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            //red route
            if (routePosition % fullRoute.Count == 2 || routePosition % fullRoute.Count == 10 || routePosition % fullRoute.Count == 18)
            {
                Debug.Log("in red route");
                UIController.instance.drawButton.SetActive(IsMyTurn());
            }

            //blue route
            if (routePosition % fullRoute.Count == 8 || routePosition % fullRoute.Count == 16 || routePosition % fullRoute.Count == 24)
            {
                Debug.Log("in blue route");
                UIController.instance.passButton.SetActive(IsMyTurn());
                //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            //purple1 route
            if (routePosition % fullRoute.Count == 4)
            {
                Debug.Log("in purple 1 route");
                UIController.instance.passButton.SetActive(IsMyTurn());
                //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            //purple2 route
            if (routePosition % fullRoute.Count == 12)
            {
                Debug.Log("in purple 2 route");
                UIController.instance.passButton.SetActive(IsMyTurn());
                //GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasJob1 = false;
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasJob2 = false;
            }
            //purple3 route
            if (routePosition % fullRoute.Count == 20)
            {
                Debug.Log("in purple 3 route");
                UIController.instance.passButton.SetActive(IsMyTurn());
                GameManager.instace.playerList[GameManager.instace.activePlayer].hasChild = true;
                if (GameManager.instace.playerList[GameManager.instace.activePlayer].child <= 3)
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
        if (IsMyTurn())
        {
            selector.SetActive(IsMyTurn());
            hasTurn = on;
        }
    }
  
    public void tohasturn()
    {
     if (hasTurn)
     {
        
        StartTheMove(GameManager.instace.rolledhumanDice);
     }
        GameManager.instace.DeactivateAllSelector();
    }
    
    private bool IsMyTurn()
    {
        // Replace with your logic. This could be checking against a player list, an ID, etc.
        return PhotonNetwork.LocalPlayer.ActorNumber - 1 == GameManager.instace.activePlayer;
    }

}

 