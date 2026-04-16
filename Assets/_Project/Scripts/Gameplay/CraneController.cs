using UnityEngine;

public class CraneController : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingSpeed = 1.2f;
    public float swingAngle = 35f;
    public float ropeLength = 4f;

    [Header("References")]
    public GameObject currentBlock;
    public LineRenderer lineRenderer;

    private bool isSwinging = true;
    private float timeCounter = 0f;
    private Vector3 anchorPoint;

    private void Start()
    {
        anchorPoint = transform.position;
        TowerManager.Instance.SpawnNextBlock();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        // Vinç anchor noktasý her zaman ekranýn üstünde sabit X:0'da
        anchorPoint = new Vector3(0f, transform.position.y, 0f);

        if (isSwinging && currentBlock != null)
        {
            timeCounter += Time.deltaTime * swingSpeed;
            float angle = Mathf.Sin(timeCounter) * swingAngle;
            float rad = angle * Mathf.Deg2Rad;

            float blockX = anchorPoint.x + Mathf.Sin(rad) * ropeLength;
            float blockY = anchorPoint.y - Mathf.Cos(rad) * ropeLength;

            currentBlock.transform.position = new Vector3(blockX, blockY, 0f);
            currentBlock.transform.rotation = Quaternion.identity;

            // Ýpi çiz
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, anchorPoint);
                lineRenderer.SetPosition(1, currentBlock.transform.position);
            }
        }

        // Vinç kuleyle yükselir
        if (TowerManager.Instance != null)
        {
            float towerTop = TowerManager.Instance.GetCurrentTopY();
            float targetY = Mathf.Max(towerTop + ropeLength + 2f, 6f);
            float newY = Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * 4f);
            transform.position = new Vector3(0f, newY, 0f);
        }
    }

    public void DropBlock()
    {
        if (!isSwinging) return;
        if (currentBlock == null) return;

        isSwinging = false;

        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    public void ResetCrane(GameObject newBlock)
    {
        currentBlock = newBlock;
        newBlock.transform.SetParent(null);
        timeCounter = 0f;
        isSwinging = true;
    }
}