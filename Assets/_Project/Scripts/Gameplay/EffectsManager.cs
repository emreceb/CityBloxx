using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    public ParticleSystem perfectEffect;
    public ParticleSystem goodEffect;
    public ParticleSystem badEffect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayEffect(string type, Vector3 position)
    {
        ParticleSystem effect = null;

        if (type == "perfect") effect = perfectEffect;
        else if (type == "good") effect = goodEffect;
        else if (type == "bad") effect = badEffect;

        if (effect != null)
        {
            effect.transform.position = position;
            effect.Play();
        }
    }
}