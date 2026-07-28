using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI.Table;

public class ArenaController : MonoBehaviour
{
    public static ArenaController arenaController;

    [SerializeField] private CardPlacePoint[] arenaSlots;
    public CardPlacePoint[,] arena = new CardPlacePoint[5, 5];
    public bool[,] virtualArena = new bool[7, 7];

    private void Awake()
    {
        if (arenaController == null)
        {
            arenaController = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetArena();
    }

    public void SetArena()
    {
        for (int i = 0; i < arena.GetLength(0); i++)
        {
            for (int j = 0; j < arena.GetLength(1); j++)
            {
                arena[i, j] = arenaSlots[i + j];
                arena[i, j].line = i;
                arena[i, j].row = j;
            }
        }

        for (int i = 0; i < virtualArena.GetLength(0); i++)
        {
            for (int j = 0; j < virtualArena.GetLength(1); j++)
            {
                if (i == 0 || j == 0 || i == virtualArena.GetLength(0) - 1 || j == virtualArena.GetLength(1) - 1)
                {
                    virtualArena[i, j] = false;
                }
                else
                {
                    virtualArena[i, j] = true;
                }

            }
        }
    }

    public void UpdateArena(int line, int row)
    {
       
        virtualArena[line + 1, row + 1] = false;
        
    }

    public void ShowAvaiablePositions(Card card, int movement = 1)
    {
        int line = card.assignPlace.line;
        int row = card.assignPlace.row;
        int virtualLine = line + 1;
        int virtualRow = row + 1;
       

        for (int i = 0; i < movement + 1; i++)
        {
            //Checagem Vertical
            if (virtualArena[virtualLine + i, virtualRow] == true)
            {
                arena[line + i, row].ChangeColor(true);
            }
            
            if (virtualArena[virtualLine - i, virtualRow] == true)
            {
                arena[line - i, row].ChangeColor(true);
            }

            if (virtualArena[virtualLine, virtualRow + i] == true)
            {
                arena[line, row + i].ChangeColor(true);
            }
            
            if (virtualArena[virtualLine, virtualRow - i] == true)
            {
                arena[line, row - i].ChangeColor(true);
            }

        }

    }


}
