using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UIText;

    bool objectShown = false;

    [SerializeField]
    AudioSource notepadAudio;
 
    private void Start()
    {
        notepadAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (objectShown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                notepadAudio.Play();
                GetComponent<Animator>().SetTrigger("hide");
                SelectableManager.GetInstance().ObjectReleased();
                objectShown = false;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                notepadAudio.Play();
                GetComponent<Animator>().SetTrigger("turn");
            }
            else { /* Do nothiing */ }
        }
    }

    public bool ObjectSelected()
    {
        bool ret = true;

        ShowNotepad();

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

    public void ShowNotepad()
    {
        notepadAudio.Play();
        UIText.SetActive(false);
        GetComponent<Animator>().SetTrigger("show");
        objectShown = true;
    }
}
