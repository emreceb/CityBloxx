using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [System.Serializable]
    public class LevelData
    {
        public int levelNumber;
        public int requiredBlocks;
        public int targetScore;
        public float craneSpeed;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public LevelData GetLevelData(int cityIndex, int levelIndex)
    {
        LevelData data = new LevelData();
        data.levelNumber = levelIndex;
        data.requiredBlocks = 10 + (levelIndex * 2);
        data.targetScore = 100 + (levelIndex * 50);
        data.craneSpeed = 1.2f + (levelIndex * 0.1f);
        return data;
    }

    public bool IsLevelUnlocked(int cityIndex, int levelIndex)
    {
        if (cityIndex == 1 && levelIndex == 1) return true;
        return PlayerPrefs.GetInt("City_" + cityIndex + "_Level_" + (levelIndex - 1) + "_Completed", 0) == 1;
    }

    public void CompleteLevel(int cityIndex, int levelIndex)
    {
        PlayerPrefs.SetInt("City_" + cityIndex + "_Level_" + levelIndex + "_Completed", 1);
        PlayerPrefs.Save();
    }

    public int GetCompletedLevels(int cityIndex)
    {
        int count = 0;
        for (int i = 1; i <= 25; i++)
        {
            if (PlayerPrefs.GetInt("City_" + cityIndex + "_Level_" + i + "_Completed", 0) == 1)
                count++;
        }
        return count;
    }
}