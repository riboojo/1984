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

    private float speed = 2f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Update()
    {
        MoveCursor();
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

    void MoveCursor()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, speed);
        Vector3 newCursorPos = new Vector3(screen.ScreenToWorldPoint(mousePos).x, screen.ScreenToWorldPoint(mousePos).y, cursor.transform.position.z);

        Vector3 curCursorPor = cursor.transform.position;

        if ((newCursorPos.x >= delimiters[0].position.x) && (newCursorPos.x <= delimiters[1].position.x))
        {
            cursor.transform.position = new Vector3(newCursorPos.x, cursor.transform.position.y, cursor.transform.position.z);
        }

        if ((newCursorPos.y <= delimiters[2].position.y) && (newCursorPos.y >= delimiters[3].position.y))
        {
            cursor.transform.position = new Vector3(cursor.transform.position.x, newCursorPos.y, cursor.transform.position.z);
        }
    }
}
