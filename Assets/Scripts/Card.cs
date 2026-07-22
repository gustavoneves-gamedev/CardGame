using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardSO;

    [Header("Card Values")]
    public int attackPower;
    public int currentHealth;
    public int manaCost;

    [Header("Upper Card References")]
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text manaCostText;

    [Header("Card Text References")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text actionDescriptionText;
    [SerializeField] private TMP_Text loreText;

    [Header("Card Images")]
    [SerializeField] private Image characterArt;
    [SerializeField] private Image bgArt;

    [Header("Card Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 540f;
    private Vector3 targetPoint;
    private Quaternion targetRot;

    public bool inHand;
    public int handPosition;
    private HandController handController; //Eu faria com um GameController em vez de usar o FindObject no Start()

    private bool isSelected;
    private bool justPressed; //Serve para controlar o primeiro clique. Testar fazer o primeiro clique funcionar no
                              // update com Input.GetMouseButtonDown(0)
    private Collider cardCollider;
    public LayerMask whatIsDesktop;
    public LayerMask whatIsPlacement;
    public CardPlacePoint assignPlace;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupCard();

        handController = FindFirstObjectByType<HandController>();
        cardCollider = GetComponent<Collider>();
    }

    public void SetupCard()
    {
        attackPower = cardSO.attackPower;
        currentHealth = cardSO.currentHealth;
        manaCost = cardSO.manaCost;

        attackText.text = attackPower.ToString();
        healthText.text = currentHealth.ToString();
        manaCostText.text = manaCost.ToString();

        nameText.text = cardSO.name;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        characterArt.sprite = cardSO.characterSprite;
        bgArt.sprite = cardSO.bgSprite;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        if (isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, whatIsDesktop))
            {
                //MoveToPoint(hit.point, Quaternion.identity);
                ////Um jeito melhor de fazer essa questão seria colocar uma referência do colisor aéreo no GameController
                ////e ativar/desativar por lá

                //moveSpeed *= 2;

                MoveToPoint(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonDown(0) && !justPressed)
            {
                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement))
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if (selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        selectedPoint.activeCard = this;
                        assignPlace = selectedPoint;

                        MoveToPoint(selectedPoint.transform.position, Quaternion.identity);

                        inHand = false;
                        isSelected = false;
                        //cardCollider.enabled = true; //Não consigo mais interagir com a carta
                        //isso pode ser alterado futuramente

                        handController.RemoveCardFromHand(this);
                    }
                    else
                    {
                        ReturnToHand();
                    }


                }
                else
                {
                    ReturnToHand();
                }
            }

            //justPressed = false; -> Acho que aqui ficaria melhor para evitar ser chamado constantemente
        }

        justPressed = false;

    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRot = rotToMatch;
        //transform.position = pointToMoveTo;
    }

    private void OnMouseOver()
    {
        Debug.Log("Fui chamado");

        if (inHand && handController != null)
        {
            MoveToPoint(handController.cardPositions[handPosition] + new Vector3(0f, 1f, 0.5f), Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        //if (isSelected) return;

        if (inHand && handController != null)
        {
            MoveToPoint(handController.cardPositions[handPosition], handController.minPos.rotation);
        }
    }

    private void OnMouseDown()
    {
        if (inHand)
        {
            isSelected = true;
            cardCollider.enabled = false;

            justPressed = true;
            //inHand = false;
        }
    }

    public void ReturnToHand()
    {
        isSelected = false;
        cardCollider.enabled = true;
        //inHand = true;

        MoveToPoint(handController.cardPositions[handPosition], handController.minPos.rotation);
    }
}
