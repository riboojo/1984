using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;

    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private Camera screen;
    [SerializeField]
    private Transform[] delimiters; // x:9_19 y:-3.69_6.29
    
    private float sensitivity = 0.5f;
    private Vector2 mouseTurn;

    private bool canMove = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene");
        }
        instance = this;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static CursorManager GetInstance()
    {
        return instance;
    }

    void Update()
    {
        if (canMove)
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

    public void SetCursorStatus(bool isActive)
    {
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canMove = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canMove = false;
        }
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
            //Debug.Log("Inside X delimiter");
        }

        if ((currMousePos.y <= delimiters[2].position.y) && (currMousePos.y >= delimiters[3].position.y))
        {
            newCursorPos.y = currMousePos.y;
            //Debug.Log("Inside Y delimiter");
        }

        cursor.transform.position = newCursorPos;
    }

    public void ShowCursor()
    {
        cursor.SetActive(true);
    }

    public void HideCursor()
    {
        cursor.SetActive(false);
    }
}
