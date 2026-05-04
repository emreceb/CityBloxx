using UnityEngine;

public class BlockLanding : MonoBehaviour
{
    private bool hasLanded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLanded) return;
        hasLanded = true;
        TowerManager.Instance.BlockLanded(gameObject, transform.position.x);
    }
}