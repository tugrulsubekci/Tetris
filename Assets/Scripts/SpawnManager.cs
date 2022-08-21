using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] tetrominoes;
    private Vector3 spawnPos;
    private int randomIndex;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spawnPos = transform.position;
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        if(!gameManager.gameOver)
        {
            randomIndex = Random.Range(0, tetrominoes.Length);
            GameObject go = Instantiate(tetrominoes[randomIndex], spawnPos, Quaternion.identity);
            foreach (Transform child in go.transform)
            {
                int roundedX = Mathf.RoundToInt(child.position.x);
                int roundedY = Mathf.RoundToInt(child.position.y);
                if (TetrisBlock.grid[roundedX, roundedY] != null)
                {
                    gameManager.GameOver();
                }
            }
        }
    }
}
