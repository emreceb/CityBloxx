using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }
    [Header("Settings")]
    public GameObject blockPrefab;
    public Transform craneTransform;
    public float blockHeight = 1f;
    public float blockWidth = 1.5f;
    public float groundY = -4f; // Ground'un Y pozisyonu

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
        currentTopY = groundY; // Baþlangýç zeminden baþlasýn
    }

    public void BlockLanded(GameObject block, float landedX)
    {
        float previousX = placedBlocks.Count > 0
            ? placedBlocks[placedBlocks.Count - 1].transform.position.x
            : 0f;

        float diff = Mathf.Abs(landedX - previousX);

        // Tamamen kaçýrdýysa game over
        if (diff > blockWidth)
        {
            Destroy(block);
            GameManager.Instance.GameOver();
            Debug.Log("GAME OVER - Block missed!");
            return;
        }

        // Bloðu dondur
        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // X pozisyonunu olduðu yerde býrak, sadece Y'yi sabitle
        block.transform.position = new Vector3(
            block.transform.position.x,
            currentTopY + blockHeight / 2f,
            0f
        );
        block.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        ScoreManager.Instance.AddScore(diff, blockWidth);

        placedBlocks.Add(block);
        currentTopY += blockHeight;

        GameHUD hud = FindFirstObjectByType<GameHUD>();
        if (hud != null) hud.IncrementBlockCount();

        DifficultyManager.Instance.OnBlockPlaced(placedBlocks.Count);

        SpawnNextBlock();

        Debug.Log("CurrentTopY: " + currentTopY);
    }

    public void SpawnNextBlock()
    {
        if (blockPrefab == null) return;

        GameHUD hud = FindFirstObjectByType<GameHUD>();
        if (hud != null) hud.IncrementBlockCount();

        GameObject newBlock = Instantiate(blockPrefab);
        newBlock.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        Rigidbody2D rb = newBlock.GetComponent<Rigidbody2D>();
        if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

        CraneController crane = FindFirstObjectByType<CraneController>();
        if (crane != null)
            crane.ResetCrane(newBlock);
    }

    public float GetCurrentTopY()
    {
        return currentTopY;
    }
}