using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    public float smoothSpeed = 3f;
    public float cameraOffset = 2f;

    private float targetY;

    private void Start()
    {
        targetY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (TowerManager.Instance == null) return;

        float towerTop = TowerManager.Instance.GetCurrentTopY();
        float craneY = towerTop + 5f;

        // Kamera vincin biraz altýnda durur — kuleyi ve vinci ikisi de gösterir
        float desiredY = craneY - cameraOffset;

        targetY = Mathf.Lerp(targetY, desiredY, Time.deltaTime * smoothSpeed);

        transform.position = new Vector3(
            transform.position.x,
            targetY,
            transform.position.z
        );
    }
}