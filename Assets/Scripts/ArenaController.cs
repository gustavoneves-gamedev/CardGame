using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI.Table;

public class ArenaController : MonoBehaviour
{
    public static ArenaController arenaController;

    [SerializeField] private CardPlacePoint[] arenaSlots;
    public CardPlacePoint[,] arena = new CardPlacePoint[5, 5];
    public bool[,] virtualArena = new bool[11, 11];

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
        int counter = 0;
        
        for (int i = 0; i < arena.GetLength(0); i++)
        {
            for (int j = 0; j < arena.GetLength(1); j++)
            {
                if (counter >= arenaSlots.Length) return;

                arena[i, j] = arenaSlots[counter];
                arena[i, j].line = i;
                arena[i, j].row = j;

                counter++;
            }
        }

        for (int i = 0; i < virtualArena.GetLength(0); i++)
        {
            for (int j = 0; j < virtualArena.GetLength(1); j++)
            {
                if (i < 3 || j < 3 || i > virtualArena.GetLength(0) - 4 || j > virtualArena.GetLength(1) - 4)
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
       
        virtualArena[line + 3, row + 3] = false;
        
    }

    public void ShowAvaiablePositions(Card card, int movement = 2)
    {
        int line = card.assignPlace.line;
        int row = card.assignPlace.row;
        int virtualLine = line + 3;
        int virtualRow = row + 3;
       

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
