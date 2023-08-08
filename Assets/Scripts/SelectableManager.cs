using System.Collections.Generic;
using UnityEngine;

public class SelectableManager : MonoBehaviour
{
    private static SelectableManager instance;

    [SerializeField]
    MainSwitchBehavior mainSwitch;

    [SerializeField]
    DisketBehavior[] diskets;

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
    }

    public void ObjectReleased()
    {
        objectSelected = false;
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
