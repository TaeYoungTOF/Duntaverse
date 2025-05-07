using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlappyPlaneUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject homeUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public void Start()
    {
        if (homeUI == null)
        {
            Debug.LogError("home UI is null");
            return;
        }

        if (gameOverUI == null)
        {
            Debug.LogError("gameOver UI is null");
            return;
        }
        
        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }
        
        homeUI.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
    }

    public void SetStart()
    {
        homeUI.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }

    public void SetRestart(int resultScore, int bestScore)
    {
        resultScoreText.text = resultScore.ToString();
        bestScoreText.text = bestScore.ToString();

        scoreText.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}