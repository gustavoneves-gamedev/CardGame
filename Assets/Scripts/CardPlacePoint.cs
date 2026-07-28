using UnityEngine;
using UnityEngine.UI;

public class CardPlacePoint : MonoBehaviour
{
    public Card activeCard;
    public bool canSpawnHere;
    public int line, row;
    private SpriteRenderer image;
    private Color defaultColor;
    private Color changeColor = Color.green;

    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        if(image != null) defaultColor = image.color;
    }

    public void ChangeColor(bool isFree)
    {
        if (isFree)
        {
            image.color = changeColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }

    public void UpdateArena()
    {
        ArenaController.arenaController.UpdateArena(line, row);
    }

    public void ShowAvaiablePositions()
    {
        ArenaController.arenaController.ShowAvaiablePositions(activeCard);
    }

}
