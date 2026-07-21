using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource sfxSource;

    [Header("Death Sounds")]
    [SerializeField] private AudioClip[] crashClips;
    [SerializeField] private AudioClip headOnClip;

    [Header("Round / Game Sounds")]
    [SerializeField] private AudioClip winnerClip;
    [SerializeField] private AudioClip drawClip;

    [Header("Countdown Sounds")]
    [SerializeField] private AudioClip countdownBlipClip;
    [SerializeField] private AudioClip goClip;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip buttonHoverClip;
    [SerializeField] private AudioClip buttonClickClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCrash()
    {
        if (crashClips != null && crashClips.Length > 0)
        {
            AudioClip clip = crashClips[Random.Range(0, crashClips.Length)];
            PlayClip(clip);
        }
    }

    public void PlayHeadOn()       { PlayClip(headOnClip); }
    public void PlayWinner()       { PlayClip(winnerClip); }
    public void PlayDraw()         { PlayClip(drawClip); }
    public void PlayBlip()         { PlayClip(countdownBlipClip); }
    public void PlayGo()           { PlayClip(goClip); }
    public void PlayButtonHover()  { PlayClip(buttonHoverClip); }
    public void PlayButtonClick()  { PlayClip(buttonClickClip); }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}