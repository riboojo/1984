using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    bool objectShown = false;

    [SerializeField]
    AudioSource audio;
    
    private void Start()
    {
        UIText.SetActive(false);
        objectShown = true;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (objectShown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                audio.Play();
                GetComponent<Animator>().SetTrigger("hide");
                SelectableManager.GetInstance().ObjectReleased();
                objectShown = false;
            }
        }
    }

    public bool ObjectSelected()
    {
        bool ret = true;

        audio.Play();
        UIText.SetActive(false);
        GetComponent<Animator>().SetTrigger("show");
        objectShown = true;

        return ret;
    }

    public void ObjectHigligthed(bool higlighted)
    {
        if (higlighted)
        {
            UIText.SetActive(true);
            UIText.GetComponent<TextMesh>().text = "Inspect\n(Left Click)";
        }
        else
        {
            UIText.SetActive(false);
        }
    }
}
