using UnityEngine;

public class CityProgressManager : MonoBehaviour
{
    public static CityProgressManager Instance { get; private set; }

    [Header("Settings")]
    public int totalCities = 5;
    public int blocksPerCity = 10;

    private int currentCity = 1;
    private int currentBlock = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void OnBlockPlaced()
    {
        currentBlock++;
        if (currentBlock >= blocksPerCity)
        {
            currentBlock = 0;
            currentCity++;
            if (currentCity > totalCities)
                currentCity = totalCities;
            Save();
            Debug.Log("Yeni sehir: " + currentCity);
        }
    }

    public int GetCurrentCity() => currentCity;
    public int GetCurrentBlock() => currentBlock;

    public void Save()
    {
        PlayerPrefs.SetInt("CurrentCity", currentCity);
        PlayerPrefs.SetInt("CurrentBlock", currentBlock);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        currentCity = PlayerPrefs.GetInt("CurrentCity", 1);
        currentBlock = PlayerPrefs.GetInt("CurrentBlock", 0);
    }

    public void Reset()
    {
        currentCity = 1;
        currentBlock = 0;
        Save();
    }
}