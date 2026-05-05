using UnityEngine;

public class BlockLanding : MonoBehaviour
{
    private bool hasLanded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLanded) return;

        string colName = collision.gameObject.name;

        if (colName == "Ground" || colName.Contains("Block"))
        {
            hasLanded = true;
            TowerManager.Instance.BlockLanded(gameObject, transform.position.x);
        }
    }
}