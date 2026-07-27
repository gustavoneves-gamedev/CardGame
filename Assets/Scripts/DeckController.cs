using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController deckController;

    public List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();

    private List<CardScriptableObject> activeCards = new List<CardScriptableObject>();

    public Card cardToSpawn;

    public int drawCardCost = 2;

    public float waitBetweenDrawingCards = .25f;


    private void Awake()
    {
        if (deckController == null)
        {
            deckController = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupDeck();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DrawCardToHand();
        //}
    }

    public void SetupDeck()
    {
        activeCards.Clear();

        List<CardScriptableObject> tempDeck = new List<CardScriptableObject>();
        tempDeck.AddRange(deckToUse);


        int interations = 0;
        while (tempDeck.Count > 0 && interations < 100)
        {
            int selected = Random.Range(0, tempDeck.Count);

            activeCards.Add(tempDeck[selected]);//Pega uma carta aleatória do deck temporário e coloca nas cartas ativas
            tempDeck.RemoveAt(selected);// remove a carta do deck temporário após adicionar às cartas temporárias

            interations++;
        }
    }

    public void DrawCardToHand()
    {
        if (activeCards.Count == 0)
        {
            SetupDeck();
        }

        Card newCard = Instantiate(cardToSpawn, transform.position, transform.rotation);
        newCard.cardSO = activeCards[0];
        newCard.SetupCard();

        activeCards.RemoveAt(0);

        HandController.handController.AddCardToHand(newCard);

    }

    public void DrawCardForMana()
    {
        if (BattleController.battleController.playerMana >= drawCardCost)
        {
            DrawCardToHand();
            BattleController.battleController.SpendPlayerMana(drawCardCost);
        }
        else
        {
            UIController.uiController.ShowManaWarning();
            UIController.uiController.drawCardButton.SetActive(false);
        }
    }

    public void DrawMultipleCards(int amountToDraw)
    {
        StartCoroutine(DrawMultipleCo(amountToDraw));
    }

    private IEnumerator DrawMultipleCo(int amountToDraw)
    {
        for (int i = 0; i < amountToDraw; i++)
        {
            DrawCardToHand();

            yield return new WaitForSeconds(waitBetweenDrawingCards);
        }
    }
}
