using UnityEngine;
using UnityEngine.Rendering;
using static BattleController;
using static UnityEngine.Rendering.DebugUI.Table;

public class ArenaController : MonoBehaviour
{
    public static ArenaController arenaController;

    [SerializeField] private CardPlacePoint[] cardPlacePoints;
    private CardPlacePoint[,] playerArenaSlots = new CardPlacePoint[2, 5];
    private CardPlacePoint[,] terrainArenaSlots = new CardPlacePoint[1, 5];
    private CardPlacePoint[,] oponentArenaSlots = new CardPlacePoint[2, 5];

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
       int counterA = 0, counterB = 0, counterC = 0, counterD = 0, counterE = 0;

        for (int i = 0; i < cardPlacePoints.Length; i++)
        {
            if (cardPlacePoints[i].isEnemyDefenseLane)
            {
                oponentArenaSlots[1, counterA] = cardPlacePoints[i];
                cardPlacePoints[i].line = 1;
                cardPlacePoints[i].row = counterA;
                counterA++;
            }
            else if (cardPlacePoints[i].isEnemyAttackLane)
            {
                oponentArenaSlots[0, counterB] = cardPlacePoints[i];
                cardPlacePoints[i].line = 0;
                cardPlacePoints[i].row = counterB;
                counterB++;
            }
            else if (cardPlacePoints[i].isTerrainLane)
            {
                terrainArenaSlots[0, counterC] = cardPlacePoints[i];
                cardPlacePoints[i].line = 0;
                cardPlacePoints[i].row = counterC;
                counterC++;
            }
            else if (cardPlacePoints[i].isPlayerAttackLane)
            {
                playerArenaSlots[0, counterD] = cardPlacePoints[i];
                cardPlacePoints[i].line = 0;
                cardPlacePoints[i].row = counterD;
                counterD++;
            }
            else if (cardPlacePoints[i].isPlayerDefenseLane)
            {
                playerArenaSlots[1, counterE] = cardPlacePoints[i];
                cardPlacePoints[i].line = 1;
                cardPlacePoints[i].row = counterE;
                counterE++;
            }

        }

    }


    //public void ShowAvaiablePositions(Card card, int movement = 2)
    //{
    //    int line = card.assignPlace.line;
    //    int row = card.assignPlace.row;
    //    int virtualLine = line + 3;
    //    int virtualRow = row + 3;
       

    //    for (int i = 0; i < movement + 1; i++)
    //    {
    //        //Checagem Vertical
    //        if (virtualArena[virtualLine + i, virtualRow] == true)
    //        {
    //            arena[line + i, row].ChangeColor(true);
    //        }
            
    //        if (virtualArena[virtualLine - i, virtualRow] == true)
    //        {
    //            arena[line - i, row].ChangeColor(true);
    //        }

    //        if (virtualArena[virtualLine, virtualRow + i] == true)
    //        {
    //            arena[line, row + i].ChangeColor(true);
    //        }
            
    //        if (virtualArena[virtualLine, virtualRow - i] == true)
    //        {
    //            arena[line, row - i].ChangeColor(true);
    //        }

    //    }

    //}


}
