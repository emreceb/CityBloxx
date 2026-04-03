using UnityEngine;

public class CraneController : MonoBehaviour
{
    [Header("Crane Settings")]
    public float swingSpeed = 1.5f;
    public float swingAngle = 45f;
    public Transform blockHangPoint;

    [Header("References")]
    public GameObject currentBlock;

    private bool isSwinging = true;
    private float timeCounter = 0f;

    private void Start()
    {
        // Baþlangýçta ilk bloðu oluþtur
        TowerManager.Instance.SpawnNextBlock();
    }

    private void Update()
    {
        if (!isSwinging) return;
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        timeCounter += Time.deltaTime * swingSpeed;
        float angle = Mathf.Sin(timeCounter) * swingAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void DropBlock()
    {
        if (!isSwinging) return;
        if (currentBlock == null) return;

        isSwinging = false;

        currentBlock.transform.SetParent(null);

        Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("Block dropped!");
    }

    public void ResetCrane(GameObject newBlock)
    {
        currentBlock = newBlock;
        timeCounter = 0f;
        isSwinging = true;
        transform.rotation = Quaternion.identity;
    }
}