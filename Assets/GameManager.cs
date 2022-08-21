using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelText;
    public bool gameOver;
    private int score;
    private int level = 1;
    public void GameOver()
    {
        gameOver = true;
        gameOverMenu.SetActive(true);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
        if(score == 1000 * level)
        {
            LevelUp();
        }
    }

    public void RestartButton()
    {
        TetrisBlock.normalRepeatRate = 0.8f;
        SceneManager.LoadScene(0);
    }

    public void LevelUp()
    {
        level++;
        levelText.text = $"LEVEL {level}";
        SpeedUp();
    }

    void SpeedUp()
    {
        TetrisBlock.normalRepeatRate -= 0.05f;
    }
}
