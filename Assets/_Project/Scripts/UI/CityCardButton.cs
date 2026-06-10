using UnityEngine;
using UnityEngine.UI;

public class CityCardButton : MonoBehaviour
{
    public int cityIndex;
    public string cityName;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        PlayerPrefs.SetInt("SelectedCity", cityIndex);
        PlayerPrefs.SetString("SelectedCityName", cityName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
}