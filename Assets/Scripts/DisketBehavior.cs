using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisketBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    bool objectShown = false;
    bool objectPlugged = false;

    void Update()
    {
        if (objectShown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GetComponent<Animator>().SetTrigger("hide");
                SelectableManager.GetInstance().ObjectReleased();
                objectShown = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
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
            UIText.GetComponent<TextMesh>().text = "Inspect\n(Right Click)";
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
            ScreenManager.GetInstance().SetNoise();
            objectPlugged = false;
        }
    }

    public bool IsPlugged()
    {
        return objectPlugged;
    }
    
}
