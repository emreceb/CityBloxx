using UnityEngine;
using UnityEngine.UI;

public class CitySelectUI : MonoBehaviour
{
    [Header("UI References")]
    public Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}