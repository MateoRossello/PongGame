using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    private readonly string backgroundRoute = "Background/Background";
    [SerializeField] private MusicPlayer musicPlayer;
    private readonly string musicRoute = "Music";

    private void Start()
    {
        LoadBackground();
        LoadMusic();
    }

    private void LoadBackground()
    {
        if (!TryGetComponent<Renderer>(out var renderer))
        {
            Debug.LogError("Not find Renderer component.");
            return;
        }

        Texture2D texture = Resources.Load<Texture2D>(backgroundRoute);
        if (texture != null)
        {
            renderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogWarning("Not find Background component.");
        }
    }

    private void LoadMusic()
    {
        if (musicPlayer == null)
        {
            Debug.Log("Not find MusicPlayer component.");
            return;
        }

        List<AudioClip> playlist = new(Resources.LoadAll<AudioClip>(musicRoute));
        if (playlist != null & playlist.Count > 0)
        {
            musicPlayer.SetPlaylist(playlist);
        }
        else
        {
            Debug.Log("Not find music components.");
        }
    }
}
