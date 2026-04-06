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

    private List<GameObject> placedBlocks = new List<GameObject>();
    private float currentTopY = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

        // Bloðu her zaman sabit boyutta ve tam hizalý yerleþtir
        block.transform.position = new Vector3(0f, currentTopY + blockHeight / 2f, 0f);
        block.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        ScoreManager.Instance.AddScore(diff, blockWidth);

        placedBlocks.Add(block);
        currentTopY += blockHeight;

        GameHUD hud = FindFirstObjectByType<GameHUD>();
        if (hud != null) hud.IncrementBlockCount();

        SpawnNextBlock();
    }

    public void SpawnNextBlock()
    {
        if (blockPrefab == null) return;

        GameObject newBlock = Instantiate(blockPrefab);
        newBlock.transform.localScale = new Vector3(blockWidth, blockHeight, 1f);

        CraneController crane = craneTransform.GetComponent<CraneController>();
        if (crane != null)
        {
            newBlock.transform.SetParent(craneTransform);
            newBlock.transform.localPosition = new Vector3(0f, 3f, 0f);

            Rigidbody2D rb = newBlock.GetComponent<Rigidbody2D>();
            if (rb != null) rb.bodyType = RigidbodyType2D.Kinematic;

            crane.ResetCrane(newBlock);
        }
    }

    public float GetCurrentTopY()
    {
        return currentTopY;
    }
}