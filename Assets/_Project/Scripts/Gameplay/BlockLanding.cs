using UnityEngine;
public class BlockLanding : MonoBehaviour
{
    private bool hasLanded = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLanded) return;
        Debug.Log("Çarpýþma: " + collision.gameObject.name + " | Tag: " + collision.gameObject.tag);
        hasLanded = true;
        TowerManager.Instance.BlockLanded(gameObject, transform.position.x);
    }
}