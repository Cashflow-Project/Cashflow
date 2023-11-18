using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MarketCard : MonoBehaviourPunCallbacks
{
    public CardGlobalMarketScriptableObj cardGMSO;


    public bool canLoan;
    public Image cardArt;

    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed =5f, rotateSpeed = 540f;

    public bool  OnShowing;
    public int ShowPosition;

    private ShowController sc;

    private bool isSelected;
    private Collider col;

    public LayerMask WhatIsDesktop, whatIsPlacemented;

    private bool justPressed;

    public CardPlacePoint assignedPlace;
    // Start is called before the first frame update
    void Start()
    {
        setupCard();
        
        sc = FindObjectOfType<ShowController>();

        col = GetComponent<Collider>();

    }

    public void setupCard()
    {

        cardArt.sprite = cardGMSO.cardSprite;
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
