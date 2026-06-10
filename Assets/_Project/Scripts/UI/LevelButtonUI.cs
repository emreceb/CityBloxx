using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI levelNumberText;
    public Image lockIcon;
    public Image completedIcon;
    public Button button;

    private int cityIndex;
    private int levelIndex;

    public void Setup(int level, bool isUnlocked, bool isCompleted, int city)
    {
        cityIndex = city;
        levelIndex = level;

        levelNumberText.text = level.ToString();

        button = GetComponent<Button>();

        if (lockIcon != null)
            lockIcon.gameObject.SetActive(!isUnlocked);

        if (completedIcon != null)
            completedIcon.gameObject.SetActive(isCompleted);

        if (button != null)
        {
            button.interactable = isUnlocked;

            if (isUnlocked && !isCompleted)
                button.GetComponent<Image>().color = new Color(0.18f, 0.31f, 0.56f);
            else if (isCompleted)
                button.GetComponent<Image>().color = new Color(0.24f, 0.65f, 0.36f);
            else
                button.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);

            button.onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        PlayerPrefs.SetInt("SelectedCity", cityIndex);
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameplayScene");
    }
}