using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playlist;
    [SerializeField] [Range(0f, 1f)] private float volume = 0.05f;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private bool musicShouldBePlaying = true;

    public static MusicPlayer Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("Not find AudioSource component on MusicPlayer.");
            return;
        }

        audioSource.volume = volume;
    }

    void Start()
    {
        PlayNextTrack();
    }

    void Update()
    {
        if(musicShouldBePlaying && !audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(hasFocus)
        {
            audioSource.UnPause();
            musicShouldBePlaying = true;
        }
        else
        {
            audioSource.Pause();
            musicShouldBePlaying = false;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
        {
            audioSource.Pause();
            musicShouldBePlaying = false;
        }
        else
        {
            audioSource.UnPause();
            musicShouldBePlaying = true;
        }
    }

    void PlayNextTrack()
    {
        if(playlist.Count == 0)
        {
            Debug.LogWarning("Playlist is empty. No music to play.");
            return;
        }

        audioSource.clip = playlist[currentTrackIndex];
        audioSource.Play();

        currentTrackIndex++;

        if(currentTrackIndex >= playlist.Count)
        {
            currentTrackIndex = 0;
        }
    }
}
