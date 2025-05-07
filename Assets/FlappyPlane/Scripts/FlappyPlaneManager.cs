using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyPlaneManager : MonoBehaviour
{
    static FlappyPlaneManager flappyPlaneManager;
    public static FlappyPlaneManager Instance
    {
        get { return flappyPlaneManager; }
    }
    
    private int currentScore = 0;
    private int bestScore = 0;

    private const string BestScoreKey = "BestScore";

    FlappyPlaneUIManager flappyPlaneUIManager;
    public FlappyPlaneUIManager FlappyPlaneUIManager { get { return flappyPlaneUIManager; } }
    
    private void Awake()
    {
        flappyPlaneManager = this;
        flappyPlaneUIManager = FindObjectOfType<FlappyPlaneUIManager>();

        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        bestScore  = PlayerPrefs.GetInt(BestScoreKey, 0);

        flappyPlaneUIManager.UpdateScore(0);
    }

    public void StartGame()
    {
        flappyPlaneUIManager.SetStart();
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        if (bestScore < currentScore)
        {
            Debug.Log("Renew Best Score!");
            bestScore = currentScore;

            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
        Debug.Log("Game Over");
        flappyPlaneUIManager.SetRestart(currentScore, bestScore);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        flappyPlaneUIManager.UpdateScore(currentScore);
                
        Debug.Log("Score: " + currentScore);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }

    public void ReturnToDungeon()
    {
        GameManager.Instance.FlappyPlaneScore = bestScore;
        PlayerPrefs.SetInt(BestScoreKey, 0);

        SceneManager.LoadScene("MainScene");
    }
}