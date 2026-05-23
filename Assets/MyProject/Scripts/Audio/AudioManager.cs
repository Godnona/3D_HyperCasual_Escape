using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Game Clips")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip challengeMapMusic;
    [SerializeField] private AudioClip shopMapMusic;
    [SerializeField] private AudioClip buttonClickSfx;
    [SerializeField] private AudioClip obstacleHitSfx;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.7f;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float challengeMusicVolumeScale = 0.45f;

    private float currentMusicVolumeScale = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        EnsureSources();
        ApplyVolumes();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ApplyMusicForScene(SceneManager.GetActiveScene().name);
    }

    private void EnsureSources()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
        }
    }

    private void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume * currentMusicVolumeScale;

        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float volumeScale = 1f)
    {
        if (clip == null || musicSource == null)
            return;

        currentMusicVolumeScale = Mathf.Clamp01(volumeScale);
        musicSource.volume = musicVolume * currentMusicVolumeScale;

        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlaySfx(AudioClip clip, float volumeScale = 1f)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip, volumeScale);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyMusicForScene(scene.name);
    }

    private void ApplyMusicForScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            PlayMainMenuMusic();
            return;
        }

        if (sceneName.StartsWith("Map"))
        {
            if (IsShopMapScene(sceneName))
            {
                PlayShopMapMusic();
                return;
            }

            PlayChallengeMapMusic();
        }
    }

    private bool IsShopMapScene(string sceneName)
    {
        return sceneName == "Map7" || sceneName == "Map10";
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null)
            PlayMusic(mainMenuMusic, true, 1f);
    }

    public void PlayChallengeMapMusic()
    {
        if (challengeMapMusic != null)
            PlayMusic(challengeMapMusic, true, challengeMusicVolumeScale);
    }

    public void PlayShopMapMusic()
    {
        if (shopMapMusic != null)
            PlayMusic(shopMapMusic, true, 1f);
    }

    public void PlayButtonClick()
    {
        if (buttonClickSfx != null)
            PlaySfx(buttonClickSfx);
    }

    public void PlayObstacleHit()
    {
        if (obstacleHitSfx != null)
            PlaySfx(obstacleHitSfx);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void SetSfxVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    public void SetMute(bool isMuted)
    {
        if (musicSource != null)
            musicSource.mute = isMuted;

        if (sfxSource != null)
            sfxSource.mute = isMuted;
    }
}
