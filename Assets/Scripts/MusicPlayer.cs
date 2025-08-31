using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playlist;
    // Audio source volume
    [SerializeField] [Range(0f, 1f)] private float volume = 0.05f;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    // Music must be paused or not
    private bool musicShouldBePlaying = true;

    // Singleton
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
            Debug.LogError("Not find AudioSource component.");
            return;
        }
        audioSource.volume = volume;
    }

    private void Start()
    {
        PlayNextTrack();
    }

    private void Update()
    {
        if(musicShouldBePlaying && !audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
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

    private void OnApplicationPause(bool pauseStatus)
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

    private void PlayNextTrack()
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

    public void SetPlaylist(List<AudioClip> playlist)
    {
        if(playlist != null)
        {
            this.playlist = playlist;

            ShufflePlaylist();

            currentTrackIndex = 0;
            PlayNextTrack();
        }
    }

    private void ShufflePlaylist()
    {
        playlist = playlist.OrderBy(x => Random.value).ToList();
    }
}
