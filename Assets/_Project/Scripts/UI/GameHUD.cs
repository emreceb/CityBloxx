using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI blockCountText;
    public TextMeshProUGUI difficultyText;

    private int blockCount = 0;
    private int maxBlocks = 20;

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