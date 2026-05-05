using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int currentScore = 0;
    private int highScore = 0;
    private int comboCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(float diff, float blockWidth, Vector3 blockPosition)
    {
        int points = 0;
        comboCount++;

        float ratio = diff / blockWidth;

        if (ratio < 0.1f)
        {
            points = 20;
            AudioManager.Instance.PlayPerfect();
            EffectsManager.Instance.PlayEffect("perfect", blockPosition - new Vector3(0, 0.5f, 0));
        }
        else if (ratio < 0.4f)
        {
            points = 10;
            AudioManager.Instance.PlayGood();
            EffectsManager.Instance.PlayEffect("good", blockPosition - new Vector3(0, 0.5f, 0));
        }
        else
        {
            points = 5;
            comboCount = 0;
            AudioManager.Instance.PlayBad();
            EffectsManager.Instance.PlayEffect("bad", blockPosition - new Vector3(0, 0.5f, 0));
        }

        // Combo bonusu
        if (comboCount >= 3)
        {
            points *= 2;
            Debug.Log("COMBO x2!");
        }

        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        Debug.Log("Skor: " + currentScore + " | Rekor: " + highScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        comboCount = 0;
    }

    public int GetScore() { return currentScore; }
    public int GetHighScore() { return highScore; }
}