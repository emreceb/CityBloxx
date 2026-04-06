using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI blockCountText;

    private int blockCount = 0;
    private int maxBlocks = 20;

    private void Update()
    {
        if (ScoreManager.Instance != null)
            scoreText.text = "Skor: " + ScoreManager.Instance.GetScore();

        blockCountText.text = blockCount + "/" + maxBlocks;
    }

    public void IncrementBlockCount()
    {
        blockCount++;
    }
}