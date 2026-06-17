using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner Instance { get; private set; }

    [Header("City Block Sprites")]
    public Sprite[] cityBlockSprites;

    private Sprite currentCitySprite;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetCitySprite(Sprite sprite)
    {
        currentCitySprite = sprite;
    }

    public Sprite GetBlockSprite(int cityIndex)
    {
        if (currentCitySprite != null)
            return currentCitySprite;
        if (cityBlockSprites == null || cityBlockSprites.Length == 0)
            return null;
        int index = Mathf.Clamp(cityIndex - 1, 0, cityBlockSprites.Length - 1);
        return cityBlockSprites[index];
    }
}