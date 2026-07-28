using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController battleController;

    public int startingMana = 4;
    public int maxMana = 12;
    public int playerMana;
    private int currentPlayerMaxMana;

    public int startingCardsAmount = 5;
    public int cardsToDrawPerTurn = 2;

    public DeckController deckController;

    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;

    private void Awake()
    {
        if (battleController == null)
        {
            battleController = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {

        currentPlayerMaxMana = startingMana;
        FillPlayerMana();

        Invoke("Initialize", .2f);
    }

    private void Initialize()
    {
        deckController.DrawMultipleCards(startingCardsAmount);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceTurn();
        }
    }

    public void SpendPlayerMana(int amountToSpend)
    {
        playerMana -= amountToSpend;

        if (playerMana < 0) playerMana = 0;

        UIController.uiController.SetPlayerManaText(playerMana);
    }

    public void FillPlayerMana()
    {
        playerMana = currentPlayerMaxMana;
        UIController.uiController.SetPlayerManaText(playerMana);
    }


    public void AdvanceTurn()
    {
        currentPhase++;

        if ((int)currentPhase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
        {
            currentPhase = 0;
        }

        switch (currentPhase)
        {
            case TurnOrder.playerActive:

                UIController.uiController.endTurnButton.SetActive(true);
                UIController.uiController.drawCardButton.SetActive(true);

                if (currentPlayerMaxMana < maxMana)
                {
                    currentPlayerMaxMana++;
                }

                FillPlayerMana();

                deckController.DrawMultipleCards(cardsToDrawPerTurn);

                break;

            case TurnOrder.playerCardAttacks:

                Debug.Log("Skip player card attacks");
                AdvanceTurn();

                break;

            case TurnOrder.enemyActive:

                Debug.Log("Skip enemy active");
                AdvanceTurn();

                break;

            case TurnOrder.enemyCardAttacks:

                Debug.Log("Skip enemy card attacks");
                AdvanceTurn();

                break;
        }
    }

    public void EndPlayerTurn()
    {
        UIController.uiController.endTurnButton.SetActive(false);
        UIController.uiController.drawCardButton.SetActive(false);

        AdvanceTurn();
    }
}
