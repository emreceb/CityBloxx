using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI difficultyText;
    public Button menuButton;

    private int blockCount = 0;
    private int maxBlocks = 20;

    private void Start()
    {
        if (menuButton != null)
            menuButton.onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
            });
    }

    private void Update()
    {
        if (ScoreManager.Instance != null)
            scoreText.text = "Skor: " + ScoreManager.Instance.GetScore();

        blockCountText.text = blockCount + "/" + maxBlocks;

        if (DifficultyManager.Instance != null)
            difficultyText.text = DifficultyManager.Instance.GetLevelName();
    }

    public void IncrementBlockCount()
    {
        blockCount++;
    }
}