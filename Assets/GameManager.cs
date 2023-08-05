using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CursorManager cursorManager;

    [SerializeField]
    private GameObject blur;
    
    [SerializeField]
    private Camera mainCamera;

    private bool isScreenActive = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isScreenActive = true;
        }
        else
        {
            isScreenActive = false;
        }

        UpdateGameMode();
    }

    public void MouseCalibrationRequested()
    {

    }

    private void UpdateGameMode()
    {
        if (isScreenActive)
        {
            ActivateScreen();
        }
        else
        {
            DeactivateScreen();
        }
    }

    private void ActivateScreen()
    {
        blur.SetActive(true);
        cursorManager.SetCursorStatus(true);

        mainCamera.GetComponent<CameraMovement>().CenterCamera();
    }

    private void DeactivateScreen()
    {
        blur.SetActive(false);
        cursorManager.SetCursorStatus(false);

        mainCamera.GetComponent<CameraMovement>().MoveCamera();
    }

}
