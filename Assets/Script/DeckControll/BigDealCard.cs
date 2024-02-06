using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class BigDealCard : MonoBehaviourPunCallbacks
{
    public CardBigChangeScriptableObj cardBCSO;

    public int BusinessValue;
    public int DownPayment;
    public int BankLoan;
    public int CashflowIncome;

    public bool canLoan;
    public Image cardArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed =5f, rotateSpeed = 540f;

    public bool  OnShowing;
    public int ShowPosition;

    private ShowBigDealController sc;

    private bool isSelected;
    private Collider col;

    public LayerMask WhatIsDesktop, whatIsPlacemented;

    private bool justPressed;

    public CardPlacePoint assignedPlace;
    // Start is called before the first frame update
    void Start()
    {
        setupCard();
        
        //sc = FindObjectOfType<ShowBigDealController>();

        col = GetComponent<Collider>();

    }

    public void setupCard()
    {
        Debug.Log(cardBCSO.cardSprite);
        Debug.Log("BigCard");
        BusinessValue = cardBCSO.BusinessValue;
        DownPayment = cardBCSO.DownPayment;
        BankLoan = cardBCSO.BankLoan;
        CashflowIncome = cardBCSO.CashflowIncome;
        Debug.Log(cardArt);
        cardArt.sprite = cardBCSO.cardSprite;
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
