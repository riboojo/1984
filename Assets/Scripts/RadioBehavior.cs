using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject UITextTurner;

    [SerializeField]
    GameObject UITextOnOff;

    [SerializeField]
    bool isTurner;

    [SerializeField]
    MeshRenderer indicator;

    [SerializeField]
    Material red;

    [SerializeField]
    Material green;

    bool isOn = false;
    
    public bool ObjectSelected()
    {
        bool ret = true;

        if (!isTurner)
        {
            isOn = !isOn;

            if (isOn)
            {
                indicator.material = green;
                MusicManager.GetInstance().Play();
                UITextOnOff.GetComponent<TextMesh>().text = "Turn Off\n(Left Click)";
            }
            else
            {
                indicator.material = red;
                MusicManager.GetInstance().Stop();
                UITextOnOff.GetComponent<TextMesh>().text = "Turn On\n(Left Click)";
            }
        }
        else
        {
            MusicManager.GetInstance().PlayNextSong();
        }

        return ret;
    }

    public void ObjectHigligthed(bool higlighted)
    {
        if (higlighted)
        {
            if (isTurner)
            {
                UITextTurner.SetActive(true);
                UITextTurner.GetComponent<TextMesh>().text = "Next station\n(Left Click)";
            }
            else
            {
                UITextOnOff.SetActive(true);

                if (isOn)
                {
                    UITextOnOff.GetComponent<TextMesh>().text = "Turn Off\n(Left Click)";
                }
                else
                {
                    UITextOnOff.GetComponent<TextMesh>().text = "Turn On\n(Left Click)";
                }
            }
        }
        else
        {
            UITextTurner.SetActive(false);
            UITextOnOff.SetActive(false);
        }
    }
}
