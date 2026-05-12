using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner Instance { get; private set; }

    [Header("City Block Sprites")]
    public Sprite[] cityBlockSprites; // 0=Istanbul, 1=Tokyo, 2=NewYork, 3=Paris, 4=Dubai

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Sprite GetBlockSprite(int cityIndex)
    {
        if (cityBlockSprites == null || cityBlockSprites.Length == 0)
            return null;
        int index = Mathf.Clamp(cityIndex - 1, 0, cityBlockSprites.Length - 1);
        return cityBlockSprites[index];
    }
}