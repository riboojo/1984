using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private Camera screen;
    [SerializeField]
    private Transform[] delimiters; // x:9_19 y:-3.69_6.29
    
    private float sensitivity = 0.5f;
    private Vector2 mouseTurn;

    private bool isMouseCalibrated = false;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (isMouseCalibrated)
        {
            MoveCursor();
        }
    }

    public bool IsCursorOnScreen()
    {
        Vector3 view = screen.ScreenToViewportPoint(Input.mousePosition);
        bool isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;

        return isOutside;
    }

    public Vector3 GetCursorPosition()
    {
        return cursor.transform.position;
    }

    public Vector3 GetMousePosition()
    {
        return mouseTurn;
    }

    public void MouseCalibrationDone()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isMouseCalibrated = true;
    }

    void MoveCursor()
    {
        Vector3 newCursorPos = cursor.transform.position;

        mouseTurn.x += Input.GetAxis("Mouse X") * sensitivity;
        mouseTurn.y += Input.GetAxis("Mouse Y") * sensitivity;
        
        Vector3 currMousePos = new Vector3(mouseTurn.x, mouseTurn.y, cursor.transform.position.z);

        if ((currMousePos.x >= delimiters[0].position.x) && (currMousePos.x <= delimiters[1].position.x))
        {
            newCursorPos.x = currMousePos.x;
            Debug.Log("Inside X delimiter");
        }

        if ((currMousePos.y <= delimiters[2].position.y) && (currMousePos.y >= delimiters[3].position.y))
        {
            newCursorPos.y = currMousePos.y;
            Debug.Log("Inside Y delimiter");
        }

        cursor.transform.position = newCursorPos;
    }
}
