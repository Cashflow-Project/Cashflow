using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendDeckController : MonoBehaviour
{
    public static SpendDeckController instance;


    private void Awake()
    {
        instance = this;
    }

    public List<CardSpendScriptableObj> deckToUse = new List<CardSpendScriptableObj>();

    private List<CardSpendScriptableObj> activeCards = new List<CardSpendScriptableObj>();

    public SpendCard cardsToSpawns;

    public int FordrawCard = 1;
    public float waitBetweenDrawingCard = .3f;
    // Start is called before the first frame update
    void Start()
    {
        SetUpDeck();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.T)){
            DrawCardToHand();
        }*/
    }

    public void SetUpDeck()
    {
        activeCards.Clear();

        List<CardSpendScriptableObj> tempDeck = new List<CardSpendScriptableObj>();
        tempDeck.AddRange(deckToUse);

        int iterations = 0;
        while (tempDeck.Count > 0 && iterations < 500)
        {
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);

            iterations++;
        }
    }

    public void DrawCardToHand()
    {
        if (activeCards.Count == 0)
        {
            SetUpDeck();
        }

        SpendCard newCard = Instantiate(cardsToSpawns, transform.position, transform.rotation);
        newCard.cardSpendSO = activeCards[0];

        activeCards.RemoveAt(0);

        ShowController.instance.AddCardToShow(newCard);
    }

    /*public void DrawCardForMana()
    {
        if (BattleContrller.instance.playerMana >= drawCardCost)
        {
            DrawCardToHand();
            BattleContrller.instance.SpendPlayerMana(drawCardCost);
        }
        else
        {
            UIController.instance.ShowManaWarning();
            UIController.instance.drawButton.SetActive(false);
        }
    }

    public void DrawMultipleCards(int amountToDraw)
    {
        StartCoroutine(DrawMultipleCo(amountToDraw));
    }

    IEnumerator DrawMultipleCo(int amountToDraw)
    {
        for (int i = 0; i < amountToDraw; i++)
        {
            DrawCardToHand();

            yield return new WaitForSeconds(waitBetweenDrawingCard);

        }
    }*/
}