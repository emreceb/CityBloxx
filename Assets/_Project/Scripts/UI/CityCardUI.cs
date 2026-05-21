using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityCardUI : MonoBehaviour
{
    [Header("UI References")]
    public Image cityImage;
    public TextMeshProUGUI cityNameText;
    public TextMeshProUGUI progressText;
    public Slider progressBar;
    public GameObject lockOverlay;
    public Button selectButton;

    public void Setup(string cityName, Sprite image, int completed, int total, bool isUnlocked)
    {
        cityNameText.text = cityName;
        cityImage.sprite = image;

        float progress = (float)completed / total;
        progressBar.value = progress;
        progressText.text = "Bölüm " + completed + "/" + total;

        if (lockOverlay != null)
            lockOverlay.SetActive(!isUnlocked);

        if (selectButton != null)
            selectButton.interactable = isUnlocked;
    }
}