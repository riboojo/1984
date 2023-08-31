using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;
    
    [SerializeField]
    Animator led;

    [SerializeField]
    GameObject noise;

    private bool isOn;
    private bool isNoise;

    AudioSource screenAudio;

    Color black = Color.black;
    Color transparent = new Color(0f,0f,0f,125f);

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        screenAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOn)
        {
            if (!isNoise)
            {
                led.SetTrigger("green");
            }
            else
            {
                led.SetTrigger("flash");
            }
        }
        else
        {
            led.SetTrigger("red");
        }
    }

    public static ScreenManager GetInstance()
    {
        return instance;
    }

    public void TurnOnScreen()
    {
        isOn = true;
        GetComponentInParent<Animator>().SetTrigger("on");
    }

    public void TurnOffScreen()
    {
        isOn = false;
        ClearNoise();
        GetComponentInParent<Animator>().SetTrigger("off");
    }

    public void SetNoise()
    {
        isNoise = true;
        noise.SetActive(true);
        GetComponentInParent<Animator>().SetBool("noise", true);
        screenAudio.Play();
    }

    public void ClearNoise()
    {
        isNoise = false;
        noise.SetActive(false);
        GetComponentInParent<Animator>().SetBool("noise", false);
        screenAudio.Stop();
    }

    public bool IsScreenOn()
    {
        return isOn;
    }
}
