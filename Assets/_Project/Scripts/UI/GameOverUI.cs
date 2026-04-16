using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Skor: " + ScoreManager.Instance.GetScore();
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        ScoreManager.Instance.ResetScore();
        GameManager.Instance.StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
