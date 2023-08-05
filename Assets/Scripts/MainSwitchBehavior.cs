using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitchBehavior : MonoBehaviour
{
    [SerializeField]
    SelectableManager selectableManager;

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
}
