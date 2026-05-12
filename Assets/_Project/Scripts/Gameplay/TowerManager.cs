using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [Header("Settings")]
    public GameObject blockPrefab;
    public float blockHeight = 1.5f;
    public float blockWidth = 1.5f;
    public float groundY = -6f;

    private List<GameObject> placedBlocks = new List<GameObject>();
    private float currentTopY;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ResetTower();
    }

    public void ResetTower()
    {
        foreach (GameObject block in placedBlocks)
            if (block != null) Destroy(block);
        placedBlocks.Clear();
        currentTopY = groundY;
    }

    public void BlockLanded(GameObject block, float landedX)
    {
        float previousX = placedBlocks.Count > 0
            ? placedBlocks[placedBlocks.Count - 1].transform.position.x
            : landedX;

        float diff = Mathf.Abs(landedX - previousX);

        if (placedBlocks.Count > 0 && diff > blockWidth)
        {
            Destroy(block);
            GameManager.Instance.GameOver();
            return;
        }

        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        block.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        block.transform.position = new Vector3(
            landedX,
            currentTopY + blockHeight / 2f,
            0f
        );

        currentTopY += blockHeight;

        placedBlocks.Add(block);
        ScoreManager.Instance.AddScore(diff, blockWidth, block.transform.position);
        DifficultyManager.Instance.OnBlockPlaced(placedBlocks.Count);
        CityProgressManager.Instance.OnBlockPlaced();
        SpawnNextBlock();
    }

    public void SpawnNextBlock()
    {
        if (blockPrefab == null)
        {
            Debug.LogError("Block Prefab atanmamis!");
            return;
        }

        GameObject newBlock = Instantiate(blockPrefab);
        newBlock.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        // Þehire göre sprite ata
        SpriteRenderer sr = newBlock.GetComponent<SpriteRenderer>();
        if (sr != null && BlockSpawner.Instance != null)
        {
            int currentCity = CityProgressManager.Instance.GetCurrentCity();
            Sprite citySprite = BlockSpawner.Instance.GetBlockSprite(currentCity);
            if (citySprite != null)
                sr.sprite = citySprite;
        }

        Rigidbody2D rb = newBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
        }

        CraneController crane = FindFirstObjectByType<CraneController>();
        if (crane != null)
            crane.ResetCrane(newBlock);
    }

    public float GetCurrentTopY() => currentTopY;
}