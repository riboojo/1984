using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchBehavior : MonoBehaviour
{
    [SerializeField]
    SelectableManager selectableManager;

    [SerializeField]
    GameObject UIText;

    bool switchOn = false;

    public void ObjectSelected()
    {
        if (switchOn)
        {
            GetComponentInParent<Animator>().SetTrigger("off");
            switchOn = false;
        }
        else
        {
            GetComponentInParent<Animator>().SetTrigger("on");
            switchOn = true;
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
}
