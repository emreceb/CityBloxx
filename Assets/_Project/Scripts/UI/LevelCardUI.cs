using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCardUI : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subText;
    public Image cardBackground;
    public Image buildingIcon;
    public Image badgeBackground;
    public TextMeshProUGUI badgeText;
    public Button button;
    public Image stripe;

    private int cityIndex;
    private int levelIndex;

    public void Setup(int level, bool isUnlocked, bool isCompleted, int city)
    {
        cityIndex = city;
        levelIndex = level;

        string[] levelNames = {
            "Temel Atma", "Duvarlar", "Ust Katlar", "Cati", "Kule",
            "Cephe", "Pencereler", "Giris", "Bahce", "Tamamlandi",
            "Yeni Bina", "Iskelet", "Beton", "Demir", "Celik",
            "Cam Cephe", "Asansor", "Merdiven", "Teras", "Cati Kati",
            "Anten", "Isiklar", "Tabela", "Son Rotus", "Acilis"
        };

        string name = level <= levelNames.Length ? levelNames[level - 1] : "Bolum " + level;
        int requiredBlocks = 12 + ((level - 1) * 4);
        int targetScore = 100 + (level * 80);

        titleText.text = "Bolum " + level + "  —  " + name;
        subText.text = requiredBlocks + " blok  |  Hedef: " + targetScore + " puan";

        if (isCompleted)
        {
            statusText.text = "TAMAMLANDI";
            statusText.color = new Color(0.18f, 0.62f, 0.35f);
            cardBackground.color = new Color(0.05f, 0.14f, 0.08f);
            buildingIcon.color = new Color(0.11f, 0.40f, 0.20f);
            badgeBackground.color = new Color(0.10f, 0.28f, 0.14f);
            badgeText.text = "OK";
            badgeText.color = new Color(0.18f, 0.62f, 0.35f);
            if (stripe != null) stripe.color = new Color(0.18f, 0.62f, 0.35f);
        }
        else if (isUnlocked)
        {
            statusText.text = "OYNA";
            statusText.color = new Color(0.96f, 0.78f, 0.26f);
            cardBackground.color = new Color(0.06f, 0.12f, 0.22f);
            buildingIcon.color = new Color(0.11f, 0.30f, 0.43f);
            badgeBackground.color = new Color(0.20f, 0.28f, 0.08f);
            badgeText.text = ">";
            badgeText.color = new Color(0.96f, 0.78f, 0.26f);
            if (stripe != null) stripe.color = new Color(0.96f, 0.78f, 0.26f);
        }
        else
        {
            statusText.text = "KILITLI";
            statusText.color = new Color(0.25f, 0.35f, 0.48f);
            cardBackground.color = new Color(0.04f, 0.08f, 0.13f);
            buildingIcon.color = new Color(0.08f, 0.15f, 0.22f);
            badgeBackground.color = new Color(0.08f, 0.15f, 0.22f);
            badgeText.text = "X";
            badgeText.color = new Color(0.25f, 0.35f, 0.48f);
            if (stripe != null) stripe.color = new Color(0.15f, 0.22f, 0.32f);
        }

        button.interactable = isUnlocked;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        PlayerPrefs.SetInt("SelectedCity", cityIndex);
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene");
    }
}