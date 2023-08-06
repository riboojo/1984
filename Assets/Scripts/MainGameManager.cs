using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private CursorManager cursorManager;

    [SerializeField]
    private GameObject blur;
    
    [SerializeField]
    private Camera mainCamera;

    private bool isScreenActive = false;
    private bool screenActiveRequest = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            screenActiveRequest = true;
        }
        else
        {
            screenActiveRequest = false;
        }

        UpdateGameMode();
    }

    public void MouseCalibrationRequested()
    {

    }

    private void UpdateGameMode()
    {
        //if (screenActiveRequest && !isScreenActive)
        if (screenActiveRequest)
        {
            ActivateScreen();
        }
        //else if (!screenActiveRequest && isScreenActive)
        else
        {
            DeactivateScreen();
        }
    }

    private void ActivateScreen()
    {
        //blur.SetActive(true);
        cursorManager.SetCursorStatus(true);

        mainCamera.GetComponent<CameraMovement>().CenterCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", true);

        isScreenActive = true;
    }

    private void DeactivateScreen()
    {
        //blur.SetActive(false);
        cursorManager.SetCursorStatus(false);

        mainCamera.GetComponent<CameraMovement>().MoveCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", false);

        isScreenActive = false;
    }

}
