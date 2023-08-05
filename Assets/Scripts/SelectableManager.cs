using System.Collections.Generic;
using UnityEngine;

public class SelectableManager : MonoBehaviour
{
    RaycastHit lastHit = new RaycastHit();

    bool objectSelected = false;
    bool mouseClicked = false;
    
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

                if (currentHit.collider.gameObject.TryGetComponent<Outline>(out outline))
                {
                    outline.OutlineWidth = 7f;
                }
                else
                {
                    outline = currentHit.collider.gameObject.AddComponent<Outline>();

                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.yellow;
                    outline.OutlineWidth = 7f;
                }

                if (lastHit.GetHashCode() != currentHit.GetHashCode())
                {
                    CleanLastSelectable();
                    lastHit = currentHit;
                }
                
                CheckClick(currentHit.collider.gameObject);
            }
            else
            {
                CleanLastSelectable();
            }
        }
        else
        {
            CleanLastSelectable();
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

    void ObjectSelected(GameObject selected)
    {
        Outline outline;

        DisketBehavior disket;
        disket = selected.GetComponentInParent<DisketBehavior>();

        if (disket != null)
        {
            objectSelected = true;
            disket.ObjectSelected();

            if (lastHit.collider.gameObject.TryGetComponent<Outline>(out outline))
            {
                outline.OutlineWidth = 0f;
            }
        }

        MainSwitchBehavior mainSwitch;
        mainSwitch = selected.GetComponentInParent<MainSwitchBehavior>();

        if (mainSwitch != null)
        {
            mainSwitch.ObjectSelected();
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
            }
        }
    }

    public void ObjectReleased()
    {
        objectSelected = false;
    }
}
