using UnityEngine;

public class ArenaController : MonoBehaviour
{
    public static ArenaController arenaController;

    [SerializeField] private CardPlacePoint[] arenaSlots;
    public CardPlacePoint[,] arena = new CardPlacePoint[5, 5];
    private bool[,] virtualArena = new bool[7, 7];
    
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
        
    }

    public void SetArena()
    {
        for (int i = 0; i < arena.GetLength(0); i++)
        {
            for (int j = 0; j < arena.GetLength(1); j++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
