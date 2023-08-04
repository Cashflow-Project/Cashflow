using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerid;

    [Header("ROUTES")]
    public Route commonRoute;
    public Route outerRoute;

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
    int turncounts;

    [Header("BOOLS")]
    public bool isOut;
    bool isMoving;

    bool hasTurn;//human input


    [Header("SELECTOR")]
    public GameObject selector;

    void Start()
    {
        startNodeIndex = commonRoute.RequestPosition(startNode.gameObject.transform);

        CreateFullRoute();
    }

    void CreateFullRoute()
    {
        for (int i = 0; i < commonRoute.childNodeList.Count; i++)
        {
            int tempPos = startNodeIndex + i;
            tempPos %= commonRoute.childNodeList.Count;

            fullRoute.Add(commonRoute.childNodeList[tempPos+1].GetComponent<Node>());
        }
        /**
        for (int i = 0; i < outerRoute.childNodeList.Count; i++)
        {
            fullRoute.Add(outerRoute.childNodeList[i+1].GetComponent<Node>());
        }**/
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1, 7); 
            turncounts++;
            Debug.Log("Turns" + turncounts);
            Debug.Log("Dice output" + steps);
            if(turncounts == 1)
            {
                steps--;
            }
            StartCoroutine(Move());
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
            while (MoveToNextNode(nextPos,8f)){yield return null;}
            
            
            yield return new WaitForSeconds(0.1f);
            steps--;
            doneSteps++;
        }
        /** lastNode = fullRoute[routePosition];
        if (lastNode.isTaken)
        {
            //return to start base node
        }
        currentNode.player = null;
        currentNode.isTaken = false;

        lastNode.player = this;
        lastNode.isTaken = true;

        currentNode = lastNode;
        lastNode = null;**/
        GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
        isMoving = false;
        
    }
    bool MoveToNextNode(Vector3 lastPos,float speed)
    {
        return lastPos != (transform.position = Vector3.MoveTowards(transform.position, lastPos, speed * Time.deltaTime));

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
        
        StartCoroutine(MoveOut());
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
            while (MoveToNextNode(nextPos, 8f)) { yield return null; }


            yield return new WaitForSeconds(0.1f);
            steps--;
            doneSteps++;

        }

        //update node
        /** lastNode = fullRoute[routePosition];
         if (lastNode.isTaken)
         {
             //return to start base node
         }
         lastNode.player = this;
         lastNode.isTaken = true;

         currentNode = lastNode;
         lastNode = null;**/


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
        turncounts++;
        Debug.Log("Turns" + turncounts);
        if (turncounts == 1)
        {
            steps--;
        }
        StartCoroutine(Move());
    }
}
