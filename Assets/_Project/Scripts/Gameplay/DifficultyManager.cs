using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("Settings")]
    public float baseSpeed = 1.2f;
    public float speedIncrement = 0.12f;
    public float maxSpeed = 2.2f;
    public int blocksPerLevel = 5;

    private int currentLevel = 1;
    private CraneController crane;

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
        crane = FindFirstObjectByType<CraneController>();
    }

    public void OnBlockPlaced(int totalBlocks)
    {
        int newLevel = (totalBlocks / blocksPerLevel) + 1;

        if (newLevel != currentLevel)
        {
            currentLevel = newLevel;
            UpdateSpeed();
        }
    }

    private void UpdateSpeed()
    {
        if (crane == null) return;

        float newSpeed = baseSpeed + (currentLevel - 1) * speedIncrement;
        newSpeed = Mathf.Min(newSpeed, maxSpeed);
        crane.swingSpeed = newSpeed;

        Debug.Log("Zorluk: " + GetLevelName() + " | Hiz: " + newSpeed);
    }

    public string GetLevelName()
    {
        if (currentLevel <= 1) return "Kolay";
        if (currentLevel <= 2) return "Orta";
        if (currentLevel <= 3) return "Zor";
        return "Uzman";
    }

    public int GetCurrentLevel() { return currentLevel; }

    public void Reset()
    {
        currentLevel = 1;
        if (crane != null) crane.swingSpeed = baseSpeed;
    }
}