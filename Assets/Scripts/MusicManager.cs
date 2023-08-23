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

    private int index = 0;

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
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            index = 0;
            radio.Stop();
            radio.PlayOneShot(music[index]);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            index = 1;
            radio.Stop();
            radio.PlayOneShot(music[index]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            index = 2;
            radio.Stop();
            radio.PlayOneShot(music[index]);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            index = 3;
            radio.Stop();
            radio.PlayOneShot(music[index]);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            index = 4;
            radio.Stop();
            radio.PlayOneShot(music[index]);
        }
        else { }
    }
}
