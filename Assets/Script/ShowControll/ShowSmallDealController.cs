using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSmallDealController : MonoBehaviour
{
    public static ShowSmallDealController instance;
    private void Awake(){
        instance = this;
    }
    public List<SmallDealCard> heldCards = new List<SmallDealCard>();

    public Transform minPos, maxPos;

    

    public List<Vector3> cardPosition = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        SetCardPositionToShow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCardPositionToShow()
    {
        cardPosition.Clear();

        Vector3 distanceBetweenPoints = Vector3.zero;

        if (heldCards.Count > 1)
        {
            distanceBetweenPoints = (maxPos.position - minPos.position) / (heldCards.Count - 1);
        }

        for (int i = 0; i < heldCards.Count; i++)
        {
            cardPosition.Add(minPos.position + (distanceBetweenPoints * i));

            //this will set where the card should move to
            heldCards[i].MoveToPoint(cardPosition[i], minPos.rotation);

            heldCards[i].OnShowing = true;
            heldCards[i].ShowPosition = i;
        }
    }

    public void RemoveCardFormShow(SmallDealCard cardToRemove)
    {
        if (heldCards[cardToRemove.ShowPosition] == cardToRemove){
            heldCards.RemoveAt(cardToRemove.ShowPosition);
        }
        else{
        Debug.LogError("Card at position " + cardToRemove.ShowPosition + " is not the card to removed form hand");
        }

        SetCardPositionToShow();
    }

    public void AddCardToShow(SmallDealCard cardToAdd){
        heldCards.Add(cardToAdd);
        SetCardPositionToShow();
    }
}
