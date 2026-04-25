using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("Parallax Settings")]
    public Transform[] layers;
    public float[] layerSpeeds;

    private float lastCameraY;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        lastCameraY = cam.transform.position.y;
    }

    private void LateUpdate()
    {
        float deltaY = cam.transform.position.y - lastCameraY;

        for (int i = 0; i < layers.Length; i++)
        {
            if (i < layerSpeeds.Length && layers[i] != null)
            {
                layers[i].position += new Vector3(0f, deltaY * layerSpeeds[i], 0f);
            }
        }

        lastCameraY = cam.transform.position.y;
    }
}