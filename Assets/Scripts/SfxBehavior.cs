using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxBehavior : MonoBehaviour
{
    [SerializeField]
    private AudioClip effect_0, effect_1;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio_0()
    {
        source.clip = effect_0;
        source.Play();
    }

    public void PlayAudio_1()
    {
        source.clip = effect_1;
        source.Play();
    }
}
