using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton;

    public AudioSource backgroundMusicSource;
    public AudioSource gameMusicSource;
    public AudioSource gameSoundSource;

    public AudioClip music_background;
    public AudioClip music_gameWon;
    public AudioClip music_gameLost;
    public AudioClip music_playerrun;
    public AudioClip sound_cookieGrab;
    public AudioClip sound_buttonClick;
    public AudioClip sound_panelpop;
    public AudioClip sound_wavelCompleted;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }
    public void PlayBackGroundMusic()
    {
        backgroundMusicSource.Play();
    }

    public void StopBackGroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void PlayGameMusic()
    {
        gameMusicSource.Play();
    }
    public void StopGameMusic()
    {
        gameMusicSource.Stop();
    }

    public void PlayOneShotSFX(AudioClip clip)
    {
        gameSoundSource.PlayOneShot(clip);
    }
}
