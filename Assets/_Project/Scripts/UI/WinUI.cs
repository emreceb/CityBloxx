using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject winPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public Button nextLevelButton;
    public Button menuButton;

    private void Start()
    {
        winPanel.SetActive(false);
        nextLevelButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(GoToMenu);
    }

    public void ShowWin(int cityIndex, int levelIndex)
    {
        winPanel.SetActive(true);
        titleText.text = "TEBRIKLER!";
        levelText.text = "Bolum " + levelIndex + " Tamamlandi!";
        scoreText.text = "Skor: " + ScoreManager.Instance.GetScore();
    }

    private void NextLevel()
    {
        int currentCity = PlayerPrefs.GetInt("SelectedCity", 1);
        int currentLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        int nextLevel = currentLevel + 1;

        if (nextLevel > 25)
        {
            int nextCity = currentCity + 1;
            if (nextCity > 5)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                return;
            }
            PlayerPrefs.SetInt("SelectedCity", nextCity);
            PlayerPrefs.SetInt("SelectedLevel", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SelectedLevel", nextLevel);
        }

        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene");
    }

    private void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}