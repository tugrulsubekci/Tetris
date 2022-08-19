using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] tetrominoes;
    private Vector3 spawnPos;
    private int randomIndex;
    private void Start()
    {
        spawnPos = transform.position;
        SpawnTetromino();
    }

    public void SpawnTetromino()
    {
        randomIndex = Random.Range(0, tetrominoes.Length);
        Instantiate(tetrominoes[randomIndex],spawnPos, Quaternion.identity);
    }
}
