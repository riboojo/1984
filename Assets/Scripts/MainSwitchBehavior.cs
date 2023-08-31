using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    [SerializeField]
    Material off, on;

    [SerializeField]
    MeshRenderer led;

    bool switchOn = false;

    AudioSource switchAudio;

    private void Start()
    {
        switchAudio = GetComponent<AudioSource>();
    }

    public void ObjectSelected()
    {
        switchAudio.Play();

        if (!switchOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void ObjectHigligthed(bool higlighted)
    {
        if (higlighted)
        {
            UIText.SetActive(true);

            if (switchOn)
            {
                UIText.GetComponent<TextMesh>().text = "Turn Off\n(Left Click)";
            }
            else
            {
                UIText.GetComponent<TextMesh>().text = "Turn On\n(Left Click)";
            }
        }
        else
        {
            UIText.SetActive(false);
        }
    }

    public void TurnOn()
    {
        GetComponentInParent<Animator>().SetTrigger("on");
        led.material = on;
        switchOn = true;

        CheckNewDisket();
        ScreenManager.GetInstance().TurnOnScreen();
    }

    public void TurnOff()
    {
        GetComponentInParent<Animator>().SetTrigger("off");
        led.material = off;
        switchOn = false;

        ScreenManager.GetInstance().TurnOffScreen();
    }

    public void CheckNewDisket()
    {
        if (!ConversationManager.GetInstance().NewDisketPlayed())
        {
            ScreenManager.GetInstance().SetNoise();
        }
        else
        {
            //ScreenManager.GetInstance().ClearNoise();
        }
    }
}
