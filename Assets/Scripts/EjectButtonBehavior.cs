using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectButtonBehavior : MonoBehaviour
{
    [SerializeField]
    SelectableManager selectableManager;

    [SerializeField]
    GameObject UIText;

    [SerializeField]
    DisketBehavior[] diskets;

    public void ObjectSelected()
    {
        UIText.SetActive(false);
        GetComponentInParent<Animator>().SetTrigger("push");

        foreach (DisketBehavior disket in diskets)
        {
            if (disket.IsPlugged())
            {
                disket.ObjectUnplugged();
            }
        }
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
}
