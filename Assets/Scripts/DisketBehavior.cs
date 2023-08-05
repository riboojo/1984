using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisketBehavior : MonoBehaviour
{
    [SerializeField]
    SelectableManager selectableManager;

    bool objectShown = false;

    void Update()
    {
        if (objectShown)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponentInParent<Animator>().SetTrigger("hide");
                selectableManager.ObjectReleased();
                objectShown = false;
            }

            if (Input.GetMouseButton(1))
            {
                GetComponentInParent<Animator>().SetTrigger("play");
                selectableManager.ObjectReleased();
                objectShown = false;
            }

        }
    }

    public void ObjectSelected()
    {
        GetComponentInParent<Animator>().SetTrigger("show");
        objectShown = true;
    }
}
