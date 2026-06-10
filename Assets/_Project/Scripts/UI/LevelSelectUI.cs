using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public TextMeshProUGUI cityNameText;
    public Button backButton;

    [Header("Card Prefab")]
    public GameObject levelCardPrefab;

    private int currentCityIndex;

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);
        int cityIndex = PlayerPrefs.GetInt("SelectedCity", 1);
        string cityName = PlayerPrefs.GetString("SelectedCityName", "Ýstanbul");
        Setup(cityIndex, cityName);
    }

    public void Setup(int cityIndex, string cityName)
    {
        currentCityIndex = cityIndex;
        cityNameText.text = cityName;
        LoadLevels();
    }

    private void LoadLevels()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        for (int i = 1; i <= 25; i++)
        {
            int levelIndex = i;
            bool isUnlocked = LevelManager.Instance.IsLevelUnlocked(currentCityIndex, levelIndex);
            bool isCompleted = PlayerPrefs.GetInt("City_" + currentCityIndex + "_Level_" + levelIndex + "_Completed", 0) == 1;

            GameObject card = LevelCardCreator.CreateCard(contentParent);
            LevelCardUI cardUI = card.GetComponent<LevelCardUI>();
            if (cardUI != null)
                cardUI.Setup(levelIndex, isUnlocked, isCompleted, currentCityIndex);
        }
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CitySelect");
    }
}