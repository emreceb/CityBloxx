using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioClip blockDropSound;
    public AudioClip perfectSound;
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioListener.volume = 1f;
        Debug.Log("SfxSource: " + sfxSource);
        Debug.Log("BlockDropSound: " + blockDropSound);

        // Direkt AudioSource üzerinden test
        sfxSource.clip = blockDropSound;
        sfxSource.Play();
        Debug.Log("Play cagrildi");
    }

    public void PlaySFX(AudioClip clip)
    {
        
        
        if (clip == null)
        {
            Debug.Log("Clip NULL!");
            return;
        }
        Debug.Log("Ses caliniyor: " + clip.name);
        sfxSource.PlayOneShot(clip);

    }

    public void PlayBlockDrop() => PlaySFX(blockDropSound);
    public void PlayPerfect() => PlaySFX(perfectSound);
    public void PlayGood() => PlaySFX(goodSound);
    public void PlayBad() => PlaySFX(badSound);
    public void PlayGameOver() => PlaySFX(gameOverSound);
    public void PlayButtonClick() => PlaySFX(buttonClickSound);

    public void ToggleMusic(bool isOn)
    {
        musicSource.volume = isOn ? 1f : 0f;
    }
}