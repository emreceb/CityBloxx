using UnityEngine;

public class CraneController : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingSpeed = 1.2f;
    public float swingAngle = 30f;
    public float ropeLength = 4f;

    [Header("References")]
    public GameObject currentBlock;
    public LineRenderer lineRenderer;

    private bool isSwinging = false;
    private float timeCounter = 0f;

    private void Start()
    {
        Invoke("DelayedSpawn", 0.1f);
    }

    private void DelayedSpawn()
    {
        TowerManager.Instance.SpawnNextBlock();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        if (!isSwinging || currentBlock == null) return;

        timeCounter += Time.deltaTime * swingSpeed;
        float angle = Mathf.Sin(timeCounter) * swingAngle;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 craneWorldPos = transform.position;
        float blockX = craneWorldPos.x + Mathf.Sin(rad) * ropeLength;
        float blockY = craneWorldPos.y - Mathf.Cos(rad) * ropeLength;

        currentBlock.transform.position = new Vector3(blockX, blockY, 0f);
        currentBlock.transform.rotation = Quaternion.identity;

        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, craneWorldPos);
            lineRenderer.SetPosition(1, currentBlock.transform.position);
        }
    }

    public void DropBlock()
    {
        if (!isSwinging || currentBlock == null) return;

        isSwinging = false;

        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (lineRenderer != null)
            lineRenderer.enabled = false;

        AudioManager.Instance.PlayBlockDrop();
    }

    public void ResetCrane(GameObject newBlock)
    {
        currentBlock = newBlock;
        newBlock.transform.SetParent(null);
        timeCounter = 0f;
        isSwinging = true;
    }
}