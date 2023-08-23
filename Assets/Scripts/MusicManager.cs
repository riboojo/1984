using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField]
    AudioSource radio;

    [SerializeField]
    AudioClip[] music;

    private int index = 1;
    private bool isPlaying = false;
    private const int MAX_SONGS = 4;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    public static MusicManager GetInstance()
    {
        return instance;
    }

    public void Play()
    {
        isPlaying = true;
        radio.PlayOneShot(music[index]);
    }

    public void Stop()
    {
        isPlaying = false;
        radio.Stop();
    }

    public void PlayNextSong()
    {
        if (isPlaying)
        {
            radio.Stop();

            if (index < MAX_SONGS)
            {
                index++;
            }
            else
            {
                index = 0;
            }

            radio.PlayOneShot(music[index]);
        }
    }
}
