using System.Collections.Generic;
using UnityEngine;

public class SelectableManager : MonoBehaviour
{
    private static SelectableManager instance;

    [SerializeField]
    MainSwitchBehavior mainSwitch;

    [SerializeField]
    DisketBehavior[] diskets;

    [SerializeField]
    Light spotLight;

    RaycastHit lastHit = new RaycastHit();

    bool objectSelected = false;
    bool disketPlugged = false;
    bool mouseClicked = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;
    }

    public static SelectableManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        objectSelected = true;
        TurnSpotlight(true);
    }

    void Update()
    {
        if (!objectSelected)
        {
            CheckSelectables();
        }
    }

    void CheckSelectables()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit currentHit;
        if (Physics.Raycast(ray, out currentHit))
        {
            if (currentHit.transform.tag == "Selectable")
            {
                Outline outline;

                if (lastHit.collider != null && currentHit.collider != null)
                {
                    if (currentHit.collider.gameObject.TryGetComponent<Outline>(out outline))
                    {
                        outline.OutlineWidth = 5f;
                    }
                    else
                    {
                        outline = currentHit.collider.gameObject.AddComponent<Outline>();

                        outline.OutlineMode = Outline.Mode.OutlineAll;
                        outline.OutlineColor = Color.yellow;
                        outline.OutlineWidth = 5f;
                    }

                    if (lastHit.collider.GetHashCode() != currentHit.collider.GetHashCode())
                    {
                        ObjectHigligthed(currentHit, true);
                        CleanLastSelectable();
                        lastHit = currentHit;
                    }

                    CheckClick(currentHit.collider.gameObject);
                }
            }
            else
            {
                CleanLastSelectable();
                lastHit = currentHit;
            }
        }
        else
        {
            CleanLastSelectable();
            lastHit = currentHit;
        }
    }

    void CheckClick(GameObject selected)
    {
        if (Input.GetMouseButton(0))
        {
            mouseClicked = true;
        }

        if (!Input.GetMouseButton(0) && mouseClicked)
        {
            mouseClicked = false;

            ObjectSelected(selected);
        }
    }

    void CleanLastSelectable()
    {
        Outline outline;

        if (lastHit.collider != null)
        {
            if (lastHit.collider.gameObject.TryGetComponent<Outline>(out outline))
            {
                outline.OutlineWidth = 0f;
                ObjectHigligthed(lastHit, false);
            }
        }
    }

    void ObjectSelected(GameObject selected)
    {
        Outline outline;

        DisketBehavior disket;
        disket = selected.GetComponentInParent<DisketBehavior>();

        if (disket != null && !disketPlugged)
        {
            if (disket.ObjectSelected())
            {
                objectSelected = true;
                TurnSpotlight(true);

                if (lastHit.collider.gameObject.TryGetComponent<Outline>(out outline))
                {
                    outline.OutlineWidth = 0f;
                }
            }
        }

        MainSwitchBehavior mainSwitch;
        mainSwitch = selected.GetComponentInParent<MainSwitchBehavior>();

        if (mainSwitch != null)
        {
            mainSwitch.ObjectSelected();
        }

        EjectButtonBehavior ejectButton;
        ejectButton = selected.GetComponentInParent<EjectButtonBehavior>();

        if (ejectButton != null)
        {
            ejectButton.ObjectSelected();
        }

        NotepadBehavior notepad;
        notepad = selected.GetComponentInParent<NotepadBehavior>();

        if (notepad != null)
        {
            if (notepad.ObjectSelected())
            {
                objectSelected = true;
                TurnSpotlight(true);

                if (lastHit.collider.gameObject.TryGetComponent<Outline>(out outline))
                {
                    outline.OutlineWidth = 0f;
                }
            }
        }

        RadioBehavior radio;
        radio = selected.GetComponent<RadioBehavior>();

        if (radio != null)
        {
            radio.ObjectSelected();
        }
    }

    void ObjectHigligthed(RaycastHit hit, bool higlighted)
    {
        DisketBehavior disket;
        disket = hit.collider.gameObject.GetComponentInParent<DisketBehavior>();

        if (disket != null)
        {
            if (!IsAnyDisketPlugged())
            {
                disket.ObjectHigligthed(higlighted);
            }
        }

        MainSwitchBehavior mainSwitch;
        mainSwitch = hit.collider.gameObject.GetComponentInParent<MainSwitchBehavior>();

        if (mainSwitch != null)
        {
            mainSwitch.ObjectHigligthed(higlighted);
        }

        EjectButtonBehavior ejectButton;
        ejectButton = hit.collider.gameObject.GetComponentInParent<EjectButtonBehavior>();

        if (ejectButton != null)
        {
            ejectButton.ObjectHigligthed(higlighted);
        }

        NotepadBehavior notepad;
        notepad = hit.collider.gameObject.GetComponentInParent<NotepadBehavior>();

        if (notepad != null)
        {
            notepad.ObjectHigligthed(higlighted);
        }

        RadioBehavior radio;
        radio = hit.collider.gameObject.GetComponent<RadioBehavior>();

        if (radio != null)
        {
            radio.ObjectHigligthed(higlighted);
        }
    }

    void TurnSpotlight(bool turnOn)
    {
        if (turnOn)
        {
            spotLight.intensity = 0.19f;
        }
        else
        {
            spotLight.intensity = 0f;
        }
    }

    public void ObjectReleased()
    {
        objectSelected = false;
        TurnSpotlight(false);
    }

    public void DisketPlugged(bool plugged)
    {
        disketPlugged = plugged;
    }

    public bool IsAnyDisketPlugged()
    {
        bool ret = false; 

        foreach (DisketBehavior disket in diskets)
        {
            if (disket.IsPlugged())
            {
                ret = true;
                break;
            }
        }

        return ret;
    }

}
