using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisketBehavior : MonoBehaviour
{
    [SerializeField]
    string disketName;

    [SerializeField]
    GameObject UIText;

    [SerializeField]
    AudioClip show;

    [SerializeField]
    AudioClip insert;

    bool objectShown = false;
    bool objectPlugged = false;

    AudioSource disketAudio;

    private void Start()
    {
        disketAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (objectShown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                disketAudio.clip = show;
                disketAudio.Play();

                GetComponent<Animator>().SetTrigger("hide");
                SelectableManager.GetInstance().ObjectReleased();
                objectShown = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                disketAudio.clip = insert;
                disketAudio.Play();

                GetComponent<Animator>().SetTrigger("play");
                SelectableManager.GetInstance().ObjectReleased();
                SelectableManager.GetInstance().DisketPlugged(true);
                objectShown = false;
                objectPlugged = true;

                CheckScreen();
            }

        }
    }

    public bool ObjectSelected()
    {
        bool ret = false;

        if (!objectPlugged)
        {
            disketAudio.clip = show;
            disketAudio.Play();

            UIText.SetActive(false);
            GetComponent<Animator>().SetTrigger("show");
            objectShown = true;

            ret = true;
        }

        return ret;
    }

    public void ObjectHigligthed(bool higlighted)
    {
        if (higlighted && !objectPlugged)
        {
            UIText.SetActive(true);
            UIText.GetComponent<TextMesh>().text = "Inspect\n(Left Click)";
        }
        else
        {
            UIText.SetActive(false);
        }
    }

    public void ObjectUnplugged()
    {
        if (objectPlugged)
        {
            GetComponent<Animator>().SetTrigger("out");
            SelectableManager.GetInstance().DisketPlugged(false);
            ConversationManager.GetInstance().DisketUnplugged();
            objectPlugged = false;
        }
    }

    public bool IsPlugged()
    {
        return objectPlugged;
    }

    public string GetName()
    {
        return disketName;
    }

    private void CheckScreen()
    {
        if (ScreenManager.GetInstance().IsScreenOn())
        {
            if (ConversationManager.GetInstance().NewDisketPlayed())
            {
                ScreenManager.GetInstance().ClearNoise();
            }
        }
    }
}
