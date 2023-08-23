using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisketBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    [SerializeField]
    AudioClip show;

    [SerializeField]
    AudioClip insert;

    bool objectShown = false;
    bool objectPlugged = false;

    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (objectShown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                audio.clip = show;
                audio.Play();

                GetComponent<Animator>().SetTrigger("hide");
                SelectableManager.GetInstance().ObjectReleased();
                objectShown = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                audio.clip = insert;
                audio.Play();

                GetComponent<Animator>().SetTrigger("play");
                SelectableManager.GetInstance().ObjectReleased();
                SelectableManager.GetInstance().DisketPlugged(true);
                objectShown = false;
                objectPlugged = true;
            }

        }
    }

    public bool ObjectSelected()
    {
        bool ret = false;

        if (!objectPlugged)
        {
            audio.clip = show;
            audio.Play();

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
    
}
