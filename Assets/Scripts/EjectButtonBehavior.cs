using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectButtonBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    [SerializeField]
    DisketBehavior[] diskets;

    [SerializeField]
    MainSwitchBehavior mainSwitch;

    AudioSource buttonAudio;

    private void Start()
    {
        buttonAudio = GetComponent<AudioSource>();
    }

    public void ObjectSelected()
    {
        EjectDisket();
    }

    public void ObjectHigligthed(bool higlighted)
    {
        if (higlighted)
        {
            UIText.SetActive(true);
            UIText.GetComponent<TextMesh>().text = "Eject Disket\n(Left Click)";
        }
        else
        {
            UIText.SetActive(false);
        }
    }

    public void EjectDisket()
    {
        buttonAudio.Play();

        UIText.SetActive(false);
        GetComponentInParent<Animator>().SetTrigger("push");

        foreach (DisketBehavior disket in diskets)
        {
            if (disket.IsPlugged())
            {
                disket.ObjectUnplugged();
                mainSwitch.TurnOff();
            }
        }
    }
}
