using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 2f;
    private float targetY;
    private float initialTopY;
    private bool initialized = false;

    private void Start()
    {
        targetY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (TowerManager.Instance == null) return;

        float towerTop = TowerManager.Instance.GetCurrentTopY();

        if (!initialized)
        {
            initialTopY = towerTop;
            initialized = true;
            return;
        }

        // Baþlangýçtan 3 birim yükseldikten sonra yavaþça takip et
        float risen = towerTop - initialTopY;
        if (risen > 3f)
        {
            float desiredY = risen - 3f;
            targetY = Mathf.Lerp(targetY, desiredY, Time.deltaTime * smoothSpeed);
            transform.position = new Vector3(0f, targetY, -10f);
        }
    }
}