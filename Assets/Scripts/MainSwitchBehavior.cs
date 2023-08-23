using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    bool switchOn = false;

    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ObjectSelected()
    {
        audio.Play();

        if (switchOn)
        {
            ScreenManager.GetInstance().TurnOffScreen();
            GetComponentInParent<Animator>().SetTrigger("off");
            switchOn = false;
        }
        else
        {
            GetComponentInParent<Animator>().SetTrigger("on");
            ScreenManager.GetInstance().TurnOnScreen();
            switchOn = true;

            if (!ConversationManager.GetInstance().NewDisketPlayed())
            {
                ScreenManager.GetInstance().SetNoise();
            }
            else
            {
                ScreenManager.GetInstance().ClearNoise();
            }
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

    public void TurnOff()
    {
        ScreenManager.GetInstance().ClearNoise();
        ScreenManager.GetInstance().TurnOffScreen();
        GetComponentInParent<Animator>().SetTrigger("off");
        switchOn = false;
    }
}
