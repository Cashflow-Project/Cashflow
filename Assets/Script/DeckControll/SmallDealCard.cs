using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SmallDealCard : MonoBehaviourPunCallbacks
{
    public CardSmallChangeScriptableObj cardSCSO;

    public int BusinessValue;
    public int DownPayment;
    public int BankLoan;
    public int CashflowIncome;

    public int value;
    public int value2;

    public bool canLoan;
    public object SmallCardArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed =5f, rotateSpeed = 540f;

    public bool  OnShowing;
    public int ShowPosition;

    private ShowSmallDealController sc;

    private bool isSelected;
    private Collider col;

    public LayerMask WhatIsDesktop, whatIsPlacemented;

    private bool justPressed;

    public CardPlacePoint assignedPlace;
    // Start is called before the first frame update
    void Start()
    {
        setupCard();
        
        //sc = FindObjectOfType<ShowSmallDealController>();

        col = GetComponent<Collider>();

    }

    public void setupCard()
    {
        Debug.Log(cardSCSO.cardSprite);
        BusinessValue = cardSCSO.BusinessValue;
        DownPayment = cardSCSO.DownPayment;
        BankLoan = cardSCSO.BankLoan;
        CashflowIncome = cardSCSO.CashflowIncome;
        value = cardSCSO.value;
        value2 = cardSCSO.value2;
        
        SmallCardArt = cardSCSO.cardSprite;
        Debug.Log(SmallCardArt);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot ,rotateSpeed * Time.deltaTime);

    }
    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch){
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
    }
    
   
}
