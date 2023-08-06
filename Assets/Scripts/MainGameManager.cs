using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blur;

    [SerializeField]
    private GameObject guides;

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
        if (screenActiveRequest)
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
        //blur.SetActive(true);
        guides.SetActive(false);
        CursorManager.GetInstance().SetCursorStatus(true);

        mainCamera.GetComponent<CameraMovement>().CenterCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", true);
    }

    private void DeactivateScreen()
    {
        //blur.SetActive(false);
        guides.SetActive(true);
        CursorManager.GetInstance().SetCursorStatus(false);

        mainCamera.GetComponent<CameraMovement>().MoveCamera();
        mainCamera.GetComponent<Animator>().SetBool("zoom", false);
    }

}
