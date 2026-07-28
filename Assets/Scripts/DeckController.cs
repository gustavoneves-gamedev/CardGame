using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    //public static DeckController deckController;

    public List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();

    private List<CardScriptableObject> activeCards = new List<CardScriptableObject>();

    public Card cardToSpawn;

    public int drawCardCost = 2;

    public float waitBetweenDrawingCards = .25f;

    [Header("Visual Deck config")]
    [SerializeField] private GameObject cardVisual;
    private Transform deckBasePosition;
    [SerializeField] private List<GameObject> visualDeckCards = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BattleController.battleController.deckController = this;

        deckBasePosition = transform;
        
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

        SetDeckCardsVisuals();
    }

    private void SetDeckCardsVisuals()
    {
        for (int i = 0; i < activeCards.Count; i++)
        {
            GameObject obj = Instantiate(cardVisual, transform.position + (Vector3.up * i * 0.02f), transform.rotation);
            visualDeckCards.Add(obj);
        }
    }

    private void UpdateDeckCardsVisuals(int amountToReduce = 1)
    {
        Destroy(visualDeckCards[visualDeckCards.Count - 1]);
        visualDeckCards.RemoveAt(visualDeckCards.Count-1);
    }

    public void DrawCardToHand()
    {
        if (activeCards.Count <= 0) return;
        

        Card newCard = Instantiate(cardToSpawn, visualDeckCards[visualDeckCards.Count - 1].transform.position, transform.rotation);
        newCard.cardSO = activeCards[0];
        newCard.SetupCard();

        activeCards.RemoveAt(0);
        UpdateDeckCardsVisuals();

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
