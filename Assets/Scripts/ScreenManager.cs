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

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    public static ScreenManager GetInstance()
    {
        return instance;
    }

    public void TurnOnScreen()
    {
        GetComponentInParent<Animator>().SetTrigger("on");
    }

    public void TurnOffScreen()
    {
        ClearNoise();
        GetComponentInParent<Animator>().SetTrigger("off");
    }

    public void SetNoise()
    {
        gameObject.GetComponent<Renderer>().material = noise;
    }

    public void ClearNoise()
    {
        gameObject.GetComponent<Renderer>().material = normal;
    }
}
