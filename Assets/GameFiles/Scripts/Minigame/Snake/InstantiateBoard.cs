using Minigame.Snake;
using UnityEngine;

public class InstantiateBoard : MonoBehaviour
{
    [SerializeField] public SnakeGameLogic snakeGameLogic;
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform cellSpawnPoint;
    [SerializeField] float offset, cellScale;
    bool HasGeneratedBoard;
    public void GenerateBoard()
    {
        int curentCell = 0;
        if (!HasGeneratedBoard)
        {
            for (int i = 0; i < snakeGameLogic.boardHeight; i++)
            {
                for (int j = 0; j < snakeGameLogic.boardWidth; j++)
                {
                    GameObject _ = Instantiate(cellPrefab, cellSpawnPoint.position + (Vector3.right * (j * (offset + (cellScale / 2)))) + (Vector3.down * (i * (offset + (cellScale / 2)))), cellSpawnPoint.rotation);
                    snakeGameLogic.cell[curentCell] = _.GetComponent<SpriteRenderer>();
                    curentCell++;
                }
            }
            HasGeneratedBoard = true;
        }
        else
        {
            for (int j = 0; j < snakeGameLogic.cell.Length; j++)
            {
                snakeGameLogic.cell[j].gameObject.SetActive(true);
            }
        }
    }
    public void DeleteBoard()
    {
        for (int i = 0;i < snakeGameLogic.cell.Length;i++)
        {
            snakeGameLogic.cell[i].gameObject.SetActive(false);
        }
    }
}