using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public Button playButton;
    public Button soundButton;
    public TextMeshProUGUI soundButtonText;

    private bool isSoundOn = true;

    private void Start()
    {
        // Highscore goster
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "En Yuksek: " + highScore;

        // Ses durumunu yukle
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundButton();

        // Buton dinleyicileri
        playButton.onClick.AddListener(StartGame);
        soundButton.onClick.AddListener(ToggleSound);
    }

   public void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    private void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        AudioListener.volume = isSoundOn ? 1f : 0f;
        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {
        soundButtonText.text = isSoundOn ? "ON" : "OFF";
        AudioListener.volume = isSoundOn ? 1f : 0f;
    }
}