using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCalibrateMouse;
    
    [SerializeField]
    private CursorManager cursorManager;
    
    void Start()
    {
        menuCalibrateMouse.SetActive(true);
    }

    public void MouseCalibrationRequested()
    {
        menuCalibrateMouse.SetActive(false);
        cursorManager.MouseCalibrationDone();
    }
}
