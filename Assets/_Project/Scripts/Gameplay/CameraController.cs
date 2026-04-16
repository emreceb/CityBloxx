using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 4f;
    private float targetY;

    private void Start()
    {
        targetY = 0f;
    }

    private void LateUpdate()
    {
        if (TowerManager.Instance == null) return;

        float towerTop = TowerManager.Instance.GetCurrentTopY();

        if (towerTop > 3f)
        {
            targetY = Mathf.Lerp(targetY, towerTop - 3f, Time.deltaTime * smoothSpeed);
            transform.position = new Vector3(0f, targetY, -10f);
        }
    }
}