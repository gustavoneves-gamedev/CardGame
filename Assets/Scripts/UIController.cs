using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController uiController;

    [SerializeField] private TMP_Text playerManaText;

    [SerializeField] private GameObject manaWarning;
    public float manaWarningTime;
    private float manaWarningCounter;

    public GameObject drawCardButton;
    public GameObject endTurnButton;

    private void Awake()
    {
        if (uiController == null)
        {
            uiController = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manaWarningCounter > 0)
        {
            manaWarningCounter -= Time.deltaTime;

            if (manaWarningCounter < 0)
            {
                manaWarning.SetActive(false);
            }
        }
    }

    public void SetPlayerManaText(int manaAmount)
    {
        playerManaText.text = "Mana: " + manaAmount;
    }

    public void ShowManaWarning()
    {
        manaWarning.SetActive(true);
        manaWarningCounter = manaWarningTime;
    }

    public void DrawCard()
    {

        BattleController.battleController.deckController.DrawCardForMana();
    }

    public void EndPlayerTurn()
    {
        BattleController.battleController.EndPlayerTurn();
    }
}
