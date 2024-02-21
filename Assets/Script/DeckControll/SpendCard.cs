using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SpendCard : MonoBehaviourPunCallbacks
{
    public CardSpendScriptableObj cardSpendSO;

    public int payCost;
    public bool canLoan;
    public Image cardArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed =5f, rotateSpeed = 540f;

    public bool  OnShowing;
    public int ShowPosition;

    //private ShowController sc;

    private bool isSelected;
    private Collider col;

    public LayerMask WhatIsDesktop, whatIsPlacemented;

    private bool justPressed;

    //public CardPlacePoint assignedPlace;
    // Start is called before the first frame update
    void Start()
    {
        setupCard();
        
        //sc = FindObjectOfType<ShowController>();

        col = GetComponent<Collider>();

    }

    public void setupCard()
    {
        payCost = cardSpendSO.payCost;

        cardArt.sprite = cardSpendSO.cardSprite;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot ,rotateSpeed * Time.deltaTime);
/*
        if(isSelected){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,100f,WhatIsDesktop)){
                MoveToPoint(hit.point + new Vector3(0f,2f,0f),Quaternion.identity);
            }

            if(Input.GetMouseButtonDown(1)){
                returnToHand();
            }

            if(Input.GetMouseButtonDown(0) && justPressed == false){
                if(Physics.Raycast(ray,out hit,100f,whatIsPlacemented)){

                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if(selectedPoint.activeCard == null && selectedPoint.isPlayerPoint){

                        //if(BattleContrller.instance.playerMana >= manaCost)
                        //{

                        selectedPoint.activeCard = this;
                        assignedPlace = selectedPoint;

                        MoveToPoint(selectedPoint.transform.position, Quaternion.identity);

                        OnShowing = false;

                        isSelected = false;

                        sc.RemoveCardFormShow(this);

                        //BattleContrller.instance.SpendPlayerMana(manaCost);
                        //}else{
                        //returnToHand();

                        //UIController.instance.ShowManaWarning();
                        //}

                    }else{
                        returnToHand();
                    }
                } else{
                    returnToHand();
                }

            }
        }

        justPressed = false;*/
    }
    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch){
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
    }
    /*
    private void OnMouseOver(){
        if(OnShowing){
            MoveToPoint(sc.cardPosition[ShowPosition] + new Vector3(0f, 1f, .5f),Quaternion.identity);
        }
    }

    private void OnMouseExit(){
        MoveToPoint(sc.cardPosition[ShowPosition] ,sc.minPos.rotation);
    }

    private void OnMouseDown(){
        if(OnShowing){
            isSelected = true;
            col.enabled = false;

            justPressed = true;
        }
    }

    public void returnToHand(){
        isSelected = false;
        col.enabled = true;

        MoveToPoint(sc.cardPosition[ShowPosition],sc.minPos.rotation);
    }
    */
   
}
