using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX")]
    public AudioClip blockDropSound;
    public AudioClip perfectSound;
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    [Header("Music")]
    public AudioClip backgroundMusic;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
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
        if (musicSource == null) return;
        musicSource.volume = isOn ? 0.5f : 0f;
    }
}