using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;

    [SerializeField]
    Material noise;

    [SerializeField]
    Material normal;

    [SerializeField]
    Animator led;

    private bool isOn;
    private bool isNoise;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
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
        gameObject.GetComponent<Renderer>().material = noise;
    }

    public void ClearNoise()
    {
        isNoise = false;
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
