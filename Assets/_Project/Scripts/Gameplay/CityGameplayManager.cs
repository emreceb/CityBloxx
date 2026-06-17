using UnityEngine;

public class CityGameplayManager : MonoBehaviour
{
    public static CityGameplayManager Instance { get; private set; }

    [Header("City Backgrounds")]
    public Sprite[] cityBackgrounds; // 0=Istanbul, 1=Tokyo, 2=NewYork, 3=Paris, 4=Dubai

    [Header("City Block Sprites")]
    public Sprite[] cityBlockSprites; // 0=Istanbul, 1=Tokyo, 2=NewYork, 3=Paris, 4=Dubai

    [Header("References")]
    public SpriteRenderer backgroundRenderer;

    private int currentCity = 1;
    private int currentLevel = 1;
    private int requiredBlocks = 12;
    private int placedBlocks = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        currentCity = PlayerPrefs.GetInt("SelectedCity", 1);
        currentLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
        requiredBlocks = 12 + ((currentLevel - 1) * 4);
        placedBlocks = 0;

        ApplyCityTheme();
    }

    private void ApplyCityTheme()
    {
        int index = currentCity - 1;

        // Arka plan
        if (backgroundRenderer != null && cityBackgrounds != null && index < cityBackgrounds.Length)
            backgroundRenderer.sprite = cityBackgrounds[index];

        // Blok sprite
        if (BlockSpawner.Instance != null && cityBlockSprites != null && index < cityBlockSprites.Length)
            BlockSpawner.Instance.SetCitySprite(cityBlockSprites[index]);
    }

    public void OnBlockPlaced()
    {
        placedBlocks++;
        if (placedBlocks >= requiredBlocks)
        {
            LevelComplete();
        }
    }

    public int GetRequiredBlocks() => requiredBlocks;
    public int GetPlacedBlocks() => placedBlocks;

    private void LevelComplete()
    {
        LevelManager.Instance.CompleteLevel(currentCity, currentLevel);
        GameManager.Instance.WinLevel();
        WinUI winUI = FindFirstObjectByType<WinUI>();
        if (winUI != null)
            winUI.ShowWin(currentCity, currentLevel);
    }
}